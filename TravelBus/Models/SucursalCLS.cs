using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBus.Models
{
    public class SucursalCLS
    {

        [Display(Name = "Código Sucursal")]
        public int idsucursal { get; set; }


        [Required]
        [Display(Name = "Nombre Sucursal")]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100")]
        public string nombre { get; set; }
        



        [Required]
        [Display(Name = "Dirección Sucursal")]
        [StringLength(200, ErrorMessage = "Longitud máxima de 200")]
        public string direccion { get; set; }
        



        [Required]   
        [Display(Name = "Teléfono  Sucursal")]
        [StringLength(10, ErrorMessage = "Longitud máxima de 10")]
        public string telefono { get; set; }


        [Required]
        [Display(Name = "Email Sucursal")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100")]
        [EmailAddress(ErrorMessage = "Ingrese un Email valido")]
        public string email { get; set; } 


        [Required] //Campo Requerido
        [DataType(DataType.Date)] //Control Fecha
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]//Formato tipo año-mes-dia puedes aplicar el que quieras;
        [Display(Name = "Fecha Apertura")]
        public DateTime fechaApertura { get; set; }
        

        public int bhabilitado { get; set; }




        //Propiedad para los mensajes de error;
        public string mensajeErrorSucursal  { get; set; }







    }
}