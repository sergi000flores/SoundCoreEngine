// MainForm.cs
// Lógica de interfaz y sincronización de la GUI de SoundCore Engine.
//
// IMPORTANTE (medición real de inserciones): cada acción de cola que el
// usuario dispara desde la GUI (Encolar, Reproducir Siguiente, Avanzar,
// Invertir, Ordenar por BPM, Purgar Duplicados) se ejecuta EN ESPEJO sobre
// las 3 estructuras (ListaSimpleEnlazada propia, LinkedList<T>, List<T>)
// con las mismas pistas reales que el usuario va agregando, midiendo el
// tiempo de cada una con Stopwatch. Así las tres siempre contienen el mismo
// contenido (se puede cambiar el "Modo" solo para inspeccionar cómo luce
// cada estructura) y la telemetría en vivo refleja tiempos reales, no datos
// sintéticos. El botón de "Benchmark de Telemetría" es, aparte, una prueba
// de estrés con 25,000 inserciones sintéticas para ver el comportamiento a
// gran escala (down las 25,000 canciones reales no son prácticas de crear).

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using SoundCore.EstructurasPropias;
using SoundCore.Modelos;

namespace SoundCore.UI
{
  public partial class MainForm : Form
  {
    // Las tres estructuras se mantienen SIEMPRE sincronizadas: toda acción
    // de cola se aplica a las tres con las mismas pistas reales, cada una
    // cronometrada por separado con Stopwatch.
    private readonly ListaSimpleEnlazada<Pista> pistasPropia = new();
    private readonly AdaptadorLinkedList<Pista> pistasLinked = new();
    private readonly AdaptadorList<Pista> pistasNet = new();

    private int siguienteId = 1;

    // Cursor de reproducción: guarda el Id de la pista que está "sonando".
    // NO es una posición fija ni mueve nodos: en cada refresco se busca este
    // Id dentro del orden actual de la cola, así que "Avanzar Pista" nunca
    // reordena ni duplica nada, solo mueve este puntero a la siguiente
    // posición de la lista tal como está.
    private int? idPistaSonando;

    // --- Motor de audio (NAudio) ---
    private IWavePlayer? dispositivoSalida;
    private AudioFileReader? lectorAudio;
    private Pista? pistaCargada;
    private bool detencionManual;
    private bool silenciado;
    private bool arrastrandoProgreso;

    // "Modo" ahora solo decide qué estructura se MUESTRA/reproduce (vista),
    // ya que las tres siempre tienen el mismo contenido real.
    private IColaDJ<Pista> Activa
    {
      get
      {
        if (rdoLinkedList.Checked) return pistasLinked;
        if (rdoListNet.Checked) return pistasNet;
        return pistasPropia;
      }
    }

    public MainForm()
    {
      InitializeComponent();
      ConfigurarColumnasGrid();
      RefrescarPlaylist();
    }

