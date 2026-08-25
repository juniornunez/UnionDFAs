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