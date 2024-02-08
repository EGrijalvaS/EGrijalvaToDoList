using _ML;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _BL
{
    public class Usuario
    {                                      // Usuario ADD
        public static _ML.Result AddUsuario(_ML.Usuario usuario)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    SqlParameter Nombre = new SqlParameter("Nombre", usuario.Nombre);
                    SqlParameter ApellidoPaterno = new SqlParameter("ApellidoPaterno", usuario.ApellidoPaterno);
                    SqlParameter ApellidoMaterno = new SqlParameter("ApellidoMaterno", usuario.ApellidoMaterno);
                    SqlParameter FechaNacimiento = new SqlParameter("FechaNacimiento", usuario.FechaNacimiento);
                    SqlParameter Correo = new SqlParameter("Correo", usuario.Correo);
                    SqlParameter Password = new SqlParameter("Password", usuario.Password);

                    string user = "AddUsuario @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNAcimiento, @Correo, @Password";

                    var query = context.Database.ExecuteSqlCommand(user, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, Correo, Password);

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = " El Usuario ha sido Agregado con Exito.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = " ¡Error!, El Usuario NO pudo ser Añadido.";
                    }
                }
            }
            catch(Exception ex) 
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

                                          // Usuario UPDATE
        public static _ML.Result UpdateUsuario(_ML.Usuario usuario)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    SqlParameter Nombre = new SqlParameter("Nombre", usuario.Nombre);
                    SqlParameter ApellidoPaterno = new SqlParameter("ApellidoPaterno", usuario.ApellidoPaterno);
                    SqlParameter ApellidoMaterno = new SqlParameter("ApellidoMaterno", usuario.ApellidoMaterno);
                    SqlParameter FechaNacimiento = new SqlParameter("FechaNacimiento", usuario.FechaNacimiento);
                    SqlParameter Correo = new SqlParameter("Correo", usuario.Correo);
                    SqlParameter Password = new SqlParameter("Password", usuario.Password);

                    string user = "UpdateUsuario @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Correo, @Password";

                    var query = context.Database.ExecuteSqlCommand(user, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, Correo, Password);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = " El Usuario fue Actualizado con Exito. ";
                    }
                    else{
                        result.Correct = false;
                        result.Message = " ¡Error!, El Usuario NO pudo ser Actualizado. ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;

        }

                                          // Usuario DELETE
        public static _ML.Result DeleteUsuario(int IdUsuario)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                //using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                //{
                //    SqlParameter idUsuario = new SqlParameter("@IdUsuario", IdUsuario);

                //    var query = context.ExcecuteSqlCommand($"DeleteUsuario{IdUsuario}");

                //    if( query > 0 )
                //    {
                //        result.Correct = true;
                //        result.Message = " El Usuario fue Eliminado Exitosamente. ";
                //    }
                //    else
                //    {
                //        result.Correct = false;
                //        result.Message = " ¡Error!, El Usuario NO pudo ser Eliminado. ";                   
                //    }
                //}
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;

        }

                                          // Usuario GETALL
        public static _ML.Result GetAllUsuario()
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    var query = (from usuarioLINQ in context.Usuarios
                                 select new
                                 {
                                     IdUsuario = usuarioLINQ.IdUsuario,
                                     Nombre = usuarioLINQ.Nombre,
                                     ApellidoPaterno = usuarioLINQ.ApellidoPaterno,
                                     ApellidoMaterno = usuarioLINQ.ApellidoMaterno,
                                     FechaNacimiento = usuarioLINQ.FechaNacimiento,
                                     Correo = usuarioLINQ.Correo,
                                     Password = usuarioLINQ.Password

                                 }).ToList();

                    if(query != null)
                    {
                        if(query .Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (var item in query)
                            {
                                _ML.Usuario usuario = new _ML.Usuario();

                                usuario.IdUsuario = item.IdUsuario;
                                usuario.Nombre = item.Nombre;
                                usuario.ApellidoPaterno = item.ApellidoPaterno;
                                usuario.ApellidoMaterno = item.ApellidoMaterno;
                                usuario.FechaNacimiento = item.FechaNacimiento.Value;
                                usuario.Correo = item.Correo;
                                usuario.Password = item.Password;

                                result.Objects.Add(usuario);

                                result.Correct = true;
                                result.Message = " Tabla de Usuarios Consultada con Exito.";
                            }
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = " La Tabla de Usuarios esta vacia.";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = " ¡Error!, NO se Encontraron Registros. ";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;

        }

                                         //  Usuario GetById
        public static _ML.Result GetByIdUsuario(int IdUsuario)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using (_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    var query = (from usuarioLINQ in context.Usuarios
                                 where usuarioLINQ.IdUsuario == IdUsuario
                                 select new
                                 {
                                     IdUsuario = usuarioLINQ.IdUsuario,
                                     Nombre = usuarioLINQ.Nombre,
                                     ApellidoPaterno = usuarioLINQ.ApellidoPaterno,
                                     ApellidoMaterno = usuarioLINQ.ApellidoMaterno,
                                     FechaNacimiento = usuarioLINQ.FechaNacimiento,
                                     Correo = usuarioLINQ.Correo,
                                     Password = usuarioLINQ.Password

                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        var item = query;

                        _ML.Usuario usuario = new _ML.Usuario();

                        usuario.IdUsuario = item.IdUsuario;
                        usuario.Nombre = item.Nombre;
                        usuario.ApellidoPaterno = item.ApellidoPaterno;
                        usuario.ApellidoMaterno = item.ApellidoMaterno;
                        usuario.FechaNacimiento = item.FechaNacimiento.Value;
                        usuario.Correo = item.Correo;
                        usuario.Password = item.Password;

                        result.Object = usuario;

                        result.Correct = true;
                        result.Message = " Registro Consultado Correctamente. ";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = " ¡Error!, NO se encontró ningun Registro. ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

                                          // Usuario
        public static Result UsuarioGetById(int v, object idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