    private void ConfigurarColumnasGrid()
    {
      dgvPlaylist.AutoGenerateColumns = false;
      dgvPlaylist.Columns.Clear();

      dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Posicion",
        DataPropertyName = "Posicion",
        HeaderText = "#",
        Width = 45
      });
      dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Titulo",
        DataPropertyName = "Titulo",
        HeaderText = "Título",
        Width = 190
      });
      dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Artista",
        DataPropertyName = "Artista",
        HeaderText = "Artista",
        Width = 160
      });
      dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Bpm",
        DataPropertyName = "Bpm",
        HeaderText = "BPM",
        Width = 60
      });
      dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Duracion",
        DataPropertyName = "Duracion",
        HeaderText = "Duración",
        Width = 80
      });
      dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Puntero",
        DataPropertyName = "Puntero",
        HeaderText = "Nodo -> Apunta a",
        Width = 150
      });
      dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Archivo",
        DataPropertyName = "Archivo",
        HeaderText = "Archivo de audio",
        Width = 150,
        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
      });
    }

    // Sincroniza el grid, la pista "sonando", el total en cola y el
    // tiempo total con el estado de la estructura que se está viendo.
    private void RefrescarPlaylist()
    {
      var items = Activa.ToList();

      int indiceSonando = ObtenerIndiceSonando(items);
      idPistaSonando = indiceSonando >= 0 ? items[indiceSonando].Id : null;

      var filas = items.Select((p, i) => new FilaPlaylist
      {
        Id = p.Id,
        Posicion = i + 1,
        Titulo = p.Titulo,
        Artista = p.Artista,
        Bpm = p.Bpm,
        Duracion = p.DuracionFormateada,
        Puntero = $"0x{(p.GetHashCode() & 0xFF):X2} -> {(i == items.Count - 1 ? "Null" : "Next")}",
        Archivo = Path.GetFileName(p.RutaArchivo)
      }).ToList();

      dgvPlaylist.DataSource = null;
      dgvPlaylist.DataSource = filas;

      lblSonando.Text = indiceSonando >= 0
          ? $"▶ Sonando: \"{items[indiceSonando].Titulo}\" - {items[indiceSonando].Artista} " +
            $"({items[indiceSonando].Bpm} BPM)   —   posición {indiceSonando + 1} de {items.Count}"
          : "▶ Sonando: (la cola está vacía)";

      lblTotalCola.Text = $"Total en cola: {items.Count} pista(s)";

      int totalSegundos = items.Sum(p => p.DuracionSegundos);
      lblTiempoTotal.Text = $"Tiempo total: {TimeSpan.FromSeconds(totalSegundos):mm\\:ss}";
    }

    // Busca, dentro del orden ACTUAL de la cola (tal como está, sin
    // moverla), la posición de la pista marcada como "sonando". Si esa
    // pista ya no existe (se eliminó, se purgó como duplicada, etc.) cae
    // de vuelta a la primera posición, sin duplicar ni perder nada.
    private int ObtenerIndiceSonando(System.Collections.Generic.List<Pista> items)
    {
      if (items.Count == 0) return -1;
      if (idPistaSonando == null) return 0;

      int indice = items.FindIndex(p => p.Id == idPistaSonando.Value);
      return indice >= 0 ? indice : 0;
    }

    private void LimpiarCampos()
    {
      txtTitulo.Text = "";
      txtArtista.Text = "";
      txtBpm.Text = "";
      txtDuracion.Text = "";
      txtRutaArchivo.Text = "";
      lblEstadoDeteccion.Text = "Selecciona un archivo de audio: el título, la duración y el BPM se detectan automáticamente.";
      txtTitulo.Focus();
    }

    // Valida los campos de "Registro" (incluido el archivo de audio) y
    // arma una Pista lista para encolar.
    private Pista? ObtenerPistaDesdeFormulario()
    {
      if (string.IsNullOrWhiteSpace(txtRutaArchivo.Text) || !File.Exists(txtRutaArchivo.Text))
      {
        MessageBox.Show("Debe seleccionar un archivo de audio válido (botón \"Examinar...\").",
            "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        btnExaminar.Focus();
        return null;
      }

      if (string.IsNullOrWhiteSpace(txtTitulo.Text))
      {
        MessageBox.Show("Debe ingresar el título de la pista.", "Dato inválido",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtTitulo.Focus();
        return null;
      }

      if (string.IsNullOrWhiteSpace(txtArtista.Text))
      {
        MessageBox.Show("Debe ingresar el artista.", "Dato inválido",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtArtista.Focus();
        return null;
      }

      if (!int.TryParse(txtBpm.Text.Trim(), out int bpm) || bpm <= 0)
      {
        MessageBox.Show("El BPM debe ser un número entero positivo (se detecta solo al elegir el archivo, pero puedes corregirlo).",
            "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtBpm.Focus();
        return null;
      }

      if (!int.TryParse(txtDuracion.Text.Trim(), out int duracion) || duracion <= 0)
      {
        MessageBox.Show("La duración debe ser un número entero de segundos (se detecta sola al elegir el archivo).",
            "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtDuracion.Focus();
        return null;
      }

      return new Pista(siguienteId++, txtTitulo.Text.Trim(), txtArtista.Text.Trim(),
          bpm, duracion, txtRutaArchivo.Text.Trim());
    }

    // ---------------------------------------------------------------
    // Selección de archivo + detección automática de metadatos.
    // ---------------------------------------------------------------
    private async void btnExaminar_Click(object sender, EventArgs e)
    {
      using var dialogo = new OpenFileDialog
      {
        Title = "Selecciona el archivo de audio de la pista",
        Filter = "Archivos de audio (*.mp3;*.wav;*.wma;*.aac;*.m4a;*.flac)|*.mp3;*.wav;*.wma;*.aac;*.m4a;*.flac|Todos los archivos (*.*)|*.*"
      };

      if (dialogo.ShowDialog(this) != DialogResult.OK)
      {
        return;
      }

      txtRutaArchivo.Text = dialogo.FileName;
      txtTitulo.Text = "";
      txtArtista.Text = "";
      txtDuracion.Text = "";
      txtBpm.Text = "";

      await AutoCompletarMetadatosAsync(dialogo.FileName);
    }

    private async Task AutoCompletarMetadatosAsync(string ruta)
    {
      btnExaminar.Enabled = false;
      Cursor.Current = Cursors.WaitCursor;
      lblEstadoDeteccion.Text = "🔍 Analizando el audio (título, duración y BPM)...";

      try
      {
        var metadatos = await Task.Run(() => LeerMetadatosDelArchivo(ruta));
        int bpmEstimado = await Task.Run(() => EstimarBpmDelAudio(ruta));

        txtTitulo.Text = !string.IsNullOrWhiteSpace(metadatos.Titulo)
            ? metadatos.Titulo
            : Path.GetFileNameWithoutExtension(ruta);

        txtArtista.Text = !string.IsNullOrWhiteSpace(metadatos.Artista)
            ? metadatos.Artista
            : "Desconocido";

        txtDuracion.Text = metadatos.DuracionSegundos > 0
            ? metadatos.DuracionSegundos.ToString()
            : "";

        txtBpm.Text = bpmEstimado > 0 ? bpmEstimado.ToString() : "";

        lblEstadoDeteccion.Text = bpmEstimado > 0
            ? "✅ Título, duración y BPM detectados automáticamente del audio (puedes ajustarlos si lo deseas)."
            : "⚠️ Se detectó el título y la duración, pero el BPM no pudo estimarse; ingrésalo manualmente.";
      }
      catch (Exception ex)
      {
        lblEstadoDeteccion.Text = "⚠️ No se pudieron detectar los metadatos automáticamente; complétalos a mano.";
        MessageBox.Show($"No se pudo analizar completamente el archivo:\n{ex.Message}",
            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      finally
      {
        btnExaminar.Enabled = true;
        Cursor.Current = Cursors.Default;
      }
    }

    private readonly record struct MetadatosAudio(string Titulo, string Artista, int DuracionSegundos);

    private static MetadatosAudio LeerMetadatosDelArchivo(string ruta)
    {
      using var archivo = TagLib.File.Create(ruta);

      string titulo = archivo.Tag.Title ?? "";
      string artista = !string.IsNullOrWhiteSpace(archivo.Tag.FirstPerformer)
          ? archivo.Tag.FirstPerformer
          : (archivo.Tag.FirstAlbumArtist ?? "");
      int duracion = (int)archivo.Properties.Duration.TotalSeconds;

      return new MetadatosAudio(titulo, artista, duracion);
    }

    // ---------------------------------------------------------------
    // Estimación de BPM analizando directamente las muestras del audio
    // (energía + autocorrelación). Ver README para el detalle del algoritmo.
    // ---------------------------------------------------------------
    private static int EstimarBpmDelAudio(string ruta)
    {
      const int segundosAAnalizar = 60;

      using var lector = new AudioFileReader(ruta);
      int canales = lector.WaveFormat.Channels;
      int frecuenciaMuestreo = lector.WaveFormat.SampleRate;
      long maxMuestras = (long)frecuenciaMuestreo * segundosAAnalizar;

      var monoMuestras = new System.Collections.Generic.List<float>();
      float[] buffer = new float[canales * 4096];
      int leidos;

      while ((leidos = lector.Read(buffer, 0, buffer.Length)) > 0)
      {
        int frames = leidos / canales;
        for (int f = 0; f < frames; f++)
        {
          float suma = 0f;
          for (int c = 0; c < canales; c++)
          {
            suma += buffer[f * canales + c];
          }
          monoMuestras.Add(suma / canales);

          if (monoMuestras.Count >= maxMuestras) break;
        }
        if (monoMuestras.Count >= maxMuestras) break;
      }

      if (monoMuestras.Count < frecuenciaMuestreo)
      {
        return 0;
      }

      int tamVentana = Math.Max(1, frecuenciaMuestreo / 100);
      int numVentanas = monoMuestras.Count / tamVentana;
      if (numVentanas < 10)
      {
        return 0;
      }

      double[] energia = new double[numVentanas];
      for (int i = 0; i < numVentanas; i++)
      {
        double suma = 0;
        int inicio = i * tamVentana;
        for (int j = 0; j < tamVentana; j++)
        {
          float m = monoMuestras[inicio + j];
          suma += (double)m * m;
        }
        energia[i] = suma;
      }

      double[] onset = new double[numVentanas];
      for (int i = 1; i < numVentanas; i++)
      {
        double diferencia = energia[i] - energia[i - 1];
        onset[i] = diferencia > 0 ? diferencia : 0;
      }

      double tasaEnvolvente = frecuenciaMuestreo / (double)tamVentana;

      double mejorBpm = 0;
      double mejorPuntaje = double.MinValue;

      for (double bpmCandidato = 60; bpmCandidato <= 200; bpmCandidato += 0.5)
      {
        double periodoSegundos = 60.0 / bpmCandidato;
        int retraso = (int)Math.Round(periodoSegundos * tasaEnvolvente);
        if (retraso < 1 || retraso >= onset.Length)
        {
          continue;
        }

        double suma = 0;
        int cuenta = 0;
        for (int i = 0; i + retraso < onset.Length; i++)
        {
          suma += onset[i] * onset[i + retraso];
          cuenta++;
        }
        if (cuenta == 0)
        {
          continue;
        }

        double puntaje = suma / cuenta;
        if (puntaje > mejorPuntaje)
        {
          mejorPuntaje = puntaje;
          mejorBpm = bpmCandidato;
        }
      }

      return mejorBpm > 0 ? (int)Math.Round(mejorBpm) : 0;
    }

    // =================================================================
    // TELEMETRÍA EN VIVO: cada acción real de cola se aplica en espejo a
    // las 3 estructuras, cronometrando cada una individualmente con
    // Stopwatch, sobre las MISMAS pistas reales que el usuario agregó.
    // =================================================================

    // Para operaciones sin valor de retorno (Agregar/Reproducir/Invertir/Purgar).
    private void MedirYEjecutarEnTodas(string nombreOperacion, Action<IColaDJ<Pista>> operacion)
    {
      var sw = new Stopwatch();

      sw.Restart();
      operacion(pistasPropia);
      sw.Stop();
      double msPropia = sw.Elapsed.TotalMilliseconds;

      sw.Restart();
      operacion(pistasLinked);
      sw.Stop();
      double msLinked = sw.Elapsed.TotalMilliseconds;

      sw.Restart();
      operacion(pistasNet);
      sw.Stop();
      double msNet = sw.Elapsed.TotalMilliseconds;

      ActualizarTelemetriaEnVivo(nombreOperacion, msPropia, msLinked, msNet);
    }

    private void ActualizarTelemetriaEnVivo(string operacion, double msPropia, double msLinked, double msNet)
    {
      int n = pistasPropia.Count; // las 3 estructuras siempre tienen el mismo tamaño

      lblTelemetriaEnVivo.Text =
          $"⏱ Última operación real: \"{operacion}\"  (n = {n} pista(s) en cola)   —   " +
          $"Lista Propia: {FormatoMicrosegundos(msPropia)}   |   " +
          $".NET LinkedList<T>: {FormatoMicrosegundos(msLinked)}   |   " +
          $".NET List<T>: {FormatoMicrosegundos(msNet)}";
    }

    private static string FormatoMicrosegundos(double milisegundos)
    {
      double microsegundos = Math.Max(0, milisegundos) * 1000.0;
      return $"{microsegundos:F2} µs";
    }

    // ---------------------------------------------------------------
    // CP-01: Inserción Final (medida en tiempo real sobre las 3 estructuras)
    // ---------------------------------------------------------------
    private void btnEncolarFinal_Click(object sender, EventArgs e)
    {
      Pista? nueva = ObtenerPistaDesdeFormulario();
      if (nueva == null) return;

      MedirYEjecutarEnTodas("Encolar al Final", c => c.AgregarAlFinal(nueva));
      RefrescarPlaylist();
      LimpiarCampos();
    }

    // ---------------------------------------------------------------
    // CP-02: Up Next VIP (medida en tiempo real sobre las 3 estructuras)
    // ---------------------------------------------------------------
    private void btnReproducirSiguiente_Click(object sender, EventArgs e)
    {
      Pista? nueva = ObtenerPistaDesdeFormulario();
      if (nueva == null) return;

      MedirYEjecutarEnTodas("Reproducir Siguiente", c => c.ReproducirSiguiente(nueva));
      RefrescarPlaylist();
      LimpiarCampos();
    }

    // ---------------------------------------------------------------
    // CP-03: Avanzar Pista / CP-07: manejo de errores en cola vacía.
    // IMPORTANTE: esto ya NO mueve nodos ni desencola nada de la cola.
    // La cola se queda exactamente en el orden en que se fue armando
    // (Encolar al Final / Reproducir Siguiente / Invertir / Ordenar BPM
    // siguen siendo las únicas acciones que cambian ese orden). "Avanzar
    // Pista" solo desplaza el CURSOR de reproducción (idPistaSonando) a la
    // siguiente posición de la lista tal como está, y vuelve a la posición
    // 1 después de la última (set en bucle). AvanzarPista()/AgregarAlFinal()
    // siguen siendo los algoritmos reales del reto: se usan tal cual en
    // "Ordenar por Curva BPM" y en "Eliminar Pista" (más abajo), donde sí
    // hace falta drenar y reconstruir la cola.
    // ---------------------------------------------------------------
    private void btnAvanzarPista_Click(object sender, EventArgs e) => EjecutarAvanzarPista();

    private void EjecutarAvanzarPista()
    {
      var items = Activa.ToList();

      if (items.Count == 0)
      {
        MessageBox.Show("La cola está vacía: no hay ninguna pista sonando para avanzar.",
            "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
        RefrescarPlaylist();
        return;
      }

      int indiceActual = ObtenerIndiceSonando(items);
      int indiceSiguiente = (indiceActual + 1) % items.Count; // vuelve al inicio tras la última
      idPistaSonando = items[indiceSiguiente].Id;

      RefrescarPlaylist();
      ReproducirCabezaActual();
    }

    // ---------------------------------------------------------------
    // Eliminar Pista (selección manual). No es uno de los 6 algoritmos
    // del reto, así que no se agrega ningún método nuevo a IColaDJ<T>:
    // se implementa componiendo los dos que ya existen (AvanzarPista +
    // AgregarAlFinal) para drenar toda la estructura, descartar solo la
    // pista seleccionada y volver a encolar el resto en el mismo orden.
    // Se aplica en espejo a las 3 estructuras para que sigan sincronizadas.
    // ---------------------------------------------------------------
    private void btnEliminarPista_Click(object sender, EventArgs e)
    {
      if (dgvPlaylist.CurrentRow?.DataBoundItem is not FilaPlaylist filaSeleccionada)
      {
        MessageBox.Show("Selecciona primero, en la tabla, la pista que quieres eliminar.",
            "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      int idAEliminar = filaSeleccionada.Id;
      string tituloEliminado = filaSeleccionada.Titulo;
      bool eraLaQueSonaba = pistaCargada?.Id == idAEliminar;

      MedirYEjecutarEnTodas("Eliminar Pista", c => EliminarPorId(c, idAEliminar));
      RefrescarPlaylist();

      if (eraLaQueSonaba)
      {
        DetenerYLiberarReproductor();
        pistaCargada = null;
        ReiniciarBarraDeProgreso();
      }

      MessageBox.Show($"Se eliminó \"{tituloEliminado}\" de la cola.",
          "Pista eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static void EliminarPorId(IColaDJ<Pista> estructura, int idAEliminar)
    {
      var restantes = new System.Collections.Generic.List<Pista>();
      Pista? actual;

      while ((actual = estructura.AvanzarPista()) != null)
      {
        if (actual.Id != idAEliminar)
        {
          restantes.Add(actual);
        }
      }

      foreach (var p in restantes)
      {
        estructura.AgregarAlFinal(p);
      }
    }

    // ---------------------------------------------------------------
    // CP-04: Inversión In-Place (medida en tiempo real sobre las 3)
    // ---------------------------------------------------------------
    private void btnInvertir_Click(object sender, EventArgs e)
    {
      MedirYEjecutarEnTodas("Invertir Lista (In-Place)", c => c.Invertir());
      RefrescarPlaylist();
    }

    // ---------------------------------------------------------------
    // CP-05: Curva BPM (orden ascendente), medida en tiempo real sobre
    // las 3 estructuras. Cada una se drena con AvanzarPista() y se
    // reinserta con InsertarOrdenado(): solo se usan los 6 métodos del
    // contrato, sin listas ni arreglos auxiliares externos.
    // ---------------------------------------------------------------
    private void btnOrdenarBpm_Click(object sender, EventArgs e)
    {
      Comparison<Pista> porBpmAscendente = (a, b) => a.Bpm.CompareTo(b.Bpm);
      var sw = new Stopwatch();

      sw.Restart();
      OrdenarEstructura(pistasPropia, porBpmAscendente);
      sw.Stop();
      double msPropia = sw.Elapsed.TotalMilliseconds;

      sw.Restart();
      OrdenarEstructura(pistasLinked, porBpmAscendente);
      sw.Stop();
      double msLinked = sw.Elapsed.TotalMilliseconds;

      sw.Restart();
      OrdenarEstructura(pistasNet, porBpmAscendente);
      sw.Stop();
      double msNet = sw.Elapsed.TotalMilliseconds;

      ActualizarTelemetriaEnVivo("Ordenar por Curva BPM", msPropia, msLinked, msNet);
      RefrescarPlaylist();
    }

    private static void OrdenarEstructura(IColaDJ<Pista> estructura, Comparison<Pista> comparador)
    {
      var extraidas = new System.Collections.Generic.List<Pista>();
      Pista? p;
      while ((p = estructura.AvanzarPista()) != null)
      {
        extraidas.Add(p);
      }
      foreach (var item in extraidas)
      {
        estructura.InsertarOrdenado(item, comparador);
      }
    }

    // ---------------------------------------------------------------
    // CP-06: Purgar Duplicados, medida en tiempo real sobre las 3.
    // ---------------------------------------------------------------
    private void btnPurgarDuplicados_Click(object sender, EventArgs e)
    {
      Func<Pista, Pista, bool> mismoTitulo =
          (a, b) => string.Equals(a.Titulo, b.Titulo, StringComparison.OrdinalIgnoreCase);

      MedirYEjecutarEnTodas("Purgar Duplicados", c => c.DepurarDuplicados(mismoTitulo));
      RefrescarPlaylist();
    }

    // Cambiar "Modo" ya no reinicia nada: las 3 estructuras siempre tienen
    // el mismo contenido real; esto solo cambia cuál se muestra/inspecciona.
    private void rdoModo_CheckedChanged(object sender, EventArgs e)
    {
      RefrescarPlaylist();
    }

    // ---------------------------------------------------------------
    // Reproducción real de audio (NAudio) sobre la pista "sonando".
    // ---------------------------------------------------------------
    private void btnReproducir_Click(object sender, EventArgs e) => ReproducirCabezaActual();

    private void ReproducirCabezaActual()
    {
      var items = Activa.ToList();
      if (items.Count == 0)
      {
        MessageBox.Show("La cola está vacía: no hay ninguna pista para reproducir.",
            "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      int indiceSonando = ObtenerIndiceSonando(items);
      var cabeza = items[indiceSonando];

      if (!File.Exists(cabeza.RutaArchivo))
      {
        MessageBox.Show($"No se encontró el archivo de audio de \"{cabeza.Titulo}\":\n{cabeza.RutaArchivo}",
            "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (pistaCargada?.Id != cabeza.Id)
      {
        DetenerYLiberarReproductor();

        lectorAudio = new AudioFileReader(cabeza.RutaArchivo)
        {
          Volume = silenciado ? 0f : trkVolumen.Value / 100f
        };
        dispositivoSalida = new WaveOutEvent();
        dispositivoSalida.PlaybackStopped += DispositivoSalida_PlaybackStopped;
        dispositivoSalida.Init(lectorAudio);
        pistaCargada = cabeza;

        trkProgreso.Maximum = Math.Max(1, (int)lectorAudio.TotalTime.TotalSeconds);
        trkProgreso.Value = 0;
      }

      dispositivoSalida!.Play();
    }

    private void btnPausar_Click(object sender, EventArgs e)
    {
      dispositivoSalida?.Pause();
    }

    private void btnDetener_Click(object sender, EventArgs e)
    {
      if (dispositivoSalida == null) return;
      detencionManual = true;
      dispositivoSalida.Stop();
      if (lectorAudio != null)
      {
        lectorAudio.CurrentTime = TimeSpan.Zero;
      }
      ReiniciarBarraDeProgreso();
    }

    private void btnMute_Click(object sender, EventArgs e)
    {
      silenciado = !silenciado;
      btnMute.Text = silenciado ? "🔇" : "🔊";
      AplicarVolumenActual();
    }

    private void trkVolumen_ValueChanged(object sender, EventArgs e)
    {
      silenciado = false;
      btnMute.Text = "🔊";
      AplicarVolumenActual();
    }

    private void AplicarVolumenActual()
    {
      if (lectorAudio != null)
      {
        lectorAudio.Volume = silenciado ? 0f : trkVolumen.Value / 100f;
      }
    }

    private void trkProgreso_MouseDown(object sender, MouseEventArgs e)
    {
      arrastrandoProgreso = true;
    }

    private void trkProgreso_MouseUp(object sender, MouseEventArgs e)
    {
      arrastrandoProgreso = false;
      if (lectorAudio != null)
      {
        lectorAudio.CurrentTime = TimeSpan.FromSeconds(trkProgreso.Value);
      }
    }

    private void timerProgreso_Tick(object sender, EventArgs e)
    {
      if (lectorAudio == null || arrastrandoProgreso)
      {
        return;
      }

      double actual = lectorAudio.CurrentTime.TotalSeconds;
      double total = lectorAudio.TotalTime.TotalSeconds;

      if (total > 0)
      {
        trkProgreso.Maximum = Math.Max(1, (int)total);
        trkProgreso.Value = Math.Min(trkProgreso.Maximum, (int)actual);
      }

      lblTiempoActual.Text = FormatoTiempo(actual);
      lblTiempoTotalPista.Text = FormatoTiempo(total);
    }

    private void ReiniciarBarraDeProgreso()
    {
      trkProgreso.Value = 0;
      lblTiempoActual.Text = "00:00";
      lblTiempoTotalPista.Text = "00:00";
    }

    private static string FormatoTiempo(double segundos)
    {
      if (double.IsNaN(segundos) || segundos < 0)
      {
        segundos = 0;
      }
      return TimeSpan.FromSeconds(segundos).ToString(@"mm\:ss");
    }

    private void DispositivoSalida_PlaybackStopped(object? sender, StoppedEventArgs e)
    {
      bool fueManual = detencionManual;
      detencionManual = false;

      if (fueManual || IsDisposed)
      {
        return;
      }

      if (InvokeRequired)
      {
        BeginInvoke(new Action(ManejarFinDePista));
      }
      else
      {
        ManejarFinDePista();
      }
    }

    private void ManejarFinDePista()
    {
      if (chkRepetir.Checked && lectorAudio != null && dispositivoSalida != null)
      {
        lectorAudio.CurrentTime = TimeSpan.Zero;
        dispositivoSalida.Play();
        return;
      }

      EjecutarAvanzarPista();
    }

    private void DetenerYLiberarReproductor()
    {
      if (dispositivoSalida != null)
      {
        detencionManual = true;
        dispositivoSalida.PlaybackStopped -= DispositivoSalida_PlaybackStopped;
        dispositivoSalida.Stop();
        dispositivoSalida.Dispose();
        dispositivoSalida = null;
      }

      lectorAudio?.Dispose();
      lectorAudio = null;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
      DetenerYLiberarReproductor();
      base.OnFormClosed(e);
    }

    // ---------------------------------------------------------------
    // Prueba de ESTRÉS SINTÉTICA y PROGRESIVA: mide el tiempo PROMEDIO por
    // inserción (ReproducirSiguiente) en 6 tamaños de cola crecientes
    // (1,000 a 25,000), no un solo número final. Antes de medir se hace un
    // "warm-up" (una corrida corta que se descarta) para que el JIT ya haya
    // compilado los métodos y no distorsione la primera medición real —
    // esto es lo que evita los "datos incongruentes" entre corridas.
    // El resultado se muestra en una tabla (FormResultadoBenchmark) que
    // deja ver el CRECIMIENTO del promedio en List<T> (evidencia de O(n))
    // frente al promedio prácticamente plano de las listas enlazadas
    // (evidencia de O(1)), en vez de un solo total agregado.
    // No usa las pistas reales del usuario (esas se miden en vivo arriba,
    // con cada clic real); aquí se generan pistas sintéticas porque crear
    // 25,000 pistas reales a mano no es práctico.
    // ---------------------------------------------------------------
    private static readonly int[] PuntosControl = { 1000, 5000, 10000, 15000, 20000, 25000 };

    private void btnBenchmark_Click(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.WaitCursor;
      Enabled = false;

      try
      {
        EjecutarLoteDeCalentamiento();

        var semilla = new Random(2026);
        var promediosPropia = MedirPromedioPorTramo(() => new ListaSimpleEnlazada<Pista>(), semilla);
        var promediosLinked = MedirPromedioPorTramo(() => new AdaptadorLinkedList<Pista>(), semilla);
        var promediosList = MedirPromedioPorTramo(() => new AdaptadorList<Pista>(), semilla);

        var filas = new System.Collections.Generic.List<FilaBenchmark>();
        for (int i = 0; i < PuntosControl.Length; i++)
        {
          filas.Add(new FilaBenchmark
          {
            Tamano = PuntosControl[i],
            PropiaMicroseg = promediosPropia[i],
            LinkedMicroseg = promediosLinked[i],
            ListMicroseg = promediosList[i]
          });
        }

        double listInicial = promediosList[0];
        double listFinal = promediosList[^1];
        double factorCrecimiento = listInicial > 0 ? listFinal / listInicial : 0;

        string conclusion =
            $"Con {PuntosControl[0]:N0} pistas en la cola, insertar en List<T> tarda en promedio " +
            $"{listInicial:0.000} µs por inserción; con {PuntosControl[^1]:N0} pistas sube a " +
            $"{listFinal:0.000} µs (≈{factorCrecimiento:0.#}x más lento). Este CRECIMIENTO es la evidencia " +
            "empírica de la complejidad O(n): List<T>.Insert(1, …) usa internamente Array.Copy para desplazar " +
            "en bloque, hacia la derecha, todos los elementos posteriores al índice de inserción; mientras más " +
            "elementos haya en la cola, más elementos hay que mover en cada inserción.\n\n" +
            $"En cambio, la Lista Propia (≈{promediosPropia[0]:0.000} µs → ≈{promediosPropia[^1]:0.000} µs) y " +
            $".NET LinkedList<T> (≈{promediosLinked[0]:0.000} µs → ≈{promediosLinked[^1]:0.000} µs) se mantienen " +
            "prácticamente constantes sin importar el tamaño de la cola: cada inserción solo reconecta un par " +
            "de referencias (nodo.Siguiente), sin desplazar memoria. Esa es la complejidad O(1) confirmada con " +
            "Stopwatch, no solo enunciada por teoría.";

        lblBenchmarkResultado.Text =
            $"Última corrida ({PuntosControl[^1]:N0} inserciones): Lista Propia ≈ {promediosPropia[^1]:0.000} µs/ins  |  " +
            $"LinkedList<T> ≈ {promediosLinked[^1]:0.000} µs/ins  |  List<T> ≈ {promediosList[^1]:0.000} µs/ins " +
            "(la tabla completa muestra cómo crece cada una según el tamaño de la cola).";

        using var dialogo = new FormResultadoBenchmark(filas, conclusion);
        dialogo.ShowDialog(this);
      }
      finally
      {
        Enabled = true;
        Cursor.Current = Cursors.Default;
      }
    }

    // Corrida corta y descartable para que el JIT compile los métodos antes
    // de la medición real; sin esto, la primera corrida siempre sale más
    // lenta por la compilación en caliente y contamina la comparación.
    private void EjecutarLoteDeCalentamiento()
    {
      var rnd = new Random(1);
      var propia = new ListaSimpleEnlazada<Pista>();
      var linked = new AdaptadorLinkedList<Pista>();
      var listNet = new AdaptadorList<Pista>();

      for (int i = 0; i < 500; i++)
      {
        propia.ReproducirSiguiente(GenerarPistaAleatoria(i, rnd));
        linked.ReproducirSiguiente(GenerarPistaAleatoria(i, rnd));
        listNet.ReproducirSiguiente(GenerarPistaAleatoria(i, rnd));
      }
    }

    // Inserta hasta cada tamaño de PuntosControl y devuelve, por tramo, el
    // tiempo PROMEDIO por inserción (µs) de ese tramo únicamente (no
    // acumulado), para poder ver si ese promedio crece o se mantiene plano.
    private static double[] MedirPromedioPorTramo(Func<IColaDJ<Pista>> fabrica, Random semillaBase)
    {
      var estructura = fabrica();
      var rnd = new Random(semillaBase.Next());
      var promedios = new double[PuntosControl.Length];
      var cronometroTramo = new Stopwatch();
      int anterior = 0;

      for (int t = 0; t < PuntosControl.Length; t++)
      {
        int objetivo = PuntosControl[t];

        cronometroTramo.Restart();
        for (int i = anterior; i < objetivo; i++)
        {
          estructura.ReproducirSiguiente(GenerarPistaAleatoria(i, rnd));
        }
        cronometroTramo.Stop();

        int cantidadEnTramo = objetivo - anterior;
        double totalMicroseg = cronometroTramo.Elapsed.TotalMilliseconds * 1000.0;
        promedios[t] = totalMicroseg / cantidadEnTramo;
        anterior = objetivo;
      }

      return promedios;
    }

    private static Pista GenerarPistaAleatoria(int indice, Random rnd)
    {
      return new Pista(
          Id: 900_000 + indice,
          Titulo: $"BenchTrack {indice}",
          Artista: "Artista Benchmark",
          Bpm: rnd.Next(90, 180),
          DuracionSegundos: rnd.Next(120, 360),
          RutaArchivo: "");
    }
  }
}
