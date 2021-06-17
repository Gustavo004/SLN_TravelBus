using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBus.Models;

namespace TravelBus.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index(ClienteCLS oCliente)
        {
            //Creando una lista tipo cliente;
            List<ClienteCLS> ListaCliente = null;

            int idsexo = oCliente.idsexo;


            //Llamando al método  LlenarSexo();
            LlenarSexo();

            //pasando Lista voy a pasar su valor a la vista mediante un ViewBag
            ViewBag.lista = listaSexo;

    

            //abriendo una conexion a la base de datos;
            using (var bd = new BDPasajeEntities())
            {

                if (oCliente.idsexo == 0)
                { 
                //hacemos un Listado;
                ListaCliente = (from cliente in bd.Cliente
                                where cliente.BHABILITADO == 1 //clausala where
                                select new ClienteCLS
                                {
                                    idcliente = cliente.IIDCLIENTE,
                                    nombre = cliente.NOMBRE,
                                    appaterno = cliente.APPATERNO,
                                    apmaterno = cliente.APMATERNO,
                                    Email = cliente.EMAIL,
                                    direccion = cliente.DIRECCION,
                                    idsexo = (int)cliente.IIDSEXO, //Casteamos el codigo sexo
                                    telefonofijo = cliente.TELEFONOFIJO,
                                    telefonocelular = cliente.TELEFONOCELULAR
                                }).ToList();
                }

                else 
                {
                    //hacemos un Listado;
                    ListaCliente = (from cliente in bd.Cliente
                                    where cliente.BHABILITADO == 1 //clausala where
                                    && cliente.IIDSEXO == idsexo

                                    select new ClienteCLS
                                    {
                                        idcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        Email = cliente.EMAIL,
                                        direccion = cliente.DIRECCION,
                                        idsexo = (int)cliente.IIDSEXO, //Casteamos el codigo sexo
                                        telefonofijo = cliente.TELEFONOFIJO,
                                        telefonocelular = cliente.TELEFONOCELULAR
                                    }).ToList();
                }


            }
            return View(ListaCliente); //regresamos la vista con la lista;
        }

        //Agregar vista para Editar el Cliente; 
        //Recuerda que esta vista recibe un parametro que es el ID para eliminar o editar
        //Recuperando los datos;
        public ActionResult Editar(int id)
        {
            ClienteCLS oClienteCLS = new ClienteCLS();

            using (var bd = new BDPasajeEntities())
            {
                //1: El controller llenar y pasar informacion gracias al ViewBag

                //Llamando al método  LlenarSexo();
                LlenarSexo();

                //pasando Lista voy a pasar su valor a la vista mediante un ViewBag
                ViewBag.lista = listaSexo;

                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();

                oClienteCLS.idcliente = oCliente.IIDCLIENTE;
                oClienteCLS.nombre = oCliente.NOMBRE;
                oClienteCLS.appaterno = oCliente.APPATERNO;
                oClienteCLS.apmaterno = oCliente.APPATERNO;
                oClienteCLS.direccion = oCliente.DIRECCION;
                oClienteCLS.Email = oCliente.EMAIL;
                oClienteCLS.idsexo = (int)oCliente.IIDSEXO;//ComboBox
                oClienteCLS.telefonocelular = oCliente.TELEFONOCELULAR;
                oClienteCLS.telefonofijo = oCliente.TELEFONOFIJO;
            }
            return View(oClienteCLS);
        }


        //Metodo real para editar
        [HttpPost]
        public ActionResult Editar(ClienteCLS oclienteCLS)
        {

            //obteniendo el id del cliente
            int idcliente = oclienteCLS.idcliente;

            if (!ModelState.IsValid)
            {
                return View(oclienteCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                //Recuperamos el ID del cliente;
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idcliente)).First();

                //CAMPOS A EDITAR
                oCliente.NOMBRE = oclienteCLS.nombre;
                oCliente.APPATERNO = oclienteCLS.appaterno;
                oCliente.APMATERNO = oclienteCLS.apmaterno;
                oCliente.DIRECCION = oclienteCLS.direccion;
                oCliente.EMAIL = oclienteCLS.Email;
                oCliente.IIDSEXO = oclienteCLS.idsexo;
                oCliente.TELEFONOFIJO = oclienteCLS.telefonofijo;
                oCliente.TELEFONOCELULAR = oclienteCLS.telefonocelular;

                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }




        //Creando una Lista para el comboBox
        List<SelectListItem> listaSexo;

        //Crear un metodo para obtener los valores de la tabla Sexo;
        private void LlenarSexo()
        {
            //Abriendo una conexion la base de datos;
            using (var bd = new BDPasajeEntities())
            {
                //la lista de arriba la recorremos con la creada variable sexo(minuscula) 
                //y con los valores de Sexo de EF

                listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1 //Voy a listar con el estado Uno
                             select new SelectListItem //Seleccionamos y pasamos a ListaSexo
                             {
                                 Text = sexo.NOMBRE,  //Valor Mostrado para el usuario (MASCULINO o FEMENINO)

                                 Value = sexo.IIDSEXO.ToString()  //Valor almacenado Real(1 o 2);
                             }).ToList();

                //Poniendo una opcion "Seleccione" como primer elemento del comboBox;
                listaSexo.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });

            }
        }

        //Agregando una Vista HTML
        public ActionResult Agregar()
        {
            //INICIALIZANDO LOS COMBOS Y EL VIEWBAG;

            //Llamando al método  LlenarSexo();
            LlenarSexo();

            //pasando Lista voy a pasar su valor a la vista mediante un ViewBag
            ViewBag.lista = listaSexo;

            return View();
        }


        //Procesando la información obtenida de los formularios;
        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS)
        {
            //Validando el modelo ;
            if (!ModelState.IsValid)
            {
                //Si hay errores ;

                //Llamando al método  LlenarSexo();
                LlenarSexo();

                //pasando Lista voy a pasar su valor a la vista mediante un ViewBag
                ViewBag.lista = listaSexo;

                //si no es correcto redirige a la misma ventana 
                //con la información llenada hasta el momento
                return View(oClienteCLS);
            }
            //Caso contrario;
            else
            {
                //Abriendo la conexión la base de datos;
                using (var bd = new BDPasajeEntities())
                {
                    //Accediento al  modelo de Entity Framework;
                    Cliente Ocliente = new Cliente();

                    //Pasando los datos obtenidos de la vista con Ayuda de la clase a Entity Framework;
                    Ocliente.NOMBRE = oClienteCLS.nombre;
                    Ocliente.APPATERNO = oClienteCLS.appaterno;
                    Ocliente.APMATERNO = oClienteCLS.apmaterno;
                    Ocliente.EMAIL = oClienteCLS.Email;
                    Ocliente.DIRECCION = oClienteCLS.direccion;
                    Ocliente.IIDSEXO = oClienteCLS.idsexo;
                    Ocliente.TELEFONOCELULAR = oClienteCLS.telefonocelular;
                    Ocliente.TELEFONOFIJO = oClienteCLS.telefonofijo;
                    Ocliente.BHABILITADO = 1;
                    //Añadiendo los datos a nuestro objeto EF
                    bd.Cliente.Add(Ocliente);

                    //Guardando los datos;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        //Eliminacion logica cliente
        public ActionResult Eliminar(int idcliente) 
        {
            using (var bd = new BDPasajeEntities() ) 
            {

                //accediento al primer registro
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idcliente)).First();

                //Cambiando el estado;
                oCliente.BHABILITADO = 0;

                //Guardando los cambios;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

















    }
}