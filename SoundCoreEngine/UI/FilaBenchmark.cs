// FilaBenchmark.cs
// Fila de la tabla de resultados del benchmark progresivo: para cada
// tamaño de cola (1,000 a 25,000) guarda el tiempo PROMEDIO por inserción
// (en microsegundos) de cada una de las 3 estructuras, calculado sobre el
// tramo que va desde el tamaño anterior hasta este. Esto permite ver el
// crecimiento (o la falta de crecimiento) de ese promedio a medida que la
// cola es más grande, que es justamente la evidencia empírica de O(1) vs O(n).

namespace SoundCore.UI
{
  public class FilaBenchmark
  {
    public int Tamano { get; set; }
    public double PropiaMicroseg { get; set; }
    public double LinkedMicroseg { get; set; }
    public double ListMicroseg { get; set; }

    public string TamanoTexto => Tamano.ToString("N0");
    public string PropiaTexto => PropiaMicroseg.ToString("0.000");
    public string LinkedTexto => LinkedMicroseg.ToString("0.000");
    public string ListTexto => ListMicroseg.ToString("0.000");
  }
}
