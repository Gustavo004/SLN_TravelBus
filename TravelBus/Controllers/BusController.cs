using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelBus.Models;


namespace TravelBus.Controllers
{
    public class BusController : Controller
    {
        //INDEX:
        //Listamos a los buses en el Index;
        public ActionResult Index(BusCLS oBusCLS)
        {
            listarCombos();


            //Creamos una listaRpta:
            List<BusCLS> ListaRpta = new List<BusCLS>();

            //Cremos una lista tipo Bus
            List<BusCLS> ListarBuses = null;
      

            //abrimos la conexion con la base de datos;
            using (var bd = new BDPasajeEntities())
            {
                //Vamos a listar todos los buses;
                //Mostrando los datos;
                ListarBuses = (from bus in bd.Bus

                               join sucursal in bd.Sucursal  //1er Join
                               on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL


                               join tipobus in bd.TipoBus   //2do Join
                               on bus.IIDTIPOBUS equals tipobus.IIDTIPOBUS

                               join tipomodelo in bd.Modelo  //3er Join
                               on bus.IIDMODELO equals tipomodelo.IIDMODELO


                               join marca in bd.Marca
                               on bus.IIDMARCA equals marca.IIDMARCA //4to Join

                               where bus.BHABILITADO == 1 //Condicion;

                               select new BusCLS
                               {
                                   IIDBUS = bus.IIDBUS,
                                   placa = bus.PLACA,
                                   nombreTipoModelo = tipomodelo.NOMBRE, //ModeloBus
                                   fechacompra = (DateTime)bus.FECHACOMPRA,
                                   descripcionModelo = tipomodelo.DESCRIPCION, //Descripcion Modelo
                                   numerofilas = (int)bus.NUMEROFILAS,          //Filas del Bus
                                   numerocolumnas = (int)bus.NUMEROCOLUMNAS,  //Columnas del Bus
                                   nombreTipoMarca = marca.NOMBRE,       //Marca 
                                   descripcionBus = bus.DESCRIPCION,
                                   nombreSucursalBus = sucursal.NOMBRE,
                                   nombreTipoBus = tipobus.NOMBRE, //Nombre Tipo BUS
                                   direccionSucursal = sucursal.DIRECCION,
                                   telefonoSucursal = sucursal.TELEFONO,
                                   emailSucursal = sucursal.EMAIL,
                                   //traemos lo restante para el filtrado del formulario;
                                   iidmodelo = tipomodelo.IIDMODELO,
                                   iidsucursal = sucursal.IIDSUCURSAL,
                                   iidtipobus = tipobus.IIDTIPOBUS
                               }).ToList();

                //Validando los datos;
                if (oBusCLS.IIDBUS == 0 &&
                   oBusCLS.placa == null &&
                   oBusCLS.iidmodelo == 0 &&
                   oBusCLS.iidsucursal == 0 &&
                   oBusCLS.iidtipobus == 0)
                {
                    //Asignamos los valores de listado orginal a listadoRpta
                    ListaRpta = ListarBuses;
                }  //fin del if
                else 
                {
                    //Inicio de filtros;


                    //Filtros;

                    //1er Filtro Bus;
                    if (oBusCLS.IIDBUS != 0)
                    {
                        //retomamos la lista pero con filtro
                        ListarBuses = ListarBuses.Where(p => p.IIDBUS.ToString().Contains(oBusCLS.IIDBUS.ToString())).ToList();
                    }

                    //2do Filtro Placa;
                    if (oBusCLS.placa != null)
                    {
                        //retomamos la lista pero con filtro
                        ListarBuses = ListarBuses.Where(p => p.placa.Contains(oBusCLS.placa)).ToList();
                    }


                    //3er Filtro Modelo;
                    if (oBusCLS.iidmodelo != 0)
                    {
                        //retomamos la lista pero con filtro
                        ListarBuses = ListarBuses.Where(p => p.iidmodelo.ToString().Contains(oBusCLS.iidmodelo.ToString())).ToList();
                    }


                    //4to Filtro Sucursal;
                    if (oBusCLS.iidsucursal != 0)
                    {
                        ListarBuses = ListarBuses.Where(p => p.iidsucursal.ToString().Contains(oBusCLS.iidsucursal.ToString())).ToList();
                    }

                    //5to Filtro por TIPOBUS;
                    if (oBusCLS.iidtipobus != 0)
                    {
                        ListarBuses = ListarBuses.Where(p => p.iidtipobus.ToString().Contains(oBusCLS.iidtipobus.ToString())).ToList();
                    }

                    //obtenemos todos los valores y lo pasamos a una lista nueva;
                    ListaRpta = ListarBuses;

                }//fin del else

            }//Fin de la conexion;
            return View(ListaRpta);
        }

