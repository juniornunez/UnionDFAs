// Esta clase guarda una union ya generada: de que dos automatas vino y cual es el automata resultante,
// para poder listarla despues y volver a usarla en la prueba de cadenas.
namespace UnionDFAs.Modelo
{
    public class UnionGuardada
    {
        public string NombreOrigen1;
        public string NombreOrigen2;
        public Automata AutomataResultante;
    }
}