// Esta clase guarda el resultado de probar una cadena en un automata: si fue aceptada o no,
// si la cadena era valida, y la secuencia de estados por los que paso, para mostrar la trazabilidad.
using UnionDFAs.Estructuras;

namespace UnionDFAs.Modelo
{
    public class ResultadoEvaluacion
    {
        public bool Aceptada;
        public bool CadenaValida;
        public string MensajeError;
        public ListaEnlazada<string> SecuenciaEstados;

        public ResultadoEvaluacion()
        {
            Aceptada = false;
            CadenaValida = true;
            MensajeError = "";
            SecuenciaEstados = new ListaEnlazada<string>();
        }
    }
}