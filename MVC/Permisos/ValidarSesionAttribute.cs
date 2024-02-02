using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute  // Referencia MVC System
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)    // Logica de filtro Permisos
        {

            if (HttpContext.Current.Session["usuario"] == null)  // Si el Usuario en la Sesión es NULO
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");   // Se redirige a la página de Login
            }

            base.OnActionExecuting(filterContext);
        }
    }
}