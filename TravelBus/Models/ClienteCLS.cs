using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TravelBus.Models
{
    public class ClienteCLS
    {
        [Display(Name = "Codigo Cliente")]
        public int idcliente { get; set; }


        [Display(Name = "Nombre Cliente")]
        [StringLength(100, ErrorMessage = "Solamente 100 caracteres")]
        [Required]
        public string nombre { get; set; }


        [Display(Name = "Apellido Paterno")]
        [StringLength(150, ErrorMessage = "Solamente 150 caracteres")]
        [Required]  
        public string appaterno { get; set; }



        [Display(Name = "Apellido Materno")]
        [StringLength(150, ErrorMessage = "Solamente 150 caracteres")]
        [Required]
        public string apmaterno { get; set; }


        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(200, ErrorMessage = "Solamente 200 caracteres")]
        [Required]
        public string Email { get; set; }


        [Display(Name = "Dirección")]  
        [StringLength(200, ErrorMessage = "Solamente 200 caracteres")]
        [Required]
        public string direccion { get; set; }


        [Display(Name = "Sexo")]
        [Required]  
        public int idsexo { get; set; }




        [Display(Name = "Teléfono Fijo")]
        [StringLength(10, ErrorMessage = "Solamente 10 caracteres")]
        [Required]
        public string telefonofijo { get; set; }




        [Display(Name = "Celular")]  
        [StringLength(9, ErrorMessage = "Solamente 9 caracteres")]
        [Required]
        public string telefonocelular { get; set; }





    }
}