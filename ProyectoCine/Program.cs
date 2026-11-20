using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoCine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Opcion;
            FuncionRepositorio repo = new FuncionRepositorio();
            do
            {
                Opcion = Menu();

                switch (Opcion)
                {
                    case "1":
                        RegistrarFuncion(repo);
                        break;
                    case "2":
                        listarFunciones(repo);
                        break;
                    case "3":
                        BuscarFuncion(repo);
                        break;
                    case "4":
                        ActualizarPrecioFuncion(repo);
                        break;
                    case "5":
                        EliminarFuncion(repo);
                        break;
                    case "6":
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opcion No Valida");
                        break;
                }
            }
            while (Opcion != "6");
        }
        public static string Menu()
        {
            Console.WriteLine();
            Console.WriteLine("-_-_-_'Sistema de Funciones Cine'_-_-_-");
            Console.WriteLine();
            Console.WriteLine("Opcion 1: Registrar Funcion");
            Console.WriteLine("Opcion 2: Listar Funciones");
            Console.WriteLine("Opcion 3: Buscar Funcion Por Id");
            Console.WriteLine("Opcion 4: Actualizar Precio");
            Console.WriteLine("Opcion 5: Eliminar Funcion");
            Console.WriteLine("Opcion 6: Salir");
            Console.Write("Elige Una Opcion: ");
            return Console.ReadLine();
        }
        public static void RegistrarFuncion(FuncionRepositorio repo)
        {

            Console.WriteLine();
            Console.WriteLine("Registro de Producto");
            Console.Write("Nombre Pelicula: ");
            string nombre = Console.ReadLine().Trim();
            Console.Write("Sala: ");
            string Sala = Console.ReadLine().Trim();
            Console.Write("Precio: ");
            bool precioOk = decimal.TryParse(Console.ReadLine(), out decimal precio);

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(Sala) || !precioOk || precio <= 0)
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }
            try
            {
                repo.Insertar(new Funcion
                {
                    Nombre = nombre,
                    Sala = Sala,
                    Precio = precio,
                });
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al Insertar {ex.Message}");
            }
        }
        public static void listarFunciones(FuncionRepositorio repo)
        {
            try
            {
                List<Funcion> lista = repo.ListarProductos();
                if (lista.Count == 0)
                {
                    Console.WriteLine("No hay productos en el inventario.");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("=== Funciones ===");
                Console.WriteLine($"{"Id",-5} {"Nombre",-25} {"Categoria",-15} {"Precio",10}");
                foreach (Funcion f in lista)
                {
                    Console.WriteLine(f);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }
        public static void BuscarFuncion(FuncionRepositorio repo)
        {
            Console.WriteLine();
            Console.Write("Id de la Funcion a Buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }
            try
            {
                Funcion? funcion = repo.BuscarPorId(Id);
                Console.WriteLine($"{"Id",-5} {"Nombre",-25} {"Categoria",-15} {"Precio",10}");
                Console.WriteLine(funcion == null ? "No existe ese Id." : funcion.ToString());
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al buscar: {ex.Message}");
            }
        }
        public static void ActualizarPrecioFuncion(FuncionRepositorio repo)
        {
            Console.WriteLine();
            Console.Write("Id De La Funcion a Actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("Id No Valido");
                return;
            }

            try
            {
                Funcion? funcion = repo.BuscarPorId(Id);
                if (funcion == null)
                {
                    Console.WriteLine("No existe ese Id");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine($"Producto Seleccionado:");
                Console.WriteLine($"{"Id",-5} {"Nombre",-25} {"Categoria",-15} {"Precio",10}");
                Console.WriteLine(funcion);

                Console.Write("Nuevo Precio: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal Precio) || Precio <= 0)
                {
                    Console.WriteLine("Precio inválido.");
                    return;
                }
                funcion.Precio = Precio;
                Console.WriteLine(repo.Actualizar(funcion) ? "Funcion Actualizada" : "No se Actualizo");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
            }
        }
        public static void EliminarFuncion(FuncionRepositorio repo)
        {
            Console.Write("Id De la Funcion a Eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("Id No Valido");
                return;
            }
            try
            {
                Console.WriteLine(repo.Eliminar(Id) ? "Funcion Eliminada" : "No se Elimino");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
            }
        }
    }
}