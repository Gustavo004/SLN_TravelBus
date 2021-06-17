using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBus.Models
{


    public class TipoUsuarioCLS
    {
        [Display(Name = "ID Tipo usuario")]
        [Required]
        public int iidtipousuario { get; set; }


        [Display(Name = "Nombre tipo usuario")]
        [StringLength(150, ErrorMessage = "Longitud máxima 150")]
        [Required]
        public string nombre { get; set; }


        [Display(Name = "Descripción tipo usuario")]
        [StringLength(250, ErrorMessage = "Longitud máxima 250")]
        [Required]
        public string descripcion { get; set; }



        public int bhabilitado { get; set; }



    }





}