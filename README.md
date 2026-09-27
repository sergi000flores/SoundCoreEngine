# SoundCore Engine GUI — DJ Queue & Transition Manager

Reto Unidad 2 — Estructura de Datos — TecNM Monclova
C# 14-ready / .NET 8.0 (WinForms) · `SimpleLinkedList` propia vs `LinkedList<T>` vs `List<T>`

## 1. Cómo abrir el proyecto

1. Abre `SoundCoreEngine.slnx` con Visual Studio 2022 (17.10+) o `dotnet build` desde consola.
2. El proyecto de arranque es `SoundCoreEngine/SoundCoreEngine.csproj` (WinForms, `net8.0-windows`).
3. Ejecuta con `F5` o `dotnet run --project SoundCoreEngine/SoundCoreEngine.csproj`.

## 2. Arquitectura en capas

```
SoundCoreEngine/
├── Modelos/
│   └── Pista.cs                 // record inmutable con metadatos de la pista
├── EstructurasPropias/
│   ├── Nodo.cs                  // Nodo<T> genérico autoreferenciado
│   ├── IColaDJ.cs               // contrato común de las 3 estructuras
│   ├── ListaSimpleEnlazada.cs   // los 6 algoritmos implementados desde cero
│   ├── AdaptadorLinkedList.cs   // envuelve System.Collections.Generic.LinkedList<T>
│   └── AdaptadorList.cs         // envuelve System.Collections.Generic.List<T>
└── UI/
    ├── MainForm.cs                       // lógica y sincronización de la GUI
    ├── MainForm.Designer.cs              // layout de controles WinForms
    ├── FilaPlaylist.cs                   // modelo de presentación para el DataGridView
    ├── FilaBenchmark.cs                  // modelo de fila para la tabla de resultados del benchmark
    ├── FormResultadoBenchmark.cs         // diálogo con la tabla de crecimiento y la conclusión
    └── FormResultadoBenchmark.Designer.cs
```

`ListaSimpleEnlazada<T>` es 100% independiente de Windows Forms y funciona con
cualquier tipo `T`, no solo `Pista`. Implementa `IEnumerable<T>` con
`yield return` para poder enlazarse directamente al `DataGridView`.

## 3. Los 6 algoritmos obligatorios (`ListaSimpleEnlazada<T>`)

| Método | Complejidad | Acción |
|---|---|---|
| `AgregarAlFinal(T)` | O(n) | Encola una pista al final de la sesión. |
| `ReproducirSiguiente(T)` | O(1) | Inserta justo después de la cabeza (Up Next VIP). |
| `AvanzarPista()` | O(1) | Desencola la pista sonando (la cabeza). |
| `Invertir()` | O(n) tiempo / O(1) espacio | Invierte el setlist **estrictamente in-place** (técnica de 3 punteros: `previo`, `actual`, `siguiente`). |
| `InsertarOrdenado(T, Comparison<T>)` | O(n) | Inserta manteniendo el orden (curva de BPM). |
| `DepurarDuplicados(Func<T,T,bool>)` | O(n²) | Elimina repetidos sin `HashSet` ni arreglos auxiliares. |

`Invertir()` no crea listas temporales ni intercambia solo los valores: reorienta
los punteros `Siguiente` de cada nodo en tiempo real (0 bytes de memoria auxiliar).

## 4. La GUI (Windows Forms)

- **Registro**: Título, Artista, BPM, Duración (segundos) y **Archivo de
  audio**. Al pulsar "📁 Examinar..." y elegir el archivo, la app **detecta
  automáticamente**:
  - **Título y Artista**: de las etiquetas ID3/ID3v2/APE embebidas en el
    propio archivo (vía **TagLibSharp**); si no existen, usa el nombre del
    archivo.
  - **Duración**: leída de los metadatos del archivo (`TagLib`).
  - **BPM**: **estimado analizando la señal de audio real** (ver algoritmo
    abajo). Los tres campos quedan editables por si se quieren corregir.
- **Modo**: radio buttons para elegir cuál de las 3 estructuras se está operando
  (Lista Simple Propia, `LinkedList<T>`, `List<T>`). Cada una conserva su propio
  estado en paralelo, así se puede repetir la misma secuencia de acciones en
  las tres y comparar el resultado y el comportamiento.
- **Acciones de cola**: Encolar al Final, Reproducir Siguiente, Avanzar Pista,
  Invertir Lista (In-Place), Ordenar por Curva BPM, Purgar Duplicados.
- **Playlist en vivo**: `DataGridView` con posición, título, artista, BPM,
  duración, columna "Nodo -> Apunta a" (simula el puntero de memoria) y el
  nombre del archivo de audio.
