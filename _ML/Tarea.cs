using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ML
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public _ML.Estatus estatus { get; set; }
        public _ML.Usuario usuario { get; set; }
        public List<object> Tareas { get; set; }
    }
}
