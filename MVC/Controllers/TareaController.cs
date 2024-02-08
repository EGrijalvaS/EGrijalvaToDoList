using MVC.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
   // [ValidarSesion]    
    // Vlidar Acceso a estas vistas por medio del Login
    public class TareaController : Controller
    {
        // GET: Tarea

        [HttpGet]
        public ActionResult GetAll()
        {
            _ML.Tarea tarea = new _ML.Tarea();
            tarea.Tareas = new List<object>(); // Lista de tareas 
            _ML.Result result = new _ML.Result();

            // Condicional
            if (result.Correct)
            {
                tarea.Tareas = result.Objects;
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
            }

            return View(tarea);
        }

        [HttpGet]

        public ActionResult Form()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Form(int IdTarea)
        {
            _ML.Tarea tarea = new _ML.Tarea();

            tarea.estatus = new _ML.Estatus();

            if (IdTarea == 0)
            {
                ViewBag.ErrorMessage = " Tarea Agregada Correctamente";
                return View(tarea);
            }
            else
            {
                ViewBag.ErrorMessage = " La Tarea NO pudo ser Añadida.";
                return View();
            }
        }

    }
}