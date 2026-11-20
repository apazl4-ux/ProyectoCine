using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine
{
    public class Funcion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Sala { get; set; }
        public decimal Precio { get; set; }

        public override string ToString()
        {
            return $"Id: {Id,5}, Nombre: {Nombre,-25}, Sala: {Sala,-15}, Precio: {Precio,9:N2}";
        }
    }
}
