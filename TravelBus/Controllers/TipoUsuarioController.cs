using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TravelBus.Models;

namespace TravelBus.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private TipoUsuarioCLS oTipoVal;

        private bool buscarTipoUsuario(TipoUsuarioCLS oTipoUsuarioCLS) 
        {

            bool busquedaId = true;
            bool busquedaNombre = true;
            bool busquedaDescripcion = true;



            if (oTipoVal.iidtipousuario > 0)
                busquedaId = oTipoUsuarioCLS.iidtipousuario.ToString().Contains(oTipoVal.iidtipousuario.ToString() );

            if (oTipoVal.nombre != null)
               busquedaNombre=oTipoUsuarioCLS.nombre.ToString().Contains(oTipoVal.nombre);

            if (oTipoVal.descripcion != null)
                busquedaDescripcion = oTipoUsuarioCLS.descripcion.ToString().Contains(oTipoVal.descripcion);


            //Retorno los 3 parametros
            return (busquedaId && busquedaNombre && busquedaDescripcion);
        }


        // GET: TipoUsuario
        public ActionResult Index(TipoUsuarioCLS oTipoUsuario)
        {
            oTipoVal = oTipoUsuario;



            //Creando una lista tipo Usuario 
            List<TipoUsuarioCLS> listaTipoUsuario = null;

            //Abriendo la conexion;

            //Pongo una variable 
            List<TipoUsuarioCLS> listaFiltrado;
            using(var bd = new BDPasajeEntities() ) 
            {
                //Hacemos el listado;
                listaTipoUsuario = (from tipoUsuario in bd.TipoUsuario
                                    where tipoUsuario.BHABILITADO == 1
                                    select new TipoUsuarioCLS
                                    {
                                        iidtipousuario = tipoUsuario.IIDTIPOUSUARIO,
                                        nombre = tipoUsuario.NOMBRE,
                                        descripcion = tipoUsuario.DESCRIPCION

                                    }).ToList();

                if (oTipoUsuario.iidtipousuario == 0 && oTipoUsuario.nombre == null && 
                    oTipoUsuario.descripcion == null)
                    listaFiltrado = listaTipoUsuario;

                //Aqui hago el filtrado
                else 
                {
                    //Permite hacer filtros;
                    Predicate<TipoUsuarioCLS> pred = new Predicate<TipoUsuarioCLS>(buscarTipoUsuario);

                   listaFiltrado=listaTipoUsuario.FindAll(pred);
                }


            }
            return View(listaFiltrado); //retorno la lista;
        }
    }
}