using UnionDFAs.Estructuras;

namespace UnionDFAs.Modelo
{
    public static class Sesion
    {
        public static ListaEnlazada<Automata> Automatas = new ListaEnlazada<Automata>();
        public static Automata AutomataUnion;
        public static Automata AutomataOrigenUnion1;
        public static Automata AutomataOrigenUnion2;

        public static string GenerarNombreDisponible()
        {
            int numero = 1;
            bool nombreOcupado = true;
            string nombreCandidato = "";

            while (nombreOcupado)
            {
                nombreCandidato = "Automata" + numero;
                nombreOcupado = false;
                for (int i = 0; i < Automatas.Cantidad; i++)
                {
                    if (Automatas.Obtener(i).Nombre == nombreCandidato)
                    {
                        nombreOcupado = true;
                        break;
                    }
                }
                numero++;
            }
            return nombreCandidato;
        }

        public static bool ExisteNombre(string nombre)
        {
            for (int i = 0; i < Automatas.Cantidad; i++)
            {
                if (Automatas.Obtener(i).Nombre == nombre)
                    return true;
            }
            return false;
        }
    }
}