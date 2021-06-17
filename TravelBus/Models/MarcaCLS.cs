using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBus.Models
{
    public class MarcaCLS
    {
        //GET:SET
        [Display(Name ="Codigo Marca")] 
        public int iidmarca { get; set; }

        [Display(Name = "Nombre de la  Marca")]
        [StringLength(100,ErrorMessage ="Máximo 100 caracteres")]
        [Required]
        public string nombre { get; set; }

        [Display(Name = "Descripción Marca")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Required]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }




        //Añado una propiedad(Errores de validacion);
        public string mensajeError { get; set; }





    }
}