using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBus.Models
{
    public class EmpleadoCLS
    {

        [Display(Name ="Codigo Empleado")]
        public int iidempleado{ get; set; }



        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100, ErrorMessage = "Solamente 100 caracteres")]  
        public string nombre { get; set; }



        [Display(Name = "Apellido Paterno")]
        [Required]
        [StringLength(200, ErrorMessage = "Solamente 200 caracteres")]   
        public string apPaterno { get; set; }


        [Display(Name = "Apellido Materno")]
        [Required]
        [StringLength(200, ErrorMessage = "Solamente 200 caracteres")]
        public string apMaterno { get; set; }



        [Display(Name = "Fecha Contrato")]
        [Required]
        [DataType(DataType.Date)] //Poner una fecha
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//Formato tipo año-mes-dia puedes aplicar el que quieras;
        public DateTime fechaContrato { get; set; }



        [Display(Name = "Tipo Usuario")]
        [Required]
        public int  iidtipoUsuario { get; set; }


        [Display(Name = "Tipo Contrato")]
        [Required]
        public int iidtipoContrato { get; set; }


        [Display(Name = "Sexo")]
        [Required]
        public int iidSexo { get; set; }



       public int bHabilitado { get; set; }



        [Display(Name = "Sueldo")]
        [Range(0,100000,ErrorMessage ="Fuera de rango")]
        [Required]
        public decimal sueldo { get; set; }



        //Propiedades Adicionales lo que mostraremos en la vista;
        [Display(Name = "Tipo Contrato")]
        public string nombreTipoContrato { get; set; }

        [Display(Name = "Tipo usuario")]
        public string nombreTipoUsuario { get; set; }


  


    }
}