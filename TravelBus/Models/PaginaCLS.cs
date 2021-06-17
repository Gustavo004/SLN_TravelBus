using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBus.Models
{
    public class PaginaCLS
    {
        [Display(Name ="ID Pagina")]
        public int iddPagina { get; set; }

        [Required]
        [Display(Name = "Titulo del Link")]
        public string  mensaje { get; set; }

        [Required]
        [Display(Name = "Nombre de la acción")]
        public string accion { get; set; }

        [Required]
        [Display(Name = "Nombre del controlador")]
        public string contralador { get; set; }


        public int bhabilitado { get; set; }




    }
}