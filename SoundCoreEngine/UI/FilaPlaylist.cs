// FilaPlaylist.cs
// Objeto de presentación (no forma parte del dominio) usado únicamente
// para proyectar cada Pista de la estructura activa hacia una fila del
// DataGridView, incluyendo una columna "Puntero" que simula la dirección
// de memoria del nodo y hacia dónde apunta (Next / Null), tal como se
// pide en el mockup de la GUI.

namespace SoundCore.UI
{
  public class FilaPlaylist
  {
    public int Id { get; set; }
    public int Posicion { get; set; }
    public string Titulo { get; set; } = "";
    public string Artista { get; set; } = "";
    public int Bpm { get; set; }
    public string Duracion { get; set; } = "";
    public string Puntero { get; set; } = "";
    public string Archivo { get; set; } = "";
  }
}
