using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBus.Models;


namespace TravelBus.Controllers
{
    public class ViajeController : Controller
    {
        //Método para listar mis viajes;
        // GET: Viaje
        public ActionResult Index()
        {
            //Lista tipo Viajes;
            List<ViajesCLS> ListaViajes = null;

            //Abriendo conexión con la base de datos;
            using (var bd = new BDPasajeEntities() ) 
            {

                ListaViajes = (from viaje in bd.Viaje
                               join lugarorigen in bd.Lugar
                               on viaje.IIDLUGARORIGEN equals lugarorigen.IIDLUGAR

                               join lugardestino in bd.Lugar
                               on viaje.IIDLUGARDESTINO equals lugardestino.IIDLUGAR

                               join bus in bd.Bus
                               on viaje.IIDBUS equals bus.IIDBUS

                               select new ViajesCLS
                               {
                                   iddViaje=viaje.IIDVIAJE,
                                   nombreBus=bus.PLACA,
                                   nombreLugarOrigen=lugarorigen.NOMBRE,
                                   nombreLugarDestino=lugardestino.NOMBRE

                               }).ToList();  
            }
         return View(ListaViajes);
       }


        //Vista HTML para mi formulario Agregar Buses;
        public ActionResult Agregar() 
        {
            listarCombos();
            return View();
        }













        //Combo Box para los campos:
        //Lugar
        //Bus

        //Funcion para listar los origenes del Bus;
        public void ListarLugaresOrigen()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;

            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {

                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from item in bd.Lugar
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,    //Valor mostrado para el usuario
                             Value = item.IIDLUGAR.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaLugar = lista;
            }
        }



        //Funcion par listar los destino del Bus;
        public void ListarBus()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;

            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {

                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from item in bd.Bus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.PLACA,    //Valor mostrado para el usuario
                             Value = item.IIDBUS.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaBus = lista;
            }
        }


        //Este método guarda los combos que usamos en el formulario Viaje,lo usamos para ponerlo en la vista;
        public void listarCombos() 
        {
            ListarBus();
            ListarLugaresOrigen();
        }





    }
}