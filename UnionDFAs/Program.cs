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