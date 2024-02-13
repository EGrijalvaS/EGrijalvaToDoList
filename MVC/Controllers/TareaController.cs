using MVC.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    //[ValidarSesion]    
    // Vlidar Acceso a estas vistas por medio del Login
    public class TareaController : Controller
    {
        // GET: Tarea
      
        public ActionResult GetAll()
        {
            _ML.Tarea tarea = new _ML.Tarea();

            _ML.Result result = _BL.Tarea.GetAllTarea();

            tarea.Tareas = new List<object>();

            if (result.Correct)
            {
                tarea.Tareas = result.Objects.ToList();
                return View(tarea);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(tarea);
            }
          
        }


        [HttpGet]

        public ActionResult Form(int? IdTarea)
        {
           _ML.Tarea tarea = new _ML.Tarea();

            if(IdTarea == 0 || IdTarea == null)
            {
                ViewBag.Accion = " Agregar Tarea ";
            }
            else
            {
                ViewBag.Accion = " Actualizar Tarea ";
                _ML.Result result = _BL.Tarea.GetByIdTarea(IdTarea.Value);
            }

            return View(tarea);
        }

        [HttpPost]
        public ActionResult Form(_ML.Tarea tarea)
        {
            _ML.Result result = new _ML.Result();

            if(tarea.IdTarea  == 0)
            {
                ViewBag.Accion = " Agregar ";
                result = _BL.Tarea.AddTarea(tarea);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Messge = " No se agrego " + result.Message;
                }
            }
            else
            {
                ViewBag.Accion = " Actualizar ";
                result = _BL.Tarea.UpdateTarea(tarea);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Messge = " No se Actualizo " + result.Message;
                }
            }

            return View(result);
        }

        public ActionResult Delete(int IdTarea)
        {
            ViewBag.Accion = "Eliminar";
            _ML.Result result = _BL.Tarea.DeleteTarea(IdTarea);
            ViewBag.Mensaje = result.Message;
            return View();
        }


    }
}


