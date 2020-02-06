using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalWebTrabajos.Models
{
    public class Trabajos
    {
        [Key]
        public int JobID { get; set; }

        [Required(ErrorMessage = "De una categoria para el trabajo")]
        [Display(Name = "Categoria")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Escriba pais y ciudad donde esta el trabajo")]
        [Display(Name = "Ciudad")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Introduzca la compañia que subio este anuncio de trabajo")]
        [Display(Name = "Compañia")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Escriba la posicion a la que se esta aplicando")]
        [Display(Name = "Posicion")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Escriba la descripcion de trabajo")]
        [Display(Name ="Descripcion")]
        public string Description { get; set; }
    }
}