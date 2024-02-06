using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ML
{
    public class Tarea
    {
        [Required]
        public int IdTarea { get; set; }
        [DisplayName("Titulo:")]
        [Required]
        [StringLength(50, ErrorMessage =" Excediste la longitud permitida en este campo.")]
        [RegularExpression(@"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage="Solo Letras")]
        public string Titulo { get; set; }
        [DisplayName("Descripcion:")]
        [Required]
        [StringLength(250, ErrorMessage = " Excediste la longitud permitida en este campo.")]
        [RegularExpression(@"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "Solo Letras")]
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public _ML.Estatus estatus { get; set; }
        public _ML.Usuario usuario { get; set; }
        public List<object> Tareas { get; set; }
    }
}
