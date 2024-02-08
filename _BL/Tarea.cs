using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BL
{
    public class Tarea
    {                                     // ADD Tarea
        public static _ML.Result AddTarea(_ML.Tarea tarea)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using (_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    SqlParameter Titulo = new SqlParameter("Titulo", tarea.Titulo);
                    SqlParameter Descripcion = new SqlParameter("Descripcion", tarea.Descripcion);
                    SqlParameter FechaInicio = new SqlParameter("FechaInicio", tarea.FechaInicio);
                    SqlParameter FechaCaducidad = new SqlParameter("FechaCaducidad", tarea.FechaCaducidad);
                    SqlParameter IdStatus = new SqlParameter("IdStatusd", tarea.estatus.IdStatus);
                    SqlParameter IdUsuario = new SqlParameter("IdUsuario", tarea.usuario.IdUsuario);

                    string task = "AddTarea @Titulo, @Descripcion, @FechaInicio, @FechaCaducidad, @IdStatus, @IdUsuario";

                    var query = context.Database.ExecuteSqlCommand(task, Titulo, Descripcion, FechaInicio, FechaCaducidad, IdStatus, IdUsuario);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = " ¡Haz Agregado una Nueva Tarea!. ";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = " ¡Lo siento!, la Tarea NO pudo ser Agregada. Intentalo más tarde.";
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

                                         // UPDATE Tarea
        public static _ML.Result UpdateTarea(_ML.Tarea tarea)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    SqlParameter Titulo = new SqlParameter("Titulo", tarea.Titulo);
                    SqlParameter Descripcion = new SqlParameter("Descripcion", tarea.Descripcion);
                    SqlParameter FechaInicio = new SqlParameter("FechaInicio", tarea.FechaInicio);
                    SqlParameter FechaCaducidad = new SqlParameter("FechaCaducidad", tarea.FechaCaducidad);
                    SqlParameter IdStatus = new SqlParameter("IdStatus", tarea.estatus.IdStatus);
                    SqlParameter IdUsuario = new SqlParameter("IdUsuario", tarea.usuario.IdUsuario);

                    string task = "UpdateTarea, @Titulo, @Descripcion, @FechaInicio, @FechaCaducidad, @IdStatus, @IdUsuario";

                    var query = context.Database.ExecuteSqlCommand(task, Titulo, Descripcion, FechaInicio, FechaCaducidad, IdStatus, IdUsuario);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = " ¡Haz Actualizado esta Tarea de forma Correcta!. ";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = " ¡Lo Siento!, LA Tarea NO fue Actualizada. ";
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

                                         // DELETE Tarea
        public static _ML.Result DeleteTarea(int IdTarea)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    SqlParameter idTarea = new SqlParameter("@IdTarea", IdTarea);

                    var query = context.Database.ExecuteSqlCommand($"TareaFinalizada{ IdTarea}");
                
                    if( query > 0 )
                    {
                        result.Correct = true;
                        result.Message = "Se Eliminó la Tarea.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "¡Ops!, La Tarea NO pudo ser Eliminada.";
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

                                         // GETALL Tarea
        public static _ML.Result GetAllTarea()
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    var query = (from tareaLINQ in context.Tareas
                                 select new
                                 {
                                     IdTarea = tareaLINQ.IdTarea,
                                     Titulo = tareaLINQ.Titulo,
                                     Descripcion = tareaLINQ.Descripcion,
                                     FechaInicio = tareaLINQ.FechaInicio,
                                     FechaCaducidad = tareaLINQ.FechaCaducidad,
                                     IdStatus = tareaLINQ.IdStatus,
                                     IdUsuario = tareaLINQ.IdUsuario

                                 }).ToList();

                    if(query != null)
                    {
                        if(query.Count > 0)
                        {
                            result.Object = new List<object>();

                            foreach (var item in query)
                            {
                                _ML.Tarea tarea = new _ML.Tarea();

                                tarea.IdTarea = item.IdTarea;
                                tarea.Titulo = item.Titulo;
                                tarea.Descripcion = item.Descripcion;
                                tarea.FechaInicio = item.FechaInicio.Value;
                                tarea.FechaCaducidad = item.FechaCaducidad.Value;

                                // Estatus
                                tarea.estatus.IdStatus = item.IdStatus.Value;

                                // Usuario
                                tarea.usuario.IdUsuario = item.IdUsuario.Value;

                                result.Objects.Add(tarea);

                                result.Correct = true;
                                result.Message = " ¡Tabla de Tareas Consultada con Exito! ";
                            }
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = " ¡Ops!, La Tabla de Tareas esta vacia. ";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = " NO se ha podido consultar la Tabla de Tareas. Intentalo más tarde.";
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

                                         // GET BY ID Tarea
        public static _ML.Result GetByIdTarea(int IdTarea)
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using (_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    var query = (from tareaLINQ in context.Tareas
                                 where tareaLINQ.IdTarea == IdTarea
                                 select new
                                 {
                                     IdTarea = tareaLINQ.IdTarea,
                                     Titulo = tareaLINQ.Titulo,
                                     Descripcion = tareaLINQ.Descripcion,
                                     FechaInicio = tareaLINQ.FechaInicio,
                                     FechaCaducidad = tareaLINQ.FechaCaducidad,
                                     IdStatus = tareaLINQ.IdStatus,
                                     IdUsuario = tareaLINQ.Usuario.IdUsuario

                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        var item = query;

                        _ML.Tarea tarea = new _ML.Tarea();

                        tarea.IdTarea = item.IdTarea;
                        tarea.Titulo = item.Titulo;
                        tarea.Descripcion = item.Descripcion;
                        tarea.FechaInicio = item.FechaInicio.Value;
                        tarea.FechaCaducidad = item.FechaCaducidad.Value;
                        tarea.estatus.IdStatus = (int)item.IdStatus;
                        tarea.usuario.IdUsuario = item.IdUsuario;

                        result.Object = tarea;

                        result.Correct = true;
                        result.Message = " ¡Se ha encontrado el Registro con Exito! ";

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "¡Ops!, NO se ha encontrado la Tarea. ";
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
    }
}
