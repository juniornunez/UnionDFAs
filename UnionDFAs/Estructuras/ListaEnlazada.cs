namespace UnionDFAs.Estructuras
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

        public bool Existe(T dato)
        {
            Nodo<T> actual = cabeza;
            while (actual != null)
            {
                if (Comparar(actual.Dato, dato))
                    return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        public int ObtenerPosicion(T dato)
        {
            Nodo<T> actual = cabeza;
            int i = 0;
            while (actual != null)
            {
                if (Comparar(actual.Dato, dato))
                    return i;
                actual = actual.Siguiente;
                i++;
            }
            return -1;
        }

        public bool EliminarPorValor(T dato)
        {
            if (cabeza == null)
                return false;

            if (Comparar(cabeza.Dato, dato))
            {
                cabeza = cabeza.Siguiente;
                cantidad--;
                return true;
            }

            Nodo<T> anterior = cabeza;
            Nodo<T> actual = cabeza.Siguiente;
            while (actual != null)
            {
                if (Comparar(actual.Dato, dato))
                {
                    anterior.Siguiente = actual.Siguiente;
                    cantidad--;
                    return true;
                }
                anterior = actual;
                actual = actual.Siguiente;
            }
            return false;
        }

        private bool Comparar(T a, T b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;
            return a.Equals(b);
        }
        public T[] AArreglo()
        {
            T[] arreglo = new T[cantidad];
            Nodo<T> actual = cabeza;
            int i = 0;
            while (actual != null)
            {
                arreglo[i] = actual.Dato;
                actual = actual.Siguiente;
                i++;
            }
            return arreglo;
        }

        public static ListaEnlazada<T> DesdeArreglo(T[] arreglo)
        {
            ListaEnlazada<T> lista = new ListaEnlazada<T>();
            if (arreglo != null)
            {
                for (int i = 0; i < arreglo.Length; i++)
                {
                    lista.Agregar(arreglo[i]);
                }
            }
            return lista;
        }
    }
}