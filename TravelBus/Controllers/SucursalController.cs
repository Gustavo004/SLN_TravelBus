using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBus.Models;

namespace TravelBus.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index(SucursalCLS osucursalCLS)
        {

            string nombreSucursal = osucursalCLS.nombre;

            //Abriendo la conexion a la base de datos y creando una lista tipo Sucursal;  
            List<SucursalCLS> ListarSucursal = null;
                  
                using (var bd = new BDPasajeEntities())
                {
                    if (osucursalCLS.nombre == null) 
                    {
                    ListarSucursal = (from sucursal in bd.Sucursal
                                      where sucursal.BHABILITADO == 1   //clausula tipo Where para traer solo los elementos que tengan el estado "1"
                                      select new SucursalCLS
                                      {
                                          idsucursal = sucursal.IIDSUCURSAL,
                                          nombre = sucursal.NOMBRE,
                                          telefono = sucursal.TELEFONO,
                                          email = sucursal.EMAIL
                                      }).ToList();
                    }else 
                    {
                    ListarSucursal = (from sucursal in bd.Sucursal
                                      where sucursal.BHABILITADO == 1   //clausula tipo Where para traer solo los elementos que tengan el estado "1"
                                      && sucursal.NOMBRE.Contains(nombreSucursal)
                                      select new SucursalCLS
                                      {
                                          idsucursal = sucursal.IIDSUCURSAL,
                                          nombre = sucursal.NOMBRE,
                                          telefono = sucursal.TELEFONO,
                                          email = sucursal.EMAIL
                                      }).ToList();
                     }                 
                }
       
            return View(ListarSucursal);
        }


        //Agregar vista html;
        public ActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Agregar(SucursalCLS oSucursalCLS)
        {
            //Validacion para que no se repita las direccioens de sucursales;

            //Contador de registros encontrados;

            int nregistrosEncontrados = 0;

            //Pasando de nombre de la clase a una variable;
            string direccionSucursal = oSucursalCLS.direccion;


            //Abriendo la cadena de conexion;
            using (var bd = new BDPasajeEntities() )
            {
                //contando la cantidad de registros asociados a la direccion Sucursal;
                nregistrosEncontrados = bd.Sucursal.Where(p => p.DIRECCION.Equals(direccionSucursal)).Count();

            }

            //Validando modelo;
            //Mandando a llamar la validacion en el lado servidor;
            if (!ModelState.IsValid || nregistrosEncontrados>=1)
            {
                if (nregistrosEncontrados>=1) 
                {

                  oSucursalCLS.mensajeErrorSucursal = "Esta dirección de sucursal ya existe";
                  return View(oSucursalCLS);//Si el modelo no es valido retornamos la misma vista con los datos ingresados;
                }
            }
            else
            {
                //Si es correcto hacemos los siguente; 

                //Abrimos la conexión
                using (var bd = new BDPasajeEntities())
                {
                    //Creamos un objeto en Entity Framework
                    Sucursal oSucursal = new Sucursal();

                    //Pasamos los datos o valores de la clase a EF;
                    oSucursal.NOMBRE = oSucursalCLS.nombre;
                    oSucursal.DIRECCION = oSucursalCLS.direccion;
                    oSucursal.TELEFONO = oSucursalCLS.telefono;
                    oSucursal.EMAIL = oSucursalCLS.email;
                    oSucursal.FECHAAPERTURA = (DateTime)oSucursalCLS.fechaApertura;
                    oSucursal.BHABILITADO = 1;

                    //Añadimos los datos;
                    bd.Sucursal.Add(oSucursal);

                    //Guardamos los datos;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //HTML Para Editar que SIEMPRE recibira un ID;
        public ActionResult Editar(int id)
        {
            //Declaramos el modelo;
            SucursalCLS oSucursalCLS = new SucursalCLS();
           
            //Abrimos la conexion a nuestra base de datos;
            using(var bd = new BDPasajeEntities() ) {
                //Creamos un Objeto EF tipo Sucursal para recuperar el ID;
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();

                //Llenamos el modelo de nuestra clase con ayuda de EF;
                oSucursalCLS.idsucursal = oSucursal.IIDSUCURSAL;
                oSucursalCLS.nombre = oSucursal.NOMBRE;
                oSucursalCLS.direccion = oSucursal.DIRECCION;
                oSucursalCLS.telefono = oSucursal.TELEFONO;
                oSucursalCLS.email = oSucursal.EMAIL;
                oSucursalCLS.fechaApertura =(DateTime)oSucursal.FECHAAPERTURA;
                 }

            return View(oSucursalCLS); //Retomamos la vista con el modelo;    
        }

        [HttpPost]
        public ActionResult Editar(SucursalCLS oSucursalCLS) 
        {

            //Validacion para que no se repita la direccion de Sucursales;

            //contador de registros encontrados;
            int nregistrosEncontrados = 0;

            //Pasando el nombre de una clase a una variable;
            string direccionSucursal = oSucursalCLS.direccion;

            //Obteniendo el id de mi sucursal
            int idsucursal = oSucursalCLS.idsucursal;

            using (var bd = new BDPasajeEntities())
            {
                nregistrosEncontrados = bd.Sucursal.Where(p => p.DIRECCION.Equals(direccionSucursal) && !p.IIDSUCURSAL.Equals(idsucursal)).Count();
            }



            if (!ModelState.IsValid || nregistrosEncontrados>=1 ) 
            {
                if (nregistrosEncontrados >= 1) oSucursalCLS.mensajeErrorSucursal = "La dirección ya existe";
                return View(oSucursalCLS);
            }

            int idSucursal = oSucursalCLS.idsucursal;
            using (var bd = new BDPasajeEntities() ) 
            {

             Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idSucursal)).First();

                oSucursal.NOMBRE = oSucursalCLS.nombre;
                oSucursal.DIRECCION = oSucursalCLS.direccion;
                oSucursal.TELEFONO = oSucursalCLS.telefono;
                oSucursal.EMAIL = oSucursalCLS.email;
                oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;

                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Eliminar(int idsucursal) 
        {
            using(var bd= new BDPasajeEntities() ) 
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idsucursal)).First();

                //Eliminación Lógica
                oSucursal.BHABILITADO = 0;

                bd.SaveChanges();

                return RedirectToAction("Index");
            }
        }


    }
}