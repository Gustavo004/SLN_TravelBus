using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TravelBus.Models
{
    public class ViajesCLS
    {
        [Display(Name ="Código Viaje")]
        public int  iddViaje { get; set; }


        [Display(Name = "Lugar Origen")]
        [Required]
        public int iddLugarOrigen { get; set; }


        [Display(Name = "Lugar Destino")]
        [Required]
        public int iddLugarDestino { get; set; }


        [Required]
        [Display(Name = "Precio")]
        [Range(0,9999,ErrorMessage ="Rango fuera de indices")]
        public double precio { get; set; }


        [Display(Name = "Fecha Viaje")]
        [DataType(DataType.Date)] //Poner una fecha
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//Formato tipo año-mes-dia puedes aplicar el que quieras;
        [Required]
        public DateTime fechaViaje { get; set; }


        [Display(Name = "Bus")]
        [Required]
        public int iidBus { get; set; }


        [Display(Name ="Número de asientos disponibles")]
        [Required]
        public int numeroAsientosDisponibles { get; set; }




        //Propiedades adicionales;

        [Display(Name ="Lugar Origen")]
        public string nombreLugarOrigen { get; set; }

        [Display(Name = "Lugar Destino")]
        public string nombreLugarDestino { get; set; }

        [Display(Name = "Nombre Bus")]
        public string nombreBus { get; set; }

    }

}