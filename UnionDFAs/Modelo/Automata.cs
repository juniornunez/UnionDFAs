using UnionDFA.Estructuras;

namespace UnionDFA.Modelo
{
    public class Automata
    {
        public string Nombre;
        public ListaEnlazada<string> Estados;
        public ListaEnlazada<string> Alfabeto;
        public string EstadoInicial;
        public ListaEnlazada<string> EstadosFinales;
        public ListaEnlazada<Transicion> Transiciones;

        public Automata(string nombre)
        {
            Nombre = nombre;
            Estados = new ListaEnlazada<string>();
            Alfabeto = new ListaEnlazada<string>();
            EstadoInicial = null;
            EstadosFinales = new ListaEnlazada<string>();
            Transiciones = new ListaEnlazada<Transicion>();
        }

        public string BuscarDestino(string estadoOrigen, string simbolo)
        {
            for (int i = 0; i < Transiciones.Cantidad; i++)
            {
                Transicion t = Transiciones.Obtener(i);
                if (t.EstadoOrigen == estadoOrigen && t.Simbolo == simbolo)
                    return t.EstadoDestino;
            }
            return null;
        }

        public bool EsFinal(string estado)
        {
            return EstadosFinales.Existe(estado);
        }
    }
}