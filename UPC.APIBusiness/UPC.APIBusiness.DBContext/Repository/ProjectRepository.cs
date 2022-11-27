using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBContext
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ResponseBase getProject(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase getProjects()
        {
            var returnEntity = new ResponseBase();
            var entitiesProjects = new List<EntityProject>();
            var imageRepository = new ImageRepository();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ListarProyectos";
                    entitiesProjects = db.Query<EntityProject>(sql: sql, commandType: CommandType.StoredProcedure).ToList();

                    foreach(var obj in entitiesProjects)
                    {
                        obj.images = imageRepository.getImages(obj.idProyecto);
                    }

                    if(entitiesProjects.Count > 0)
                    {
                        returnEntity.isSuccess = true;
                        returnEntity.errorCode = "0000";
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = entitiesProjects;
                    }
                    else
                    {
                        returnEntity.isSuccess = false;
                        returnEntity.errorCode = "0000";
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = null;
                    }
                }
            }
            catch(Exception ex)
            {
                returnEntity.isSuccess = false;
                returnEntity.errorCode = "0001";
                returnEntity.errorMessage = ex.Message;
                returnEntity.data = null;
            }

            return returnEntity;
        }

        public ResponseBase Insert(EntityProject project)
        {
            var returnEntity = new ResponseBase();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_InsertarProyecto";

                    var p = new DynamicParameters();
                    p.Add(name: "@IDPROYECTO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add(name: "@NOMBRE", value: project.nombre, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@PRECIO", value: project.precio, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                    p.Add(name: "@DIRECCION", value: project.direccion, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@UBICACION", value: project.ubicacion, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@IMAGENNOMBRE", value: project.images.Count > 0 ? project.images[0].nombre : string.Empty, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@IMAGENRUTA", value: project.images.Count > 0 ? project.images[0].ruta : string.Empty, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@USUARIOCREA", value: project.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityProject>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    int idProject = p.Get<int>("@IDPROYECTO");

                    if (idProject > 0)
                    {
                        returnEntity.isSuccess = true;
                        returnEntity.errorCode = "0000";
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = new
                        {
                            id = idProject,
                            nombre = project.nombre,
                            precio = project.precio
                        };
                    }
                    else
                    {
                        returnEntity.isSuccess = false;
                        returnEntity.errorCode = "0000";
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                returnEntity.isSuccess = false;
                returnEntity.errorCode = "0001";
                returnEntity.errorMessage = ex.Message;
                returnEntity.data = null;
            }
            return returnEntity;
        }
    }
}
