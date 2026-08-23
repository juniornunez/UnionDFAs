namespace UnionDFAs.Modelo
{
    public class Transicion
    {
        public string EstadoOrigen;
        public string Simbolo;
        public string EstadoDestino;

        public Transicion(string estadoOrigen, string simbolo, string estadoDestino)
        {
            EstadoOrigen = estadoOrigen;
            Simbolo = simbolo;
            EstadoDestino = estadoDestino;
        }
    }
}