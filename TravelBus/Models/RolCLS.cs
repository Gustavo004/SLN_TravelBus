using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBus.Models
{
    public class RolCLS
    {
        [Display(Name = "ID Rol")]
        public int iidRol { get; set; }

        [Required]
        [Display(Name ="Nombre Rol")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [StringLength(100, ErrorMessage = "Solamente 100 caracteres")]
        public string descripcion { get; set; }


        public int bhabilitado { get; set; }



    }
}