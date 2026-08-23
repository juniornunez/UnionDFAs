namespace UnionDFA.Estructuras
{
   
    public class Nodo<T>
    {
        public T Dato;
        public Nodo<T> Siguiente;

        public Nodo(T dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    public class ListaEnlazada<T>
    {
        private Nodo<T> cabeza;
        private int cantidad;

        public int Cantidad => cantidad;

        public ListaEnlazada()
        {
            cabeza = null;
            cantidad = 0;
        }

        public void Agregar(T dato)
        {
            Nodo<T> nuevo = new Nodo<T>(dato);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<T> actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            cantidad++;
        }

       
        public T Obtener(int posicion)
        {
            if (posicion < 0 || posicion >= cantidad)
                throw new IndexOutOfRangeException("Posicion fuera de rango");

            Nodo<T> actual = cabeza;
            int i = 0;
            while (i < posicion)
            {
                actual = actual.Siguiente;
                i++;
            }
            return actual.Dato;
        }
    }
}