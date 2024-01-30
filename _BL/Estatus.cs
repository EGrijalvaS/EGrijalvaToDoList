using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BL
{
    public class Estatus
    {
       public static _ML.Result GetAllStatus()
        {
            _ML.Result result = new _ML.Result();

            try
            {
                using(_DL.EGrijalvaToDoListEntities context = new _DL.EGrijalvaToDoListEntities())
                {
                    var query = (from statusLINQ in context.Estatus
                                 select new
                                 {
                                     IdStatus = statusLINQ.IdStatus,
                                     Descripcion = statusLINQ.Descripcion

                                 }).ToList();

                    if (query != null){

                        if (query.Count > 0)
                        {
                            result.Object = new List<object>();

                            foreach(var item in query)
                            {
                                _ML.Estatus status = new _ML.Estatus();

                                status.IdStatus = item.IdStatus;
                                status.Descripcion = item.Descripcion;

                                result.Objects.Add(status); 

                                result.Correct = true;
                                result.Message = " Lista de Estatus Consultada Correctamente. ";
                            }
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "La lista de Estatus se encuentra vacia. ";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = " ¡Error!, NO se puede realizar la Consulta. ";
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
    }
}
