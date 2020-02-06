using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalWebTrabajos.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage ="Introduzca su nombre de usuario")]
        [StringLength(200, ErrorMessage ="Porfavor introduzca un nombre de usuario con mas de 3 caracteres", MinimumLength =3)]
        [Display(Name ="Nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Porfavor introduzca su nombre")]
        [Display(Name="Nombre Completo")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Introduzca un correo electronico valido")]
        [Required(ErrorMessage ="Introduzca un correo electronico valido")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(200, ErrorMessage = "Porfavor introduzca una contraseña con mas de 7 caracteres", MinimumLength = 7)]
        [Required(ErrorMessage = "Introduzca su contraseña")]
        [Display(Name="Contraseña")]
        public string Password { get; set; }

        public bool Admin { get; set; }
    }
}