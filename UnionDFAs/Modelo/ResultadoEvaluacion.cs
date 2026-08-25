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