        //MÉTODOS CRUD:
        //Vista HTML Para Agregar;
        public ActionResult Agregar()
        {
            //Pasamos la informacion obtenida de los 4 combos;
            listarCombos();
            return View();
        }

        //No tocar;
        //Método agregar
        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS)
        {

            //Validacion para que no se repita el nombre de marcas;

            //contador de registros encontrados;
            int nregistrosEncontrados = 0;

            //Pasando de nombre de la clase a una variable;
            string nombrePlaca = oBusCLS.placa;


            using (var bd = new BDPasajeEntities())
            {
                nregistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(nombrePlaca)).Count();

            }

            if (ModelState.IsValid && nregistrosEncontrados == 0)
            {
                using (var bd = new BDPasajeEntities())
                {
                    Bus oBus = new Bus();
                    oBus.BHABILITADO = 1;
                    oBus.PLACA = oBusCLS.placa;
                    oBus.FECHACOMPRA = oBusCLS.fechacompra;
                    oBus.IIDMODELO = oBusCLS.iidmodelo;
                    oBus.NUMEROFILAS = oBusCLS.numerofilas;
                    oBus.NUMEROCOLUMNAS = oBusCLS.numerocolumnas;
                    oBus.IIDMARCA = oBusCLS.iidmarca;
                    oBus.DESCRIPCION = oBusCLS.descripcionBus;
                    oBus.OBSERVACION = oBusCLS.observacion;
                    oBus.IIDTIPOBUS = oBusCLS.iidtipobus;
                    oBus.IIDSUCURSAL = oBusCLS.iidsucursal;
                    bd.Bus.Add(oBus);
                    bd.SaveChanges();
                }
            }
            else
            {
                listarCombos();
                oBusCLS.mensajeErrorBus = "La placa ya existe";
                return View(oBusCLS);
            }
            return RedirectToAction("Index");
        }


        //Vista HTML para editar;
        //LLENANDO INFORMACION
        public ActionResult Editar(int id)
        {

            BusCLS oBusCLS = new BusCLS();
            listarCombos();

            using (var bd = new BDPasajeEntities())
            {

                Bus obus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();

                //Pasando informacion
                oBusCLS.IIDBUS = obus.IIDBUS;
                oBusCLS.iidsucursal = (int)obus.IIDSUCURSAL;
                oBusCLS.iidtipobus = (int)obus.IIDTIPOBUS;
                oBusCLS.placa = obus.PLACA;
                oBusCLS.fechacompra = (DateTime)obus.FECHACOMPRA;
                oBusCLS.iidmodelo = (int)obus.IIDMODELO;
                oBusCLS.numerocolumnas = (int)obus.NUMEROCOLUMNAS;
                oBusCLS.numerofilas = (int)obus.NUMEROFILAS;
                oBusCLS.descripcionBus = obus.DESCRIPCION;
                oBusCLS.observacion = obus.OBSERVACION;
                oBusCLS.iidmarca = (int)obus.IIDMARCA;
            }
            return View(oBusCLS);
        }


        //Método Editar:
        //Metodo real para editar la informacion;
        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS)
        {
            // Validacion para que no se repita la Placa de Bus;

            //contador de registros encontrados;
            int nregistrosEncontrados = 0;

            //Obtengo el valor de la placa de mi clase
            string placa = oBusCLS.placa;

            //Obteniendo el ID del cliente;
            int idBus = oBusCLS.IIDBUS;

            //llamada a la bd para ver si se repite o no 

            using(var bd = new BDPasajeEntities() ) 
            {
                /*Esta linea de codigo cuenta el numero de registros de bus y placa 
                 * quiere decir:almacename en una variable desde la base de datos todo lo relacionado con placa y codigo de bus sin repetir valores;
                 */
                nregistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placa) && !p.IIDBUS.Equals(idBus)).Count();
            }


            if (!ModelState.IsValid || nregistrosEncontrados>=1 ) 
            {
                listarCombos();
                if (nregistrosEncontrados >= 1) 
                {
                    oBusCLS.mensajeErrorBus = "La placa ya existe";
                    listarCombos();
                    return View(oBusCLS);
                }
            }


            //Abriendo la cadena de cnx;
            using (var bd = new BDPasajeEntities())
            {
                //recuperamos el ID del bus;
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();

                //CAMPOS A EDITAR
                oBus.PLACA = oBusCLS.placa;
                oBus.FECHACOMPRA = (DateTime)oBusCLS.fechacompra;
                oBus.IIDMODELO = (int)oBusCLS.iidmodelo; //ComboBox;
                oBus.NUMEROFILAS = (int)oBusCLS.numerofilas;
                oBus.NUMEROCOLUMNAS = (int)oBusCLS.numerocolumnas;
                oBus.IIDMARCA = (int)oBusCLS.iidmarca;  //ComboBox;
                oBus.DESCRIPCION = oBusCLS.descripcionBus;
                oBus.OBSERVACION = oBusCLS.observacion;
                oBus.IIDTIPOBUS = (int)oBusCLS.iidtipobus; //ComboBox;
                oBus.IIDSUCURSAL = (int)oBusCLS.iidsucursal; // ComboBox;

                //GUARDAMOS LOS DATOS;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //Método elimninar
        public ActionResult Eliminar(int IIDBUS) 
        {
            //Abriendo cadena de conexion

            using (var bd = new BDPasajeEntities() ) 
            {
                //eliminacion lógica;

                //Obteniendo el registro cuando le demos click;
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(IIDBUS)).First();

                //cambiamos el estado:
                oBus.BHABILITADO = 0;

                //Guardando los cambios;
                bd.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        // MÉTODOS PARA EL LLENADO DE COMBOS:

        //LISTAR BUSES;
        public void ListarTipoBus()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;
            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {

                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from item in bd.TipoBus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,    //Valor mostrado para el usuario
                             Value = item.IIDTIPOBUS.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaTipoBus = lista;
            }
        }

        //LISTAR MARCAS;
        public void ListarMarca()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;

            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {

                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from item in bd.Marca
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,    //Valor mostrado para el usuario
                             Value = item.IIDMARCA.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaMarca = lista;
            }
        }

        //LISTAR MODELO DE BUSES;
        public void ListarModelo()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;

            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {
                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from item in bd.Modelo
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,    //Valor mostrado para el usuario
                             Value = item.IIDMODELO.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaModelo = lista;
            }
        }

        //LISTAMOS LAS SUCURSALES;
        public void ListarSucursal()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;
            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {
                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from item in bd.Sucursal
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,    //Valor mostrado para el usuario
                             Value = item.IIDSUCURSAL.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });

                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaSucursal = lista;
            }
        }

        //Metodo que llama a todos los combos;
        public void listarCombos()
        {
            ListarSucursal();
            ListarModelo();
            ListarMarca();
            ListarTipoBus();
        }


    }
}