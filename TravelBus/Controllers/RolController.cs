using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBus.Models;

namespace TravelBus.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {

            //Lista tipo Rol CLS
            List<RolCLS> listaRol = new List<RolCLS>();

            //Abriendo la conexion;
            using (var bd = new BDPasajeEntities() ) 
            {
                //Listando aquellos roles que tenga el estado habilitado;
                listaRol = (from rol in bd.Rol
                            where rol.BHABILITADO == 1
                            select new RolCLS
                            {
                                iidRol = rol.IIDROL,
                                nombre = rol.NOMBRE,
                                descripcion = rol.DESCRIPCION

                            }).ToList();
            
            }//Cerrando cadena conexion
            return View(listaRol);
        }//Fin del método Index


        public ActionResult Filtro(string nombre)
        {
            //Lista tipo Rol CLS
            List<RolCLS> listaRol = new List<RolCLS>();

            //Abriendo la conexion;
            using (var bd = new BDPasajeEntities())
            {

                if(nombre == null) 
                
                    //Listando aquellos roles que tenga el estado habilitado;
                    listaRol = (from rol in bd.Rol
                                where rol.BHABILITADO == 1
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION
                                }).ToList();
                //Fin del condicional
                else 
                
                    //Listando aquellos roles que tenga el estado habilitado;
                    listaRol = (from rol in bd.Rol
                                where rol.BHABILITADO == 1
                                && rol.NOMBRE.Contains(nombre)
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION
                                }).ToList();
                
  
            }//Fin de la conexión
            return PartialView("_TablaRol", listaRol);
        }//Fin del método Filtro


        //Método guardar()
        public int Guardar(RolCLS oRolCLS,int titulo)
        {

            int rpta = 0;

            using(var bd = new BDPasajeEntities() ) 
            {
            
                if(titulo.Equals(1) )
                {

                    Rol oRol = new Rol();

                    oRol.NOMBRE = oRolCLS.nombre;
                    oRol.DESCRIPCION = oRolCLS.descripcion;

                    oRol.BHABILITADO = 1;

                    bd.Rol.Add(oRol);

                    rpta=bd.SaveChanges();
                }                   
            }
            return rpta;    
        }

    }
}