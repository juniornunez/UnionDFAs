// Esta clase guarda en memoria todos los automatas y todas las uniones que el usuario ha creado
// mientras el programa esta corriendo, para que las demas pantallas puedan accederlos.
using UnionDFAs.Estructuras;

namespace UnionDFAs.Modelo
{
    public static class Sesion
    {
        public static ListaEnlazada<Automata> Automatas = new ListaEnlazada<Automata>();
        public static ListaEnlazada<UnionGuardada> Uniones = new ListaEnlazada<UnionGuardada>();

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

        public static bool ExisteNombreUnion(string nombre)
        {
            for (int i = 0; i < Uniones.Cantidad; i++)
            {
                if (Uniones.Obtener(i).AutomataResultante.Nombre == nombre)
                    return true;
            }
            return false;
        }
    }
}