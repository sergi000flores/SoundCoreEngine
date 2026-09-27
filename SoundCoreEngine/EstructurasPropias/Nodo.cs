// Nodo.cs
// Bloque constructivo básico de la lista enlazada simple: guarda un valor
// genérico y una referencia (puntero) al siguiente nodo de la cadena.

using System;

namespace SoundCore.EstructurasPropias
{
  public class Nodo<T>
  {
    public T Valor { get; set; }
    public Nodo<T>? Siguiente { get; set; }

    public Nodo(T valor)
    {
      Valor = valor;
      Siguiente = null;
    }
  }
}
