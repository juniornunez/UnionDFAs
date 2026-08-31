// Esta clase guarda el resultado de validar un automata: si quedo valido o no, y la lista de errores
// que se encontraron, para poder mostrarlos despues en pantalla.
using UnionDFAs.Estructuras;

namespace UnionDFAs.Modelo
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