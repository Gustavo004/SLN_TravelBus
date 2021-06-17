using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBus.Models;

namespace TravelBus.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index(MarcaCLS omarcaCLS)
        {
            //Variable empleada para la busqueda dentro de mi sentencia if-else
            String nombreMarca = omarcaCLS.nombre;

            //definiendo una variable tipo lista para poder retonarlo en la vista;
            List<MarcaCLS> ListaMarca = null;

            //Si mi método index no recibe ninguna variable,listo todos;

            if (omarcaCLS.nombre == null)
            {
                //creamos una conexión para la base de datos 
                using (var bd = new BDPasajeEntities())
                {
                    //hacemos un Listado;
                    ListaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1 //clausula where 

                                  select new MarcaCLS

                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION
                                  }).ToList();
                }
            }
            else 
            {
                //Cuando hay parametros que buscar;
                //creamos una conexión para la base de datos 
                using (var bd = new BDPasajeEntities())
                {
                    //hacemos un Listado;
                    ListaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1 //clausula where 
                                  && marca.NOMBRE.Contains(nombreMarca)

                                  select new MarcaCLS

                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION
                                  }).ToList();
                }
            }          
           return View(ListaMarca);
        }

        //Creamos el método para la vista la pantalla lo que el usuario ve o sea el html ;
        public ActionResult Agregar()
        {

            return View();
        }


        //Creamos el codigo backend para la inserccion ;
        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS) //Esto es para el formulario
        {
            //Validacion para que no se repita el nombre de marcas;
           
            //contador de registros encontrados;
            int nregistrosEncontrados = 0;

            //Pasando de nombre de la clase a una variable;
            string nombreMarca = oMarcaCLS.nombre; 


            using (var bd = new BDPasajeEntities() ) 
            {
                nregistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();

            }

                //Validando el modelo 
                if (!ModelState.IsValid || nregistrosEncontrados>=1)
                {
                                
                               if (nregistrosEncontrados >= 1)
                               { 
                                oMarcaCLS.mensajeError = "El nombre ya existe"; 
                                return View(oMarcaCLS); //Si no es correcto devuelve la misma vista; 
                               }
                 }
                else
                {
                    //Si es correcto;

                    using (var bd = new BDPasajeEntities())
                    {
                        //Esta clase pertenece a Entity Framework;
                        Marca oMarca = new Marca();

                        //Agregando varoles;
                        oMarca.NOMBRE = oMarcaCLS.nombre;
                        oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                        oMarca.BHABILITADO = 1;

                        //Agregando datos al modelo ;
                        bd.Marca.Add(oMarca);

                        //Guardando datos;
                        bd.SaveChanges();
                    }
                }
            return RedirectToAction("Index");
        }



        //Vista HTML EDITAR
        public ActionResult Editar(int id)
        {
            //Declaramos el modelo;
            MarcaCLS oMarcaCLS = new MarcaCLS();
            //Abrimos la conexión
            using (var bd = new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                //Llenamos el modelo;
                oMarcaCLS.iidmarca = oMarca.IIDMARCA;
                oMarcaCLS.nombre = oMarca.NOMBRE;
                oMarcaCLS.descripcion = oMarca.DESCRIPCION;
            }
            return View(oMarcaCLS); //Pasamos el modelo a nuestra vista;       
        }

        [HttpPost]
        public ActionResult Editar(MarcaCLS oMarcaCLS)
        {
            //contador de registros encontrados
            int nregistrosEncontrados = 0;

            string nombreMarca = oMarcaCLS.nombre;

            int iidmarca = oMarcaCLS.iidmarca;

            //llamada a la bd para ver si se repite o no 
            using (var bd = new BDPasajeEntities() ) 
            {
                nregistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)&& !p.IIDMARCA.Equals(iidmarca)).Count();
            }


            //Validando el modelo ;si no es correcto retomos los mismos datos con los valores obtenidos;
            if (!ModelState.IsValid || nregistrosEncontrados>=1)
            {

                if (nregistrosEncontrados >= 1) oMarcaCLS.mensajeError = "Ya se encuentra registrada la marca";
                return View(oMarcaCLS);
            }


            //Recuperando el id de mi modelo;
            int idMarca = oMarcaCLS.iidmarca;

            //Abrimos una conexion
            using (var bd = new BDPasajeEntities())
            {

                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();//Obtenemos  solo id

                //Valor que esta en la base de datos|valor que se almacena en mi vista y que lo pasamos al controller
                oMarca.NOMBRE = oMarcaCLS.nombre;
                oMarca.DESCRIPCION = oMarcaCLS.descripcion;

                bd.SaveChanges();//Guardamos los datos;

            }

            return RedirectToAction("Index");
        }


        //Eliminacion de datos:
        public ActionResult Eliminar(int iidmarca)
        {
            using (var bd = new BDPasajeEntities() ) 
            {

                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(iidmarca)).First();

                //Eliminacion Lógica;
                oMarca.BHABILITADO = 0;

                bd.SaveChanges();
            }
            return RedirectToAction("Index");         
        }
   }
}