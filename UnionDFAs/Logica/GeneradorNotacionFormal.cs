using System.Text;
using UnionDFAs.Estructuras;
using UnionDFAs.Modelo;

namespace UnionDFAs.Logica
{
    public static class GeneradorNotacionFormal
    {
        private const string SimboloDeltaGorrito = "\u03B4\u0302";
        private const string SimboloDelta = "\u03B4";
        private const string SimboloEpsilon = "\u03B5";

        public static string Generar(string estadoInicial, string cadena, ListaEnlazada<string> secuenciaEstados)
        {
            StringBuilder constructor = new StringBuilder();

            constructor.AppendLine(SimboloDeltaGorrito + "(" + estadoInicial + ", " + SimboloEpsilon + ") = " + estadoInicial + "  (estado inicial)");
            constructor.AppendLine();

            for (int i = 1; i <= cadena.Length; i++)
            {
                string prefijoActual = cadena.Substring(0, i);
                string prefijoAnterior = cadena.Substring(0, i - 1);
                char simboloActual = cadena[i - 1];

                string estadoAnterior = secuenciaEstados.Obtener(i - 1);
                string estadoNuevo = secuenciaEstados.Obtener(i);

                string prefijoAnteriorMostrado = prefijoAnterior == "" ? SimboloEpsilon : prefijoAnterior;

                constructor.AppendLine(SimboloDeltaGorrito + "(" + estadoInicial + ", " + prefijoActual + ") = " + SimboloDelta + "(" + SimboloDeltaGorrito + "(" + estadoInicial + ", " + prefijoAnteriorMostrado + "), " + simboloActual + ")");
                constructor.AppendLine("= " + SimboloDelta + "(" + estadoAnterior + ", " + simboloActual + ")");
                constructor.AppendLine("= " + estadoNuevo);
                constructor.AppendLine();
            }

            return constructor.ToString();
        }
    }
}