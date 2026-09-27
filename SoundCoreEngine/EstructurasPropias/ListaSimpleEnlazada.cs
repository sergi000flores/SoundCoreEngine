// ListaSimpleEnlazada.cs
// Lista enlazada simple genérica implementada desde cero (sin apoyarse en
// List<T>, LinkedList<T> ni arrays auxiliares). Es 100% independiente de
// Windows Forms: no conoce Pista, Node ni ningún control de la GUI, por lo
// que puede reutilizarse con cualquier tipo T (Desacoplamiento).
//
// Implementa IEnumerable<T> mediante yield return para poder enlazarse
// directamente a un DataGridView (Data Binding).

using System;
using System.Collections;
using System.Collections.Generic;

namespace SoundCore.EstructurasPropias
{
  public class ListaSimpleEnlazada<T> : IColaDJ<T>
  {
    public Nodo<T>? Cabeza { get; private set; }

    private int contador;
    public int Count => contador;

    public ListaSimpleEnlazada()
    {
      Cabeza = null;
      contador = 0;
    }

    // ---------------------------------------------------------------
    // 1. AgregarAlFinal(T) -> O(n) recorriendo hasta la cola.
    // ---------------------------------------------------------------
    public void AgregarAlFinal(T valor)
    {
      Nodo<T> nuevo = new Nodo<T>(valor);

      if (Cabeza == null)
      {
        Cabeza = nuevo;
        contador++;
        return;
      }

      Nodo<T> actual = Cabeza;
      while (actual.Siguiente != null)
      {
        actual = actual.Siguiente;
      }
      actual.Siguiente = nuevo;
      contador++;
    }

    // ---------------------------------------------------------------
    // 2. ReproducirSiguiente(T) -> O(1). Solo reconecta 2 referencias:
    //    la cabeza pasa a apuntar al nuevo nodo y el nuevo nodo apunta
    //    a lo que antes seguía a la cabeza.
    // ---------------------------------------------------------------
    public void ReproducirSiguiente(T valor)
    {
      Nodo<T> nuevo = new Nodo<T>(valor);

      if (Cabeza == null)
      {
        Cabeza = nuevo;
        contador++;
        return;
      }

      nuevo.Siguiente = Cabeza.Siguiente;
      Cabeza.Siguiente = nuevo;
      contador++;
    }

    // ---------------------------------------------------------------
    // 3. AvanzarPista() -> O(1). Desencola la cabeza (pista sonando)
    //    y la cabeza se desplaza al siguiente nodo.
    // ---------------------------------------------------------------
    public T? AvanzarPista()
    {
      if (Cabeza == null)
      {
        return default;
      }

      T valor = Cabeza.Valor;
      Cabeza = Cabeza.Siguiente;
      contador--;
      return valor;
    }

    // ---------------------------------------------------------------
    // 4. Invertir() -> Técnica clásica de 3 punteros. Estrictamente
    //    in-place: O(n) en tiempo, O(1) en espacio auxiliar. No se
    //    crean listas temporales ni se intercambian solo los valores;
    //    se reorientan los punteros "Siguiente" de cada nodo.
    // ---------------------------------------------------------------
    public void Invertir()
    {
      Nodo<T>? previo = null;
      Nodo<T>? actual = Cabeza;
      Nodo<T>? siguiente = null;

      while (actual != null)
      {
        siguiente = actual.Siguiente;   // 1. Guardar el resto de la lista
        actual.Siguiente = previo;      // 2. Invertir la flecha del nodo actual
        previo = actual;                // 3. Desplazar "previo" hacia adelante
        actual = siguiente;             // 4. Desplazar "actual" hacia adelante
      }

      Cabeza = previo;                  // 5. La nueva cabeza es el último "previo"
    }

    // ---------------------------------------------------------------
    // 5. InsertarOrdenado(T, Comparison<T>) -> O(n). Recorre la lista
    //    buscando la primera posición donde el nuevo valor ya no es
    //    mayor que el siguiente nodo (orden ascendente por comparador,
    //    p. ej. por BPM para la curva de energía armónica).
    // ---------------------------------------------------------------
    public void InsertarOrdenado(T valor, Comparison<T> comparador)
    {
      Nodo<T> nuevo = new Nodo<T>(valor);

      if (Cabeza == null || comparador(valor, Cabeza.Valor) < 0)
      {
        nuevo.Siguiente = Cabeza;
        Cabeza = nuevo;
        contador++;
        return;
      }

      Nodo<T> actual = Cabeza;
      while (actual.Siguiente != null && comparador(actual.Siguiente.Valor, valor) <= 0)
      {
        actual = actual.Siguiente;
      }

      nuevo.Siguiente = actual.Siguiente;
      actual.Siguiente = nuevo;
      contador++;
    }

    // ---------------------------------------------------------------
    // 6. DepurarDuplicados(Func<T,T,bool>) -> O(n²). Compara cada nodo
    //    contra todos los que le siguen (doble puntero: "actual" y
    //    "runner") sin usar HashSets ni arreglos auxiliares. Mantiene
    //    siempre la primera aparición y desconecta las repetidas.
    // ---------------------------------------------------------------
    public void DepurarDuplicados(Func<T, T, bool> sonIguales)
    {
      if (Cabeza == null)
      {
        return;
      }

      Nodo<T>? actual = Cabeza;
      while (actual != null)
      {
        Nodo<T> runner = actual;
        while (runner.Siguiente != null)
        {
          if (sonIguales(actual.Valor, runner.Siguiente.Valor))
          {
            runner.Siguiente = runner.Siguiente.Siguiente; // se salta el duplicado
            contador--;
          }
          else
          {
            runner = runner.Siguiente;
          }
        }
        actual = actual.Siguiente;
      }
    }

    // Recorrido completo, usado por ToString() y utilidades de depuración.
    public override string ToString()
    {
      string resultado = "";
      Nodo<T>? actual = Cabeza;
      while (actual != null)
      {
        resultado += actual.Valor?.ToString() + "\n";
        actual = actual.Siguiente;
      }
      return resultado;
    }

    // Habilita foreach y el enlace directo a DataGridView.DataSource.
    public IEnumerator<T> GetEnumerator()
    {
      Nodo<T>? actual = Cabeza;
      while (actual != null)
      {
        yield return actual.Valor;
        actual = actual.Siguiente;
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
