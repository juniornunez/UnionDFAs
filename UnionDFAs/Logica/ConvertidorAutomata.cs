using UnionDFAs.Estructuras;

namespace UnionDFAs.Modelo
{
    public static class ConvertidorAutomata
    {
        public static AutomataArchivo AArchivo(Automata automata)
        {
            AutomataArchivo archivo = new AutomataArchivo();
            archivo.Nombre = automata.Nombre;
            archivo.Estados = automata.Estados.AArreglo();
            archivo.Alfabeto = automata.Alfabeto.AArreglo();
            archivo.EstadoInicial = automata.EstadoInicial;
            archivo.EstadosFinales = automata.EstadosFinales.AArreglo();

            TransicionArchivo[] transiciones = new TransicionArchivo[automata.Transiciones.Cantidad];
            for (int i = 0; i < automata.Transiciones.Cantidad; i++)
            {
                Transicion t = automata.Transiciones.Obtener(i);
                TransicionArchivo ta = new TransicionArchivo();
                ta.Origen = t.EstadoOrigen;
                ta.Simbolo = t.Simbolo;
                ta.Destino = t.EstadoDestino;
                transiciones[i] = ta;
            }
            archivo.Transiciones = transiciones;

            return archivo;
        }

        public static Automata DesdeArchivo(AutomataArchivo archivo)
        {
            Automata automata = new Automata(archivo.Nombre);
            automata.Estados = ListaEnlazada<string>.DesdeArreglo(archivo.Estados);
            automata.Alfabeto = ListaEnlazada<string>.DesdeArreglo(archivo.Alfabeto);
            automata.EstadoInicial = archivo.EstadoInicial;
            automata.EstadosFinales = ListaEnlazada<string>.DesdeArreglo(archivo.EstadosFinales);

            automata.Transiciones = new ListaEnlazada<Transicion>();
            if (archivo.Transiciones != null)
            {
                for (int i = 0; i < archivo.Transiciones.Length; i++)
                {
                    TransicionArchivo ta = archivo.Transiciones[i];
                    automata.Transiciones.Agregar(new Transicion(ta.Origen, ta.Simbolo, ta.Destino));
                }
            }

            return automata;
        }
    }
}