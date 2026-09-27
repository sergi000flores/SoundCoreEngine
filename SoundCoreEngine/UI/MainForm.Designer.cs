namespace SoundCore.UI
{
  partial class MainForm
  {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.timerProgreso = new System.Windows.Forms.Timer(this.components);
      this.grpRegistro = new System.Windows.Forms.GroupBox();
      this.lblEstadoDeteccion = new System.Windows.Forms.Label();
      this.btnExaminar = new System.Windows.Forms.Button();
      this.txtRutaArchivo = new System.Windows.Forms.TextBox();
      this.lblArchivo = new System.Windows.Forms.Label();
      this.rdoListNet = new System.Windows.Forms.RadioButton();
      this.rdoLinkedList = new System.Windows.Forms.RadioButton();
      this.rdoListaPropia = new System.Windows.Forms.RadioButton();
      this.lblModo = new System.Windows.Forms.Label();
      this.txtDuracion = new System.Windows.Forms.TextBox();
      this.lblDuracion = new System.Windows.Forms.Label();
      this.txtBpm = new System.Windows.Forms.TextBox();
      this.lblBpm = new System.Windows.Forms.Label();
      this.txtArtista = new System.Windows.Forms.TextBox();
      this.lblArtista = new System.Windows.Forms.Label();
      this.txtTitulo = new System.Windows.Forms.TextBox();
      this.lblTitulo = new System.Windows.Forms.Label();
      this.grpAcciones = new System.Windows.Forms.GroupBox();
      this.btnEliminarPista = new System.Windows.Forms.Button();
      this.btnPurgarDuplicados = new System.Windows.Forms.Button();
      this.btnOrdenarBpm = new System.Windows.Forms.Button();
      this.btnInvertir = new System.Windows.Forms.Button();
      this.btnAvanzarPista = new System.Windows.Forms.Button();
      this.btnReproducirSiguiente = new System.Windows.Forms.Button();
      this.btnEncolarFinal = new System.Windows.Forms.Button();
      this.grpPlaylist = new System.Windows.Forms.GroupBox();
      this.dgvPlaylist = new System.Windows.Forms.DataGridView();
      this.lblTiempoTotalPista = new System.Windows.Forms.Label();
      this.trkProgreso = new System.Windows.Forms.TrackBar();
      this.lblTiempoActual = new System.Windows.Forms.Label();
      this.trkVolumen = new System.Windows.Forms.TrackBar();
      this.lblVolumen = new System.Windows.Forms.Label();
      this.btnMute = new System.Windows.Forms.Button();
      this.chkRepetir = new System.Windows.Forms.CheckBox();
      this.btnDetener = new System.Windows.Forms.Button();
      this.btnPausar = new System.Windows.Forms.Button();
      this.btnReproducir = new System.Windows.Forms.Button();
      this.lblSonando = new System.Windows.Forms.Label();
      this.pnlEstado = new System.Windows.Forms.Panel();
      this.lblTiempoTotal = new System.Windows.Forms.Label();
      this.lblTotalCola = new System.Windows.Forms.Label();
      this.grpBenchmark = new System.Windows.Forms.GroupBox();
      this.lblBenchmarkResultado = new System.Windows.Forms.Label();
      this.btnBenchmark = new System.Windows.Forms.Button();
      this.lblTelemetriaEnVivo = new System.Windows.Forms.Label();
      this.grpRegistro.SuspendLayout();
      this.grpAcciones.SuspendLayout();
      this.grpPlaylist.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkProgreso)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkVolumen)).BeginInit();
      this.pnlEstado.SuspendLayout();
      this.grpBenchmark.SuspendLayout();
      this.SuspendLayout();
      //
      // timerProgreso
      //
      this.timerProgreso.Enabled = true;
      this.timerProgreso.Interval = 500;
      this.timerProgreso.Tick += new System.EventHandler(this.timerProgreso_Tick);
      //
      // grpRegistro
      //
      this.grpRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.grpRegistro.Controls.Add(this.lblEstadoDeteccion);
      this.grpRegistro.Controls.Add(this.btnExaminar);
      this.grpRegistro.Controls.Add(this.txtRutaArchivo);
      this.grpRegistro.Controls.Add(this.lblArchivo);
      this.grpRegistro.Controls.Add(this.rdoListNet);
      this.grpRegistro.Controls.Add(this.rdoLinkedList);
      this.grpRegistro.Controls.Add(this.rdoListaPropia);
      this.grpRegistro.Controls.Add(this.lblModo);
      this.grpRegistro.Controls.Add(this.txtDuracion);
      this.grpRegistro.Controls.Add(this.lblDuracion);
      this.grpRegistro.Controls.Add(this.txtBpm);
      this.grpRegistro.Controls.Add(this.lblBpm);
      this.grpRegistro.Controls.Add(this.txtArtista);
      this.grpRegistro.Controls.Add(this.lblArtista);
      this.grpRegistro.Controls.Add(this.txtTitulo);
      this.grpRegistro.Controls.Add(this.lblTitulo);
      this.grpRegistro.Location = new System.Drawing.Point(12, 12);
      this.grpRegistro.Name = "grpRegistro";
      this.grpRegistro.Size = new System.Drawing.Size(1160, 190);
      this.grpRegistro.TabIndex = 0;
      this.grpRegistro.TabStop = false;
      this.grpRegistro.Text = "Registro de Pista";
      //
      // lblTitulo
      //
      this.lblTitulo.AutoSize = true;
      this.lblTitulo.Location = new System.Drawing.Point(18, 30);
      this.lblTitulo.Name = "lblTitulo";
      this.lblTitulo.Size = new System.Drawing.Size(40, 15);
      this.lblTitulo.TabIndex = 0;
      this.lblTitulo.Text = "Título:";
      //
      // txtTitulo
      //
      this.txtTitulo.Location = new System.Drawing.Point(90, 27);
      this.txtTitulo.Name = "txtTitulo";
      this.txtTitulo.Size = new System.Drawing.Size(220, 23);
      this.txtTitulo.TabIndex = 1;
      //
      // lblArtista
      //
      this.lblArtista.AutoSize = true;
      this.lblArtista.Location = new System.Drawing.Point(330, 30);
      this.lblArtista.Name = "lblArtista";
      this.lblArtista.Size = new System.Drawing.Size(48, 15);
      this.lblArtista.TabIndex = 2;
      this.lblArtista.Text = "Artista:";
      //
      // txtArtista
      //
      this.txtArtista.Location = new System.Drawing.Point(390, 27);
      this.txtArtista.Name = "txtArtista";
      this.txtArtista.Size = new System.Drawing.Size(200, 23);
      this.txtArtista.TabIndex = 3;
      //
      // lblBpm
      //
      this.lblBpm.AutoSize = true;
      this.lblBpm.Location = new System.Drawing.Point(610, 30);
      this.lblBpm.Name = "lblBpm";
      this.lblBpm.Size = new System.Drawing.Size(38, 15);
      this.lblBpm.TabIndex = 4;
      this.lblBpm.Text = "BPM:";
      //
      // txtBpm
      //
      this.txtBpm.Location = new System.Drawing.Point(654, 27);
      this.txtBpm.Name = "txtBpm";
      this.txtBpm.Size = new System.Drawing.Size(70, 23);
      this.txtBpm.TabIndex = 5;
      //
      // lblDuracion
      //
      this.lblDuracion.AutoSize = true;
      this.lblDuracion.Location = new System.Drawing.Point(745, 30);
      this.lblDuracion.Name = "lblDuracion";
      this.lblDuracion.Size = new System.Drawing.Size(112, 15);
      this.lblDuracion.TabIndex = 6;
      this.lblDuracion.Text = "Duración (segundos):";
      //
      // txtDuracion
      //
      this.txtDuracion.Location = new System.Drawing.Point(863, 27);
      this.txtDuracion.Name = "txtDuracion";
      this.txtDuracion.Size = new System.Drawing.Size(80, 23);
      this.txtDuracion.TabIndex = 7;
      //
      // lblModo
      //
      this.lblModo.AutoSize = true;
      this.lblModo.Location = new System.Drawing.Point(18, 78);
      this.lblModo.Name = "lblModo";
      this.lblModo.Size = new System.Drawing.Size(45, 15);
      this.lblModo.TabIndex = 8;
      this.lblModo.Text = "Modo:";
      //
      // rdoListaPropia
      //
      this.rdoListaPropia.AutoSize = true;
      this.rdoListaPropia.Checked = true;
      this.rdoListaPropia.Location = new System.Drawing.Point(90, 76);
      this.rdoListaPropia.Name = "rdoListaPropia";
      this.rdoListaPropia.Size = new System.Drawing.Size(213, 19);
      this.rdoListaPropia.TabIndex = 9;
      this.rdoListaPropia.TabStop = true;
      this.rdoListaPropia.Text = "Lista Simple Propia (Nodos)";
      this.rdoListaPropia.UseVisualStyleBackColor = true;
      this.rdoListaPropia.CheckedChanged += new System.EventHandler(this.rdoModo_CheckedChanged);
      //
      // rdoLinkedList
      //
      this.rdoLinkedList.AutoSize = true;
      this.rdoLinkedList.Location = new System.Drawing.Point(330, 76);
      this.rdoLinkedList.Name = "rdoLinkedList";
      this.rdoLinkedList.Size = new System.Drawing.Size(150, 19);
      this.rdoLinkedList.TabIndex = 10;
      this.rdoLinkedList.Text = ".NET LinkedList<T>";
      this.rdoLinkedList.UseVisualStyleBackColor = true;
      this.rdoLinkedList.CheckedChanged += new System.EventHandler(this.rdoModo_CheckedChanged);
      //
      // rdoListNet
      //
      this.rdoListNet.AutoSize = true;
      this.rdoListNet.Location = new System.Drawing.Point(510, 76);
      this.rdoListNet.Name = "rdoListNet";
      this.rdoListNet.Size = new System.Drawing.Size(108, 19);
      this.rdoListNet.TabIndex = 11;
      this.rdoListNet.Text = ".NET List<T>";
      this.rdoListNet.UseVisualStyleBackColor = true;
      this.rdoListNet.CheckedChanged += new System.EventHandler(this.rdoModo_CheckedChanged);
      //
      // lblArchivo
      //
      this.lblArchivo.AutoSize = true;
      this.lblArchivo.Location = new System.Drawing.Point(18, 121);
      this.lblArchivo.Name = "lblArchivo";
      this.lblArchivo.Size = new System.Drawing.Size(105, 15);
      this.lblArchivo.TabIndex = 12;
      this.lblArchivo.Text = "Archivo de audio:";
      //
      // txtRutaArchivo
      //
      this.txtRutaArchivo.Location = new System.Drawing.Point(140, 118);
      this.txtRutaArchivo.Name = "txtRutaArchivo";
      this.txtRutaArchivo.ReadOnly = true;
      this.txtRutaArchivo.Size = new System.Drawing.Size(680, 23);
      this.txtRutaArchivo.TabIndex = 13;
      this.txtRutaArchivo.TabStop = false;
      //
      // btnExaminar
      //
      this.btnExaminar.Location = new System.Drawing.Point(830, 117);
      this.btnExaminar.Name = "btnExaminar";
      this.btnExaminar.Size = new System.Drawing.Size(130, 25);
      this.btnExaminar.TabIndex = 14;
      this.btnExaminar.Text = "📁 Examinar...";
      this.btnExaminar.UseVisualStyleBackColor = true;
      this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
      //
      // lblEstadoDeteccion
      //
      this.lblEstadoDeteccion.AutoSize = false;
      this.lblEstadoDeteccion.ForeColor = System.Drawing.SystemColors.GrayText;
      this.lblEstadoDeteccion.Location = new System.Drawing.Point(140, 150);
      this.lblEstadoDeteccion.Name = "lblEstadoDeteccion";
      this.lblEstadoDeteccion.Size = new System.Drawing.Size(1000, 30);
      this.lblEstadoDeteccion.TabIndex = 15;
      this.lblEstadoDeteccion.Text = "Selecciona un archivo de audio: el título, la duración y el BPM se detectan automáticamente.";
      //
      // grpAcciones
      //
      this.grpAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  )));
      this.grpAcciones.Controls.Add(this.btnEliminarPista);
      this.grpAcciones.Controls.Add(this.btnPurgarDuplicados);
      this.grpAcciones.Controls.Add(this.btnOrdenarBpm);
      this.grpAcciones.Controls.Add(this.btnInvertir);
      this.grpAcciones.Controls.Add(this.btnAvanzarPista);
      this.grpAcciones.Controls.Add(this.btnReproducirSiguiente);
      this.grpAcciones.Controls.Add(this.btnEncolarFinal);
      this.grpAcciones.Location = new System.Drawing.Point(12, 212);
      this.grpAcciones.Name = "grpAcciones";
      this.grpAcciones.Size = new System.Drawing.Size(300, 470);
      this.grpAcciones.TabIndex = 1;
      this.grpAcciones.TabStop = false;
      this.grpAcciones.Text = "Acciones de Cola";
      //
      // btnEncolarFinal
      //
      this.btnEncolarFinal.Location = new System.Drawing.Point(15, 30);
      this.btnEncolarFinal.Name = "btnEncolarFinal";
      this.btnEncolarFinal.Size = new System.Drawing.Size(270, 38);
      this.btnEncolarFinal.TabIndex = 0;
      this.btnEncolarFinal.Text = "➕ Encolar al Final";
      this.btnEncolarFinal.UseVisualStyleBackColor = true;
      this.btnEncolarFinal.Click += new System.EventHandler(this.btnEncolarFinal_Click);
      //
      // btnReproducirSiguiente
      //
      this.btnReproducirSiguiente.Location = new System.Drawing.Point(15, 76);
      this.btnReproducirSiguiente.Name = "btnReproducirSiguiente";
      this.btnReproducirSiguiente.Size = new System.Drawing.Size(270, 38);
      this.btnReproducirSiguiente.TabIndex = 1;
      this.btnReproducirSiguiente.Text = "⏭ Reproducir Siguiente";
      this.btnReproducirSiguiente.UseVisualStyleBackColor = true;
      this.btnReproducirSiguiente.Click += new System.EventHandler(this.btnReproducirSiguiente_Click);
      //
      // btnAvanzarPista
      //
      this.btnAvanzarPista.Location = new System.Drawing.Point(15, 122);
      this.btnAvanzarPista.Name = "btnAvanzarPista";
      this.btnAvanzarPista.Size = new System.Drawing.Size(270, 38);
      this.btnAvanzarPista.TabIndex = 2;
      this.btnAvanzarPista.Text = "⏩ Avanzar Pista";
      this.btnAvanzarPista.UseVisualStyleBackColor = true;
      this.btnAvanzarPista.Click += new System.EventHandler(this.btnAvanzarPista_Click);
      //
      // btnInvertir
      //
      this.btnInvertir.Location = new System.Drawing.Point(15, 168);
      this.btnInvertir.Name = "btnInvertir";
      this.btnInvertir.Size = new System.Drawing.Size(270, 38);
      this.btnInvertir.TabIndex = 3;
      this.btnInvertir.Text = "⇅ Invertir Lista (In-Place)";
      this.btnInvertir.UseVisualStyleBackColor = true;
      this.btnInvertir.Click += new System.EventHandler(this.btnInvertir_Click);
      //
      // btnOrdenarBpm
      //
      this.btnOrdenarBpm.Location = new System.Drawing.Point(15, 214);
      this.btnOrdenarBpm.Name = "btnOrdenarBpm";
      this.btnOrdenarBpm.Size = new System.Drawing.Size(270, 38);
      this.btnOrdenarBpm.TabIndex = 4;
      this.btnOrdenarBpm.Text = "⚡ Ordenar por Curva BPM";
      this.btnOrdenarBpm.UseVisualStyleBackColor = true;
      this.btnOrdenarBpm.Click += new System.EventHandler(this.btnOrdenarBpm_Click);
      //
      // btnPurgarDuplicados
      //
      this.btnPurgarDuplicados.Location = new System.Drawing.Point(15, 260);
      this.btnPurgarDuplicados.Name = "btnPurgarDuplicados";
      this.btnPurgarDuplicados.Size = new System.Drawing.Size(270, 38);
      this.btnPurgarDuplicados.TabIndex = 5;
      this.btnPurgarDuplicados.Text = "🧹 Purgar Duplicados";
      this.btnPurgarDuplicados.UseVisualStyleBackColor = true;
      this.btnPurgarDuplicados.Click += new System.EventHandler(this.btnPurgarDuplicados_Click);
      //
      // btnEliminarPista
      //
      this.btnEliminarPista.Location = new System.Drawing.Point(15, 306);
      this.btnEliminarPista.Name = "btnEliminarPista";
      this.btnEliminarPista.Size = new System.Drawing.Size(270, 38);
      this.btnEliminarPista.TabIndex = 6;
      this.btnEliminarPista.Text = "🗑 Eliminar Pista Seleccionada";
      this.btnEliminarPista.UseVisualStyleBackColor = true;
      this.btnEliminarPista.Click += new System.EventHandler(this.btnEliminarPista_Click);
      //
      // grpPlaylist
      //
      this.grpPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.grpPlaylist.Controls.Add(this.dgvPlaylist);
      this.grpPlaylist.Controls.Add(this.lblTiempoTotalPista);
      this.grpPlaylist.Controls.Add(this.trkProgreso);
      this.grpPlaylist.Controls.Add(this.lblTiempoActual);
      this.grpPlaylist.Controls.Add(this.trkVolumen);
      this.grpPlaylist.Controls.Add(this.lblVolumen);
      this.grpPlaylist.Controls.Add(this.btnMute);
      this.grpPlaylist.Controls.Add(this.chkRepetir);
      this.grpPlaylist.Controls.Add(this.btnDetener);
      this.grpPlaylist.Controls.Add(this.btnPausar);
      this.grpPlaylist.Controls.Add(this.btnReproducir);
      this.grpPlaylist.Controls.Add(this.lblSonando);
      this.grpPlaylist.Location = new System.Drawing.Point(324, 212);
      this.grpPlaylist.Name = "grpPlaylist";
      this.grpPlaylist.Size = new System.Drawing.Size(848, 470);
      this.grpPlaylist.TabIndex = 2;
      this.grpPlaylist.TabStop = false;
      this.grpPlaylist.Text = "Playlist en Vivo";
      //
      // lblSonando
      //
      this.lblSonando.AutoSize = true;
      this.lblSonando.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.lblSonando.Location = new System.Drawing.Point(15, 18);
      this.lblSonando.Name = "lblSonando";
      this.lblSonando.Size = new System.Drawing.Size(200, 17);
      this.lblSonando.TabIndex = 0;
      this.lblSonando.Text = "▶ Sonando: (la cola está vacía)";
      //
      // btnReproducir
      //
      this.btnReproducir.Location = new System.Drawing.Point(15, 42);
      this.btnReproducir.Name = "btnReproducir";
      this.btnReproducir.Size = new System.Drawing.Size(100, 30);
      this.btnReproducir.TabIndex = 1;
      this.btnReproducir.Text = "▶ Reproducir";
      this.btnReproducir.UseVisualStyleBackColor = true;
      this.btnReproducir.Click += new System.EventHandler(this.btnReproducir_Click);
      //
      // btnPausar
      //
      this.btnPausar.Location = new System.Drawing.Point(120, 42);
      this.btnPausar.Name = "btnPausar";
      this.btnPausar.Size = new System.Drawing.Size(90, 30);
      this.btnPausar.TabIndex = 2;
      this.btnPausar.Text = "⏸ Pausar";
      this.btnPausar.UseVisualStyleBackColor = true;
      this.btnPausar.Click += new System.EventHandler(this.btnPausar_Click);
      //
      // btnDetener
      //
      this.btnDetener.Location = new System.Drawing.Point(215, 42);
      this.btnDetener.Name = "btnDetener";
      this.btnDetener.Size = new System.Drawing.Size(90, 30);
      this.btnDetener.TabIndex = 3;
      this.btnDetener.Text = "⏹ Detener";
      this.btnDetener.UseVisualStyleBackColor = true;
      this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
      //
      // chkRepetir
      //
      this.chkRepetir.AutoSize = true;
      this.chkRepetir.Location = new System.Drawing.Point(315, 48);
      this.chkRepetir.Name = "chkRepetir";
      this.chkRepetir.Size = new System.Drawing.Size(90, 19);
      this.chkRepetir.TabIndex = 4;
      this.chkRepetir.Text = "🔁 Repetir";
      this.chkRepetir.UseVisualStyleBackColor = true;
      //
      // btnMute
      //
      this.btnMute.Location = new System.Drawing.Point(415, 42);
      this.btnMute.Name = "btnMute";
      this.btnMute.Size = new System.Drawing.Size(45, 30);
      this.btnMute.TabIndex = 5;
      this.btnMute.Text = "🔊";
      this.btnMute.UseVisualStyleBackColor = true;
      this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
      //
      // lblVolumen
      //
      this.lblVolumen.AutoSize = true;
      this.lblVolumen.Location = new System.Drawing.Point(466, 50);
      this.lblVolumen.Name = "lblVolumen";
      this.lblVolumen.Size = new System.Drawing.Size(58, 15);
      this.lblVolumen.TabIndex = 6;
      this.lblVolumen.Text = "Volumen:";
      //
      // trkVolumen
      //
      this.trkVolumen.LargeChange = 20;
      this.trkVolumen.Location = new System.Drawing.Point(526, 39);
      this.trkVolumen.Maximum = 100;
      this.trkVolumen.Name = "trkVolumen";
      this.trkVolumen.Size = new System.Drawing.Size(140, 45);
      this.trkVolumen.TabIndex = 7;
      this.trkVolumen.TickStyle = System.Windows.Forms.TickStyle.None;
      this.trkVolumen.Value = 80;
      this.trkVolumen.ValueChanged += new System.EventHandler(this.trkVolumen_ValueChanged);
      //
      // lblTiempoActual
      //
      this.lblTiempoActual.AutoSize = true;
      this.lblTiempoActual.Location = new System.Drawing.Point(15, 89);
      this.lblTiempoActual.Name = "lblTiempoActual";
      this.lblTiempoActual.Size = new System.Drawing.Size(38, 15);
      this.lblTiempoActual.TabIndex = 8;
      this.lblTiempoActual.Text = "00:00";
      //
      // trkProgreso
      //
      this.trkProgreso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.trkProgreso.LargeChange = 10;
      this.trkProgreso.Location = new System.Drawing.Point(62, 82);
      this.trkProgreso.Maximum = 100;
      this.trkProgreso.Name = "trkProgreso";
      this.trkProgreso.Size = new System.Drawing.Size(610, 45);
      this.trkProgreso.TabIndex = 9;
      this.trkProgreso.TickStyle = System.Windows.Forms.TickStyle.None;
      this.trkProgreso.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trkProgreso_MouseDown);
      this.trkProgreso.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trkProgreso_MouseUp);
      //
      // lblTiempoTotalPista
      //
      this.lblTiempoTotalPista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblTiempoTotalPista.AutoSize = true;
      this.lblTiempoTotalPista.Location = new System.Drawing.Point(678, 89);
      this.lblTiempoTotalPista.Name = "lblTiempoTotalPista";
      this.lblTiempoTotalPista.Size = new System.Drawing.Size(38, 15);
      this.lblTiempoTotalPista.TabIndex = 10;
      this.lblTiempoTotalPista.Text = "00:00";
      //
      // dgvPlaylist
      //
      this.dgvPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvPlaylist.AllowUserToAddRows = false;
      this.dgvPlaylist.AllowUserToDeleteRows = false;
      this.dgvPlaylist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvPlaylist.Location = new System.Drawing.Point(15, 120);
      this.dgvPlaylist.MultiSelect = false;
      this.dgvPlaylist.Name = "dgvPlaylist";
      this.dgvPlaylist.ReadOnly = true;
      this.dgvPlaylist.RowHeadersWidth = 25;
      this.dgvPlaylist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvPlaylist.Size = new System.Drawing.Size(818, 335);
      this.dgvPlaylist.TabIndex = 11;
      //
      // pnlEstado
      //
      this.pnlEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right))));
      this.pnlEstado.Controls.Add(this.lblTiempoTotal);
      this.pnlEstado.Controls.Add(this.lblTotalCola);
      this.pnlEstado.Location = new System.Drawing.Point(12, 692);
      this.pnlEstado.Name = "pnlEstado";
      this.pnlEstado.Size = new System.Drawing.Size(1160, 28);
      this.pnlEstado.TabIndex = 3;
      //
      // lblTotalCola
      //
      this.lblTotalCola.AutoSize = true;
      this.lblTotalCola.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.lblTotalCola.Location = new System.Drawing.Point(3, 6);
      this.lblTotalCola.Name = "lblTotalCola";
      this.lblTotalCola.Size = new System.Drawing.Size(130, 15);
      this.lblTotalCola.TabIndex = 0;
      this.lblTotalCola.Text = "Total en cola: 0 pista(s)";
      //
      // lblTiempoTotal
      //
      this.lblTiempoTotal.AutoSize = true;
      this.lblTiempoTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.lblTiempoTotal.Location = new System.Drawing.Point(300, 6);
      this.lblTiempoTotal.Name = "lblTiempoTotal";
      this.lblTiempoTotal.Size = new System.Drawing.Size(120, 15);
      this.lblTiempoTotal.TabIndex = 1;
      this.lblTiempoTotal.Text = "Tiempo total: 00:00";
      //
      // grpBenchmark
      //
      this.grpBenchmark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right))));
      this.grpBenchmark.Controls.Add(this.lblTelemetriaEnVivo);
      this.grpBenchmark.Controls.Add(this.lblBenchmarkResultado);
      this.grpBenchmark.Controls.Add(this.btnBenchmark);
      this.grpBenchmark.Location = new System.Drawing.Point(12, 730);
      this.grpBenchmark.Name = "grpBenchmark";
      this.grpBenchmark.Size = new System.Drawing.Size(1160, 136);
      this.grpBenchmark.TabIndex = 4;
      this.grpBenchmark.TabStop = false;
      this.grpBenchmark.Text = "Benchmark de Telemetría";
      //
      // lblTelemetriaEnVivo
      //
      this.lblTelemetriaEnVivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblTelemetriaEnVivo.AutoSize = false;
      this.lblTelemetriaEnVivo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.lblTelemetriaEnVivo.ForeColor = System.Drawing.Color.DarkSlateBlue;
      this.lblTelemetriaEnVivo.Location = new System.Drawing.Point(15, 20);
      this.lblTelemetriaEnVivo.Name = "lblTelemetriaEnVivo";
      this.lblTelemetriaEnVivo.Size = new System.Drawing.Size(1130, 36);
      this.lblTelemetriaEnVivo.TabIndex = 2;
      this.lblTelemetriaEnVivo.Text = "⏱ Telemetría en vivo: agrega o mueve pistas reales (Encolar, Avanzar, Invertir, " +
    "Ordenar, Purgar) para medir aquí el tiempo real de cada estructura.";
      //
      // btnBenchmark
      //
      this.btnBenchmark.Location = new System.Drawing.Point(15, 64);
      this.btnBenchmark.Name = "btnBenchmark";
      this.btnBenchmark.Size = new System.Drawing.Size(300, 50);
      this.btnBenchmark.TabIndex = 0;
      this.btnBenchmark.Text = "🚀 Probar 25,000 Inserciones (Estrés Sintético)";
      this.btnBenchmark.UseVisualStyleBackColor = true;
      this.btnBenchmark.Click += new System.EventHandler(this.btnBenchmark_Click);
      //
      // lblBenchmarkResultado
      //
      this.lblBenchmarkResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblBenchmarkResultado.Location = new System.Drawing.Point(330, 58);
      this.lblBenchmarkResultado.Name = "lblBenchmarkResultado";
      this.lblBenchmarkResultado.Size = new System.Drawing.Size(815, 66);
      this.lblBenchmarkResultado.TabIndex = 1;
      this.lblBenchmarkResultado.Text = "Prueba de estrés con datos sintéticos (no usa tus pistas reales): pulsa \"Probar 25,000 Inserciones\" para ver el comportamiento a gran escala.";
      //
      // MainForm
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1184, 878);
      this.Controls.Add(this.grpBenchmark);
      this.Controls.Add(this.pnlEstado);
      this.Controls.Add(this.grpPlaylist);
      this.Controls.Add(this.grpAcciones);
      this.Controls.Add(this.grpRegistro);
      this.MinimumSize = new System.Drawing.Size(1060, 832);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SoundCore Engine v2.0 - DJ Set Controller [TecNM Monclova]";
      this.grpRegistro.ResumeLayout(false);
      this.grpRegistro.PerformLayout();
      this.grpAcciones.ResumeLayout(false);
      this.grpPlaylist.ResumeLayout(false);
      this.grpPlaylist.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkProgreso)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trkVolumen)).EndInit();
      this.pnlEstado.ResumeLayout(false);
      this.pnlEstado.PerformLayout();
      this.grpBenchmark.ResumeLayout(false);
      this.grpBenchmark.PerformLayout();
      this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Timer timerProgreso;
    private System.Windows.Forms.GroupBox grpRegistro;
    private System.Windows.Forms.Label lblTitulo;
    private System.Windows.Forms.TextBox txtTitulo;
    private System.Windows.Forms.Label lblArtista;
    private System.Windows.Forms.TextBox txtArtista;
    private System.Windows.Forms.Label lblBpm;
    private System.Windows.Forms.TextBox txtBpm;
    private System.Windows.Forms.Label lblDuracion;
    private System.Windows.Forms.TextBox txtDuracion;
    private System.Windows.Forms.Label lblModo;
    private System.Windows.Forms.RadioButton rdoListaPropia;
    private System.Windows.Forms.RadioButton rdoLinkedList;
    private System.Windows.Forms.RadioButton rdoListNet;
    private System.Windows.Forms.Label lblArchivo;
    private System.Windows.Forms.TextBox txtRutaArchivo;
    private System.Windows.Forms.Button btnExaminar;
    private System.Windows.Forms.Label lblEstadoDeteccion;
    private System.Windows.Forms.GroupBox grpAcciones;
    private System.Windows.Forms.Button btnEncolarFinal;
    private System.Windows.Forms.Button btnReproducirSiguiente;
    private System.Windows.Forms.Button btnAvanzarPista;
    private System.Windows.Forms.Button btnInvertir;
    private System.Windows.Forms.Button btnOrdenarBpm;
    private System.Windows.Forms.Button btnPurgarDuplicados;
    private System.Windows.Forms.Button btnEliminarPista;
    private System.Windows.Forms.GroupBox grpPlaylist;
    private System.Windows.Forms.Label lblSonando;
    private System.Windows.Forms.Button btnReproducir;
    private System.Windows.Forms.Button btnPausar;
    private System.Windows.Forms.Button btnDetener;
    private System.Windows.Forms.CheckBox chkRepetir;
    private System.Windows.Forms.Button btnMute;
    private System.Windows.Forms.Label lblVolumen;
    private System.Windows.Forms.TrackBar trkVolumen;
    private System.Windows.Forms.Label lblTiempoActual;
    private System.Windows.Forms.TrackBar trkProgreso;
    private System.Windows.Forms.Label lblTiempoTotalPista;
    private System.Windows.Forms.DataGridView dgvPlaylist;
    private System.Windows.Forms.Panel pnlEstado;
    private System.Windows.Forms.Label lblTotalCola;
    private System.Windows.Forms.Label lblTiempoTotal;
    private System.Windows.Forms.GroupBox grpBenchmark;
    private System.Windows.Forms.Button btnBenchmark;
    private System.Windows.Forms.Label lblBenchmarkResultado;
    private System.Windows.Forms.Label lblTelemetriaEnVivo;
  }
}
