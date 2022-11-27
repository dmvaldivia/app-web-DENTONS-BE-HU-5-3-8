using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBContext
{
    public class ImageRepository : BaseRepository, IImageRepository
    {
        public List<EntityImage> getImages(int id)
        {
            var returnEntity = new List<EntityImage>();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ListarImagesXProyecto";
                    var p = new DynamicParameters();
                    p.Add(name: "@IDPROYECTO", value: id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    
                    returnEntity = db.Query<EntityImage>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return returnEntity;
        }
    }
}