// Pista.cs
// Modelo de datos inmutable que representa una pista musical dentro del
// setlist del DJ. Se implementa como record por su naturaleza de valor
// (dos pistas son "iguales" si todos sus datos coinciden).

using System;

namespace SoundCore.Modelos
{
  public record Pista(
      int Id,
      string Titulo,
      string Artista,
      int Bpm,
      int DuracionSegundos,
      string RutaArchivo)
  {
    // Representación amigable usada en logs y en la columna "Resumen" del grid.
    public override string ToString() =>
        $"[{Id:D3}] {Titulo} - {Artista} | {Bpm} BPM";

    // Duración formateada mm:ss para mostrarse en la interfaz.
    public string DuracionFormateada =>
        TimeSpan.FromSeconds(Math.Max(0, DuracionSegundos)).ToString(@"mm\:ss");
  }
}
