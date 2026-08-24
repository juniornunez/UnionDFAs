using UnionDFAs.Estructuras;
using UnionDFAs.Modelo;

namespace UnionDFAs.Logica
{
    public class Validador
    {
        public ResultadoValidacion Validar(Automata automata)
        {
            ResultadoValidacion resultado = new ResultadoValidacion();

            ValidarEstados(automata, resultado);
            ValidarAlfabeto(automata, resultado);
            ValidarEstadoInicial(automata, resultado);
            ValidarEstadosFinales(automata, resultado);
            ValidarTransiciones(automata, resultado);

            return resultado;
        }

        private void ValidarEstados(Automata automata, ResultadoValidacion resultado)
        {
            if (automata.Estados.Cantidad == 0)
            {
                resultado.AgregarError("El automata no tiene estados definidos");
                return;
            }

            for (int i = 0; i < automata.Estados.Cantidad; i++)
            {
                string estadoActual = automata.Estados.Obtener(i);

                for (int j = i + 1; j < automata.Estados.Cantidad; j++)
                {
                    string otroEstado = automata.Estados.Obtener(j);
                    if (estadoActual == otroEstado)
                    {
                        resultado.AgregarError("El estado '" + estadoActual + "' esta duplicado en el conjunto de estados");
                    }
                }
            }
        }

        private void ValidarAlfabeto(Automata automata, ResultadoValidacion resultado)
        {
            if (automata.Alfabeto.Cantidad == 0)
            {
                resultado.AgregarError("El automata no tiene alfabeto definido");
                return;
            }

            for (int i = 0; i < automata.Alfabeto.Cantidad; i++)
            {
                string simboloActual = automata.Alfabeto.Obtener(i);

                if (!EsSimboloValido(simboloActual))
                {
                    resultado.AgregarError("El simbolo '" + simboloActual + "' no es valido, debe ser una sola letra o un solo numero");
                }

                for (int j = i + 1; j < automata.Alfabeto.Cantidad; j++)
                {
                    string otroSimbolo = automata.Alfabeto.Obtener(j);
                    if (simboloActual == otroSimbolo)
                    {
                        resultado.AgregarError("El simbolo '" + simboloActual + "' esta duplicado en el alfabeto");
                    }
                }
            }
        }

        private bool EsSimboloValido(string simbolo)
        {
            if (simbolo == null || simbolo.Length != 1)
                return false;
            char c = simbolo[0];
            return char.IsLetterOrDigit(c);
        }

        private void ValidarEstadoInicial(Automata automata, ResultadoValidacion resultado)
        {
            if (automata.EstadoInicial == null || automata.EstadoInicial == "")
            {
                resultado.AgregarError("No se definio un estado inicial");
                return;
            }

            if (!automata.Estados.Existe(automata.EstadoInicial))
            {
                resultado.AgregarError("El estado inicial '" + automata.EstadoInicial + "' no pertenece al conjunto de estados");
            }
        }

        private void ValidarEstadosFinales(Automata automata, ResultadoValidacion resultado)
        {
            for (int i = 0; i < automata.EstadosFinales.Cantidad; i++)
            {
                string estadoFinal = automata.EstadosFinales.Obtener(i);
                if (!automata.Estados.Existe(estadoFinal))
                {
                    resultado.AgregarError("El estado final '" + estadoFinal + "' no pertenece al conjunto de estados");
                }
            }
        }

        private void ValidarTransiciones(Automata automata, ResultadoValidacion resultado)
        {
            for (int i = 0; i < automata.Estados.Cantidad; i++)
            {
                string estado = automata.Estados.Obtener(i);

                for (int j = 0; j < automata.Alfabeto.Cantidad; j++)
                {
                    string simbolo = automata.Alfabeto.Obtener(j);

                    int contador = 0;
                    string destinoEncontrado = null;

                    for (int k = 0; k < automata.Transiciones.Cantidad; k++)
                    {
                        Transicion t = automata.Transiciones.Obtener(k);
                        if (t.EstadoOrigen == estado && t.Simbolo == simbolo)
                        {
                            contador++;
                            destinoEncontrado = t.EstadoDestino;
                        }
                    }

                    if (contador == 0)
                    {
                        resultado.AgregarError("El estado '" + estado + "' carece de transicion para el simbolo '" + simbolo + "'");
                    }
                    else if (contador > 1)
                    {
                        resultado.AgregarError("El estado '" + estado + "' tiene multiples transiciones para el simbolo '" + simbolo + "', lo cual viola el determinismo");
                    }
                    else
                    {
                        if (!automata.Estados.Existe(destinoEncontrado))
                        {
                            resultado.AgregarError("El estado destino '" + destinoEncontrado + "' no esta registrado en el conjunto de estados");
                        }
                    }
                }
            }
        }
    }
}