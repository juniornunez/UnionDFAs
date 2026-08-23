using UnionDFA.Estructuras;

namespace UnionDFA.Modelo
{
    public class ResultadoValidacion
    {
        public bool EsValido;
        public ListaEnlazada<string> Errores;

        public ResultadoValidacion()
        {
            EsValido = true;
            Errores = new ListaEnlazada<string>();
        }

        public void AgregarError(string mensaje)
        {
            Errores.Agregar(mensaje);
            EsValido = false;
        }
    }
}   