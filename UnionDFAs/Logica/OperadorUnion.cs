using UnionDFAs.Estructuras;
using UnionDFAs.Modelo;

namespace UnionDFAs.Logica
{
    public class OperadorUnion
    {
        public Automata GenerarUnion(Automata automata1, Automata automata2)
        {
            Automata resultado = new Automata(automata1.Nombre + "_UNION_" + automata2.Nombre);

            for (int i = 0; i < automata1.Alfabeto.Cantidad; i++)
            {
                resultado.Alfabeto.Agregar(automata1.Alfabeto.Obtener(i));
            }

            ListaEnlazada<ParEstados> pares = new ListaEnlazada<ParEstados>();

            for (int i = 0; i < automata1.Estados.Cantidad; i++)
            {
                string estadoA = automata1.Estados.Obtener(i);
                for (int j = 0; j < automata2.Estados.Cantidad; j++)
                {
                    string estadoB = automata2.Estados.Obtener(j);
                    ParEstados par = new ParEstados(estadoA, estadoB);
                    pares.Agregar(par);
                    resultado.Estados.Agregar(par.NombreCombinado);

                    bool esFinal = automata1.EsFinal(estadoA) || automata2.EsFinal(estadoB);
                    if (esFinal)
                    {
                        resultado.EstadosFinales.Agregar(par.NombreCombinado);
                    }
                }
            }

            ParEstados parInicial = new ParEstados(automata1.EstadoInicial, automata2.EstadoInicial);
            resultado.EstadoInicial = parInicial.NombreCombinado;

            for (int i = 0; i < pares.Cantidad; i++)
            {
                ParEstados par = pares.Obtener(i);
                for (int j = 0; j < resultado.Alfabeto.Cantidad; j++)
                {
                    string simbolo = resultado.Alfabeto.Obtener(j);
                    string destinoA = automata1.BuscarDestino(par.EstadoA, simbolo);
                    string destinoB = automata2.BuscarDestino(par.EstadoB, simbolo);
                    string nombreDestino = "(" + destinoA + "," + destinoB + ")";
                    Transicion transicion = new Transicion(par.NombreCombinado, simbolo, nombreDestino);
                    resultado.Transiciones.Agregar(transicion);
                }
            }

            return resultado;
        }
    }
}