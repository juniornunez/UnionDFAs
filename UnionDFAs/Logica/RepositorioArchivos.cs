// Esta clase se encarga de guardar y cargar los automatas y las uniones en un archivo dentro
// de la carpeta del proyecto, para que no se pierdan cuando se cierra el programa.
using System.Text.Json;
using UnionDFAs.Estructuras;
using UnionDFAs.Modelo;

namespace UnionDFAs.Logica
{
    public static class RepositorioArchivos
    {
        private static string CarpetaDatos()
        {
            string carpetaProyecto = BuscarCarpetaProyecto();
            string carpeta = Path.Combine(carpetaProyecto, "Datos");
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
            return carpeta;
        }

        private static string BuscarCarpetaProyecto()
        {
            string carpetaActual = AppContext.BaseDirectory;
            DirectoryInfo directorio = new DirectoryInfo(carpetaActual);

            while (directorio != null)
            {
                string[] archivosProyecto = Directory.GetFiles(directorio.FullName, "*.csproj");
                if (archivosProyecto.Length > 0)
                {
                    return directorio.FullName;
                }
                directorio = directorio.Parent;
            }

            return AppContext.BaseDirectory;
        }

        private static string RutaArchivo()
        {
            return Path.Combine(CarpetaDatos(), "datos_automatas.json");
        }

        public static void Guardar()
        {
            AutomataArchivo[] automatasArchivo = new AutomataArchivo[Sesion.Automatas.Cantidad];
            for (int i = 0; i < Sesion.Automatas.Cantidad; i++)
            {
                automatasArchivo[i] = ConvertidorAutomata.AArchivo(Sesion.Automatas.Obtener(i));
            }

            UnionArchivo[] unionesArchivo = new UnionArchivo[Sesion.Uniones.Cantidad];
            for (int i = 0; i < Sesion.Uniones.Cantidad; i++)
            {
                UnionGuardada union = Sesion.Uniones.Obtener(i);
                UnionArchivo ua = new UnionArchivo();
                ua.NombreOrigen1 = union.NombreOrigen1;
                ua.NombreOrigen2 = union.NombreOrigen2;
                ua.NombreUnion = union.AutomataResultante.Nombre;
                unionesArchivo[i] = ua;
            }

            DatosGuardados datos = new DatosGuardados();
            datos.Automatas = automatasArchivo;
            datos.Uniones = unionesArchivo;

            JsonSerializerOptions opciones = new JsonSerializerOptions();
            opciones.WriteIndented = true;

            string json = JsonSerializer.Serialize(datos, opciones);
            File.WriteAllText(RutaArchivo(), json);
        }

        public static void Cargar()
        {
            string ruta = RutaArchivo();
            if (!File.Exists(ruta))
                return;

            string json = File.ReadAllText(ruta);
            DatosGuardados datos = JsonSerializer.Deserialize<DatosGuardados>(json);

            if (datos == null)
                return;

            Sesion.Automatas = new ListaEnlazada<Automata>();
            if (datos.Automatas != null)
            {
                for (int i = 0; i < datos.Automatas.Length; i++)
                {
                    Automata automata = ConvertidorAutomata.DesdeArchivo(datos.Automatas[i]);
                    Sesion.Automatas.Agregar(automata);
                }
            }

            Sesion.Uniones = new ListaEnlazada<UnionGuardada>();
            if (datos.Uniones != null)
            {
                OperadorUnion operador = new OperadorUnion();
                for (int i = 0; i < datos.Uniones.Length; i++)
                {
                    UnionArchivo ua = datos.Uniones[i];
                    Automata origen1 = BuscarAutomataPorNombre(ua.NombreOrigen1);
                    Automata origen2 = BuscarAutomataPorNombre(ua.NombreOrigen2);

                    if (origen1 == null || origen2 == null)
                        continue;

                    Automata resultante = operador.GenerarUnion(origen1, origen2);
                    resultante.Nombre = ua.NombreUnion;

                    UnionGuardada union = new UnionGuardada();
                    union.NombreOrigen1 = ua.NombreOrigen1;
                    union.NombreOrigen2 = ua.NombreOrigen2;
                    union.AutomataResultante = resultante;
                    Sesion.Uniones.Agregar(union);
                }
            }
        }

        private static Automata BuscarAutomataPorNombre(string nombre)
        {
            for (int i = 0; i < Sesion.Automatas.Cantidad; i++)
            {
                Automata automata = Sesion.Automatas.Obtener(i);
                if (automata.Nombre == nombre)
                    return automata;
            }
            return null;
        }
    }
}