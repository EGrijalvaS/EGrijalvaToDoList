using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AccesoController : Controller
    {
        // Cadena de Conexión
        static string ConnectionString = "Data Source=LAPTOP-AD7LFKO5\\MSSQLSERVER1;Initial Catalog=EGrijalvaToDoList;User ID=sa;Password=pass@word1;Encrypt=False";

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }


        [HttpPost]    // Método / Lógica para el Registro de Usuario
        public ActionResult Registrar(_ML.Usuario usuario)
        {
            bool registrado;
            string mensaje;

            // Validar que la contraseña ingresada coincida.

            if(usuario.Password == usuario.ConfirmarPassword)
            {
                // Si NO se implementará el Hasheo de la contraseña esta se veria de la siguiente manera;

                // usuario.Password = "Contraseña123";     sin embargo, esto no es correcto, por lo tanto

                usuario.Password = ConvertSha256(usuario.Password);   // Se cambia/actualiza la contraseña ya hasheada.
            }
            else
            {
                ViewData["Message"] = " ¡Ops!, Las Contraseñas que ingresaste NO Coinciden.";
                return View();
            }


            // Método para Registrar usuario

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[RegistrarUsuario]", con);

                // Se agregan los Parametros de Entrada igual que el SP de la Base de Datos.

                cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("Password", usuario.Password);

                // Se agregan también los Parametros de Salida del SP

                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
           
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open(); // Apertura de conexión

                cmd.ExecuteNonQuery();

                // Se leen los Parametros de Salida:

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Message"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;  // Se envía el mismo mensaje que esta dentro del Stored en la BD

            if(registrado)
            {
                return RedirectToAction("Acceso", "Login");    // Si el usuario esta Registrado, el método redirecciona al usuario al Login
            }
            else
            {
                return View();
            }

        }

        // LOGIN lógica

        [HttpPost]
        public ActionResult Login(_ML.Usuario usuario)
        {
            // Encriptar la contraseña en la Interfaz

            usuario.Password = ConvertSha256(usuario.Password);

            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[ValidarUsuario]", con);  // Se agrega el Stored Procedure

                // Se agregan los Parametros de Entrada igual que el SP de la Base de Datos.

                cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("Password", usuario.Password);

                cmd.CommandType= CommandType.StoredProcedure;

                con.Open();   // Se abre la conexión

                // Se lee la primera fila y/o columna

                usuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if(usuario.IdUsuario != 0)   // Sí el Id del Usuario es diferente de 0
            {
                Session["usuario"] = usuario;  // Se genera una sesión

                return RedirectToAction("Index", "Home");   // Se redirecciona al Index que se encuentra en el Home Controller
            }
            else    // De lo contrario
            {
                ViewData["Mensaje"] = "¡Lo sentimos!, El Usuario NO ha sido Encontrado. ";   // Se envia mensaje de Error

                return View();
            }
        }

        // Método para Hashear la contraseña

        public static string ConvertSha256(string texto)    // Recibe un objeto de tipo texto
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())    // Hasheo tipo SHA256
            { 
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(texto));     // Obtiene los bytes de la cadena de texto.

                foreach (byte b in result)
                {
                    sb.Append(b.ToString());
                }

                return sb.ToString();     // Se devuelve la Contraseña Hasheada
            }
        }
    }
}