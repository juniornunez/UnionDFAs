// Esta clase recorre una cadena de simbolos dentro de un automata, paso por paso, siguiendo las
// transiciones, y al final dice si la cadena fue aceptada o rechazada segun donde termino.
using UnionDFAs.Modelo;

namespace UnionDFAs.Logica
{
    public class EvaluadorCadenas
    {
        public ResultadoEvaluacion Evaluar(Automata automata, string cadena)
        {
            ResultadoEvaluacion resultado = new ResultadoEvaluacion();

            for (int i = 0; i < cadena.Length; i++)
            {
                string simbolo = cadena[i].ToString();
                if (!automata.Alfabeto.Existe(simbolo))
                {
                    resultado.CadenaValida = false;
                    resultado.MensajeError = "El simbolo '" + simbolo + "' no pertenece al alfabeto de " + automata.Nombre;
                    return resultado;
                }
            }

            string estadoActual = automata.EstadoInicial;
            resultado.SecuenciaEstados.Agregar(estadoActual);

            for (int i = 0; i < cadena.Length; i++)
            {
                string simbolo = cadena[i].ToString();
                string siguienteEstado = automata.BuscarDestino(estadoActual, simbolo);

                if (siguienteEstado == null)
                {
                    resultado.CadenaValida = false;
                    resultado.MensajeError = "No existe transicion desde '" + estadoActual + "' con el simbolo '" + simbolo + "'";
                    return resultado;
                }

                estadoActual = siguienteEstado;
                resultado.SecuenciaEstados.Agregar(estadoActual);
            }

            resultado.Aceptada = automata.EsFinal(estadoActual);
            return resultado;
        }
    }
}