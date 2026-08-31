// Estas clases son solo para poder guardar y leer los automatas y las uniones en un archivo,
// convirtiendo nuestras listas enlazadas en arreglos normales que si se pueden guardar en formato JSON.
namespace UnionDFAs.Modelo
{
    public class TransicionArchivo
    {
        public string Origen { get; set; }
        public string Simbolo { get; set; }
        public string Destino { get; set; }
    }

    public class AutomataArchivo
    {
        public string Nombre { get; set; }
        public string[] Estados { get; set; }
        public string[] Alfabeto { get; set; }
        public string EstadoInicial { get; set; }
        public string[] EstadosFinales { get; set; }
        public TransicionArchivo[] Transiciones { get; set; }
    }

    public class UnionArchivo
    {
        public string NombreOrigen1 { get; set; }
        public string NombreOrigen2 { get; set; }
        public string NombreUnion { get; set; }
    }

    public class DatosGuardados
    {
        public AutomataArchivo[] Automatas { get; set; }
        public UnionArchivo[] Uniones { get; set; }
    }
}