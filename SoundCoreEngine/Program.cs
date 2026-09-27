// Program.cs
// Reto Unidad 2 - SoundCore Engine GUI - TecNM Monclova
// Punto de entrada de la aplicación Windows Forms.

using System;
using SoundCore.UI;

namespace SoundCore
{
  internal static class Program
  {
    [STAThread]
    static void Main()
    {
      ApplicationConfiguration.Initialize();
      Application.Run(new MainForm());
    }
  }
}
