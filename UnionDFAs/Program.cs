// Este es el punto de entrada del programa. Antes de abrir la pantalla principal, carga los
// automatas y las uniones que ya estaban guardados en el archivo.
using UnionDFAs.Logica;

namespace UnionDFAs
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            RepositorioArchivos.Cargar();
            Application.Run(new Form1());
        }
    }
}