- **Reproductor**, sobre la pista en la cabeza ("Sonando"):
  - ▶ Reproducir / ⏸ Pausar / ⏹ Detener.
  - **Barra de progreso (seek)**: arrástrala para saltar a cualquier punto
    de la pista; muestra tiempo transcurrido y tiempo total.
  - **🔊 Mute** y control deslizante de **Volumen**.
  - **🔁 Repetir**: si está activo, al terminar la pista se repite ella
    misma; si no, se dispara la **transición automática** hacia la
    siguiente (equivalente a "Avanzar Pista"), simulando un set de DJ
    continuo.
- **Benchmark**: botón "Probar 25,000 Inserciones" que mide con `Stopwatch` el
  tiempo real de 25,000 inserciones intermedias (`ReproducirSiguiente`) en las
  tres estructuras y muestra el resultado y su justificación Big-O.

### Motor de audio y detección automática

- **Reproducción**: paquete NuGet **[NAudio](https://github.com/naudio/NAudio)**
  (`WaveOutEvent` + `AudioFileReader`). No usa Windows Media Player ni ninguna
  API COM del sistema; es 100% administrado y soporta mp3/wav/wma/aac/flac.
- **Metadatos (Título/Artista/Duración)**: paquete NuGet
  **[TagLibSharp](https://github.com/mono/taglib-sharp)**, que lee las
  etiquetas embebidas del propio archivo sin ningún servicio en línea.
- **BPM (estimado desde el audio, sin servicios externos)**: algoritmo de
  *"energía + autocorrelación"* implementado en
  `MainForm.EstimarBpmDelAudio`:
  1. Se decodifican los primeros 60 segundos del archivo a PCM mono con NAudio.
  2. Se calcula la energía de la señal en ventanas de ~10 ms (envolvente).
  3. Se obtiene el *onset* (subida súbita de energía = posible golpe de
     batería) como la derivada positiva de esa envolvente.
  4. Se autocorrelaciona la señal de *onsets* contra periodos candidatos de
     60 a 200 BPM; el periodo con mayor autocorrelación es el BPM estimado.

  Es una estimación (no un detector profesional de tempo), por lo que puede
  variar en pistas sin un pulso rítmico claro; el campo BPM queda siempre
  editable para ajustarlo manualmente.

Al abrir el proyecto por primera vez, Visual Studio/`dotnet restore`
descargará automáticamente NAudio y TagLibSharp (ver `.csproj`).

## 5. Benchmark y análisis de complejidad (medición real, no solo teoría)

Hay **dos mediciones distintas**, ambas con `Stopwatch`, para que la
diferencia O(1) vs O(n) quede demostrada con datos y no solo enunciada:

- **Telemetría en vivo (pistas reales)**: cada acción de cola que el
  usuario dispara desde la GUI (Encolar al Final, Reproducir Siguiente,
  Avanzar Pista, Invertir, Ordenar por BPM, Purgar Duplicados) se ejecuta
  **en espejo sobre las 3 estructuras** (Lista Propia, `LinkedList<T>`,
  `List<T>`) con las **mismas pistas reales** que el usuario agregó, y cada
  una se cronometra por separado. El resultado (en microsegundos, por cada
  estructura) se muestra al instante arriba del botón de benchmark
  (`lblTelemetriaEnVivo`). Por eso el "Modo" ya no reinicia nada: las 3
  estructuras siempre tienen el mismo contenido real; el radio button solo
  decide cuál se ve/reproduce.
- **Benchmark de estrés progresivo (sintético)**: el botón "🚀 Probar
  25,000 Inserciones" no da un solo número final; mide el tiempo
  **promedio por inserción** en 6 tamaños de cola crecientes (1,000; 5,000;
  10,000; 15,000; 20,000; 25,000), cada uno sobre el tramo correspondiente
  (no acumulado). Antes de medir se ejecuta un **warm-up** (una corrida
  corta descartable) para que el JIT ya haya compilado los métodos y la
  primera medición real no salga artificialmente lenta — así se evitan los
  "datos incongruentes" entre corridas. El resultado abre una ventana
  (`FormResultadoBenchmark`) con una tabla que deja ver el **crecimiento**
  del promedio en `List<T>` (evidencia empírica de O(n), por el
  `Array.Copy` interno de `List<T>.Insert`) frente al promedio
  prácticamente **plano** de la Lista Propia y `LinkedList<T>` (evidencia
  de O(1)), junto con la explicación teórica de por qué ocurre.

## 6. Casos de prueba cubiertos

- **CP-01** Inserción final → `AgregarAlFinal`.
- **CP-02** Up Next VIP → `ReproducirSiguiente` (posición 2, detrás de la cabeza).
- **CP-03** Avanzar pista → `AvanzarPista` saca la cabeza y refresca el grid.
- **CP-04** Inversión in-place → `Invertir`.
- **CP-05** Curva BPM → `InsertarOrdenado` reordena ascendente por BPM.
- **CP-06** Purgar duplicados → `DepurarDuplicados` compara por título.
- **CP-07** Manejo de errores → avanzar con la cola vacía muestra un
  `MessageBox` informativo sin colapsar la aplicación.

## 7. Autores

// Nombres, Números de Control TecNM y Fecha
