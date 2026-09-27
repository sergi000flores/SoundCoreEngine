// IColaDJ.cs
// Contrato común que exponen las tres colecciones que la GUI compara en
// paralelo: la lista propia basada en nodos, System.Collections.Generic
// .LinkedList<T> y System.Collections.Generic.List<T>. Gracias a esta
// interfaz, MainForm.cs no necesita saber cuál de las tres estructuras
// está usando: solo invoca los mismos 6 métodos sobre IColaDJ<Pista>.

using System;
using System.Collections.Generic;

namespace SoundCore.EstructurasPropias
{
  public interface IColaDJ<T> : IEnumerable<T>
  {
    int Count { get; }

    // 1. Encola una pista al final de la sesión de música.
    void AgregarAlFinal(T valor);

    // 2. Inserta inmediatamente después de la cabeza ("Up Next" VIP).
    void ReproducirSiguiente(T valor);

    // 3. Desencola la pista que está sonando (la cabeza) y la retorna.
    T? AvanzarPista();

    // 4. Invierte el setlist completo.
    void Invertir();

    // 5. Inserta manteniendo el orden dado por el comparador (curva de BPM).
    void InsertarOrdenado(T valor, Comparison<T> comparador);

    // 6. Elimina duplicados según el criterio de igualdad recibido.
    void DepurarDuplicados(Func<T, T, bool> sonIguales);
  }
}
