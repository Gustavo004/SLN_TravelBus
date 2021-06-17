using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBus.Models
{
    public class BusCLS
    {
        [Display(Name = "ID BUS")]
        public int IIDBUS { get; set; }


        [Display(Name = "Nombre Sucursal")]
        [Required]
        public int iidsucursal { get; set; }


        [Display(Name = "Tipo Bus")]
        [Required]
        public int iidtipobus { get; set; }


        [Display(Name = "Placa")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Required]
        public string placa { get; set; }


        [Display(Name = "Fecha compra")]
        [DataType(DataType.Date)] //Poner una fecha
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//Formato tipo año-mes-dia puedes aplicar el que quieras;
        [Required]
        public DateTime fechacompra { get; set; }


        [Display(Name = "Nombre modelo")]
        [Required]
        public int iidmodelo { get; set; }


        [Display(Name = "Número  filas")]   
        [Required] 
        public int numerofilas { get; set; }


        [Display(Name = "Número de columnas")]
        [Required]
        public int numerocolumnas { get; set; }

        public int bhabilitado { get; set; }


        [Display(Name = "Descripción")]
        [Required]
        public string descripcionBus { get; set; }



        [Display(Name = "Observación")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string observacion { get; set; }


        [Display(Name = "Nombre marca")]
        [Required]
        public int iidmarca { get; set; }


    

        //Propiedades adicionales mostradas a la vista;
        //Empleadas para Join

        [Display(Name = "Nombre Sucursal")]
        public string nombreSucursalBus { get; set; }


        [Display(Name = "Nombre Tipo De Bus")]
        public string nombreTipoBus { get; set; }


        [Display(Name = "Nombre modelo Bus ")]
        public string nombreTipoModelo { get; set; }


        [Display(Name = "Nombre del Tipo de Marca")]
        public string nombreTipoMarca { get; set; }


        [Display(Name = "Dirección de Sucursal")]
        public string direccionSucursal { get; set; }

        [Display(Name = "Teléfono de Sucursal")]
        public string telefonoSucursal { get; set; }

        [Display(Name = "Email de Sucursal")]
        public string emailSucursal { get; set; }

        [Display(Name = "Descripción General Del Modelo")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        //[Required]
        public string descripcionModelo { get; set; }



        //Propiedades para los mensajes de error;
        public string mensajeErrorBus { get; set; }

















    }
}