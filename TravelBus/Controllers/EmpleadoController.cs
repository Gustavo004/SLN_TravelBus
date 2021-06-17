using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBus.Models;

namespace TravelBus.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index(EmpleadoCLS oEmpleadoCLS)
        {

            //Lista de empleados para pasarla a la vista;
            List<EmpleadoCLS> ListaEmpleados = null;

            //Llamando al combo;
            ListarComboTipoUsuario();

            //Obteniendo el ID tipo Usuario;
            int iidtipoUsuario = oEmpleadoCLS.iidtipoUsuario;


            //Abriendo Conexion
            using (var bd = new BDPasajeEntities())
            {

                if (iidtipoUsuario == 0)
                {
                    //Usando la lista Empleado crear un Join

                    /* ¿Qué es lo que hice?
                     * ===================
                     * 1)cree una variable empleado que recorre la tabla Empleado
                     * 2)hice un Join y luego creé una variable tipousuario e hice que recorriera la tabla TipoUsuario
                     * 3)hice un match entre ambas tablas de mi base de datos(Empleado y tipo usuario )

                     * Lo mismo pero ahora con la tabla Tipo Contrato
                     * ==============================================
                     * 4) Creé una variable empleado que recorre la tabla Empleado
                     * 5) hice un join cree una variable tipousario y luego lo recorri con la tabla TipoUsuario
                     * 6) Hice un match entre los atributos comunes de ambas tablas (Empleado y TipoContrato)

                     Nota:Las propiedades adicionales son las que tendra nombre en tu vista y se crea en el modelo;

                    Luego guardas los nombres en la clase EmpleadoCLS(Esta en tu modelo definido con las etiquetas)

                     */

                    //Primer Join
                    ListaEmpleados = (from empleado in bd.Empleado
                                      join tipousuario in bd.TipoUsuario
                                      on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO

                                      //Segundo Join
                                      join tipocontrato in bd.TipoContrato
                                      on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO

                                      //Condicion Where:
                                      where empleado.BHABILITADO == 1

                                      select new EmpleadoCLS
                                      {
                                          iidempleado = empleado.IIDEMPLEADO,
                                          nombre = empleado.NOMBRE,
                                          apPaterno = empleado.APPATERNO,
                                          nombreTipoUsuario = tipousuario.NOMBRE,
                                          nombreTipoContrato = tipocontrato.NOMBRE

                                      }).ToList();
                }
                else
                {
                    ListaEmpleados = (from empleado in bd.Empleado
                                      join tipousuario in bd.TipoUsuario
                                      on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO

                                      //Segundo Join
                                      join tipocontrato in bd.TipoContrato
                                      on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO

                                      //Condicion 1: Where:
                                      where empleado.BHABILITADO == 1

                                      //Condicion 2 : listo lo que el usuario selecciono 
                                      && empleado.IIDTIPOUSUARIO == iidtipoUsuario

                                      select new EmpleadoCLS
                                      {
                                          iidempleado = empleado.IIDEMPLEADO,
                                          nombre = empleado.NOMBRE,
                                          apPaterno = empleado.APPATERNO,
                                          nombreTipoUsuario = tipousuario.NOMBRE,
                                          nombreTipoContrato = tipocontrato.NOMBRE

                                      }).ToList();
                }                     
            }
         return View(ListaEmpleados);
        }


     
        //Vista Agregar HTML;
        public ActionResult Agregar() 
        {
            //Pasamos la informacion obtenida de los 3 combos;
            ListarTodosLosCombos();

            return View();
        }



        [HttpPost]
        //Procesando la infomración obtenida del formulario;
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS) //Formulario Real 
        {
            //Validando que el modelo este correcto;

            if (!ModelState.IsValid)
            {

                //Llamando de nuevo a los combo box para evitar errores si no se rellena;
                ListarTodosLosCombos();
                return View(oEmpleadoCLS); //Retornamos la vista con los datos guardados;
            }

            //Si todo esta correcto
            using (var bd = new BDPasajeEntities())
            {
                //Creando un objeto EF tipo Empleado
                Empleado oEmpleado = new Empleado();

                oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno;
                oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                oEmpleado.SUELDO = oEmpleadoCLS.sueldo;

                //COMBO BOX;
                oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;

                //Pasando valores por defecto;
                oEmpleado.BHABILITADO = 1;


                //Pasando los valores del Objeto Empleado;
                bd.Empleado.Add(oEmpleado);


                //Guardamos los datos;
                bd.SaveChanges();

            }
            return RedirectToAction("Index");
        }





        //Vista Editar HTML;
        public ActionResult Editar(int id)
        {
            //Pasamos la informacion obtenida de los 3 combos;
            ListarTodosLosCombos();

            //Instancio mi modelo;
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();

            //Abriendo la conexion con la base de datos
            using (var bd = new BDPasajeEntities() ) 
            {
             Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();

                //oEmpleado esta lleno 
                //todo lo que pongo en la izq lo almacena en la derecha;
       
                oEmpleadoCLS.nombre = oEmpleado.NOMBRE;
                oEmpleadoCLS.apPaterno = oEmpleado.APPATERNO;
                oEmpleadoCLS.apMaterno = oEmpleado.APPATERNO;
                oEmpleadoCLS.fechaContrato = (DateTime)oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.sueldo = (decimal)oEmpleado.SUELDO;
                oEmpleadoCLS.iidempleado = oEmpleado.IIDEMPLEADO;

                oEmpleadoCLS.iidtipoUsuario =(int) oEmpleado.IIDTIPOUSUARIO;
                oEmpleadoCLS.iidtipoContrato =(int)oEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.iidSexo = (int)oEmpleado.IIDSEXO;

            }
            return View(oEmpleadoCLS);
        }


        //Método real para agregar 
        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {
            //obteniendo el id del Empleado
            int idEmpleado = oEmpleadoCLS.iidempleado;

            //Validando el modelo;
            if (!ModelState.IsValid) 
            {
                //Si no es un modelo correcto retornamos la vista;
                return View (oEmpleadoCLS);
            }


            //Abriendo las conexiones;
            using (var bd = new BDPasajeEntities() ) 
            {
             //Recuperamos el ID del Empleado;
            Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(idEmpleado)).First();

                oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno;
                oEmpleado.FECHACONTRATO = (DateTime)oEmpleadoCLS.fechaContrato;
                oEmpleado.SUELDO = (decimal)oEmpleadoCLS.sueldo;
                oEmpleado.IIDEMPLEADO = oEmpleadoCLS.iidempleado;

                oEmpleado.IIDTIPOUSUARIO = (int)oEmpleadoCLS.iidtipoUsuario;
                oEmpleado.IIDTIPOCONTRATO = (int)oEmpleadoCLS.iidtipoContrato;
                oEmpleado.IIDSEXO = (int)oEmpleadoCLS.iidSexo;

                bd.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int iidempleado) 
        {

            //Abriendo la cadena conexion
            using (var bd = new BDPasajeEntities() ) 
            {
                //Capturando el registro seleccionado
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(iidempleado)).First();

                //Cambiando el estado 
                oEmpleado.BHABILITADO = 0;

                bd.SaveChanges();            
            }
            return RedirectToAction("Index");
        }



        //Funciones Llenar Combo Sexo;
        public void ListarComboSexo() 
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;

            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {

                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from sexo in bd.Sexo
                         where sexo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sexo.NOMBRE,    //Valor mostrado para el usuario
                             Value = sexo.IIDSEXO.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
              lista.Insert(0, new SelectListItem { Text = "Seleccione",Value=""});
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaSexo = lista;
            }
       }


        //Funciones Llenar Combo Contrato;
        public void ListarComboTipoContrato()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;

            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {

                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from tipcontrato in bd.TipoContrato
                         where tipcontrato.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipcontrato.NOMBRE,    //Valor mostrado para el usuario
                             Value = tipcontrato.IIDTIPOCONTRATO.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaTipoContrato = lista;
            }
        }


        //Funciones Llenar Combo TipoUsuario;
        public void ListarComboTipoUsuario()
        {
            //Agregar;
            //La lista SelecItemEsNecesaria para llenar el combo;
            List<SelectListItem> lista;

            //Abrimos la conexion
            using (var bd = new BDPasajeEntities())
            {

                //Recorremos los valores de la tabla con el estado "1" y lo guardamos en la lista;
                lista = (from tipousuario in bd.TipoUsuario
                         where tipousuario.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipousuario.NOMBRE,    //Valor mostrado para el usuario
                             Value = tipousuario.IIDTIPOUSUARIO.ToString()   //Valor real que se va a Almacenar;
                         }).ToList();
                //Mostrará como primer item la palabra seleccione
                lista.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
                //El ViewBag permite poner información del Controller a la vista;
                ViewBag.listaTipoUsuario = lista;
            }
        }


        //Funcion que almacena toda la informacion obtenida de los combos 
        public void ListarTodosLosCombos()
        {
            ListarComboTipoUsuario();
            ListarComboTipoContrato();
            ListarComboSexo();
        }



    }
}