// AdaptadorLinkedList.cs
// Envuelve System.Collections.Generic.LinkedList<T> (doblemente enlazada,
// nativa de .NET) bajo el mismo contrato IColaDJ<T> que usa la lista
// propia, para poder compararlas en la GUI bajo condiciones idénticas.

using System;
using System.Collections;
using System.Collections.Generic;

namespace SoundCore.EstructurasPropias
{
  public class AdaptadorLinkedList<T> : IColaDJ<T>
  {
    private readonly LinkedList<T> lista = new();

    public int Count => lista.Count;

    public void AgregarAlFinal(T valor) => lista.AddLast(valor);

    public void ReproducirSiguiente(T valor)
    {
      if (lista.First == null)
      {
        lista.AddFirst(valor);
      }
      else
      {
        lista.AddAfter(lista.First, valor);
      }
    }

    public T? AvanzarPista()
    {
      if (lista.First == null)
      {
        return default;
      }
      T valor = lista.First.Value;
      lista.RemoveFirst();
      return valor;
    }

    public void Invertir()
    {
      // LinkedList<T> no expone una inversión nativa in-place; se
      // reconstruyen los enlaces recorriendo la lista una sola vez.
      List<T> valores = new List<T>(lista);
      lista.Clear();
      for (int i = valores.Count - 1; i >= 0; i--)
      {
        lista.AddLast(valores[i]);
      }
    }

    public void InsertarOrdenado(T valor, Comparison<T> comparador)
    {
      var nodo = lista.First;
      while (nodo != null && comparador(nodo.Value, valor) <= 0)
      {
        nodo = nodo.Next;
      }

      if (nodo == null)
      {
        lista.AddLast(valor);
      }
      else
      {
        lista.AddBefore(nodo, valor);
      }
    }

    public void DepurarDuplicados(Func<T, T, bool> sonIguales)
    {
      var actual = lista.First;
      while (actual != null)
      {
        var runner = actual.Next;
        while (runner != null)
        {
          var siguienteRunner = runner.Next;
          if (sonIguales(actual.Value, runner.Value))
          {
            lista.Remove(runner);
          }
          runner = siguienteRunner;
        }
        actual = actual.Next;
      }
    }

    public IEnumerator<T> GetEnumerator() => lista.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
