// FormResultadoBenchmark.cs
// Ventana modal que muestra la tabla de tiempos promedio de inserción por
// tamaño de cola (evidencia empírica) y la conclusión teórica que explica
// el porqué (impacto de Array.Copy en List<T> vs reconexión de punteros
// en las listas enlazadas).

using System.Collections.Generic;
using System.Windows.Forms;

namespace SoundCore.UI
{
  public partial class FormResultadoBenchmark : Form
  {
    public FormResultadoBenchmark(List<FilaBenchmark> filas, string conclusion)
    {
      InitializeComponent();
      ConfigurarColumnas();

      dgvResultados.DataSource = filas;
      lblConclusion.Text = conclusion;
    }

    private void ConfigurarColumnas()
    {
      dgvResultados.AutoGenerateColumns = false;
      dgvResultados.Columns.Clear();

      dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
      {
        DataPropertyName = "TamanoTexto",
        HeaderText = "Tamaño de la cola",
        Width = 160
      });
      dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
      {
        DataPropertyName = "PropiaTexto",
        HeaderText = "Lista Propia (µs/inserción)",
        Width = 190
      });
      dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
      {
        DataPropertyName = "LinkedTexto",
        HeaderText = ".NET LinkedList<T> (µs/inserción)",
        Width = 190
      });
      dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
      {
        DataPropertyName = "ListTexto",
        HeaderText = ".NET List<T> (µs/inserción)",
        Width = 190,
        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
      });
    }

    private void btnCerrar_Click(object sender, System.EventArgs e)
    {
      Close();
    }
  }
}
