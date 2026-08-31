// Esta clase representa una sola transicion del automata: de donde sale, con que simbolo, y a donde llega.
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