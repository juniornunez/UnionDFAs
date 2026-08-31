// Esta clase representa un par de estados combinados, uno del automata 1 y uno del automata 2,
// que se usa para armar los estados nuevos cuando se hace la union de dos automatas.
namespace UnionDFAs.Modelo
{
    public class ParEstados
    {
        public string EstadoA;
        public string EstadoB;
        public string NombreCombinado;

        public ParEstados(string estadoA, string estadoB)
        {
            EstadoA = estadoA;
            EstadoB = estadoB;
            NombreCombinado = "(" + estadoA + "," + estadoB + ")";
        }
    }
}