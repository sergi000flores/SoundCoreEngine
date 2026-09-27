// AdaptadorList.cs
// Envuelve System.Collections.Generic.List<T> (arreglo dinámico) bajo el
// mismo contrato IColaDJ<T>. Es la "villana" del benchmark: al insertar
// cerca del inicio obliga a mover en bloque todos los elementos
// posteriores (Array.Copy interno), lo que degrada su rendimiento a O(n)
// por inserción frente al O(1) de las listas enlazadas.

using System;
using System.Collections;
using System.Collections.Generic;

namespace SoundCore.EstructurasPropias
{
  public class AdaptadorList<T> : IColaDJ<T>
  {
    private readonly List<T> lista = new();

    public int Count => lista.Count;

    public void AgregarAlFinal(T valor) => lista.Add(valor);

    public void ReproducirSiguiente(T valor)
    {
      // Insertar en la posición 1 desplaza en memoria todos los
      // elementos restantes: aquí es donde List<T> pierde el duelo.
      if (lista.Count == 0)
      {
        lista.Add(valor);
      }
      else
      {
        lista.Insert(1, valor);
      }
    }

    public T? AvanzarPista()
    {
      if (lista.Count == 0)
      {
        return default;
      }
      T valor = lista[0];
      lista.RemoveAt(0);
      return valor;
    }

    public void Invertir() => lista.Reverse();

    public void InsertarOrdenado(T valor, Comparison<T> comparador)
    {
      int indice = 0;
      while (indice < lista.Count && comparador(lista[indice], valor) <= 0)
      {
        indice++;
      }
      lista.Insert(indice, valor);
    }

    public void DepurarDuplicados(Func<T, T, bool> sonIguales)
    {
      for (int i = 0; i < lista.Count; i++)
      {
        for (int j = lista.Count - 1; j > i; j--)
        {
          if (sonIguales(lista[i], lista[j]))
          {
            lista.RemoveAt(j);
          }
        }
      }
    }

    public IEnumerator<T> GetEnumerator() => lista.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
