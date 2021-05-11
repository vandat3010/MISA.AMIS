using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region property
        protected IDbConnection dbConnection;
        protected IConfiguration _configuration;
        protected string connectionString;
        protected string tableName = typeof(T).Name;
        #endregion

        #region Constructor

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("connectionDB");
        }

        #endregion

        #region Method

        /// <summary>
        /// Xoa một bản ghi theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// createdBy: NVDat(07/05/2021)

        public int Delete(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_Delete{tableName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var rowAffects = dbConnection.Execute(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowAffects;
            }
        }

        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>trả về số bản ghi có trong DB</returns>
        public IEnumerable<T> GetAll()
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_Get{tableName}s";
                var entities = dbConnection.Query<T>(sql, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId"> Id bản ghi cần lấy</param>
        /// <returns>trả về bản ghi có Id = entityId</returns>
        public T GetById(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_Get{tableName}ById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var entity = dbConnection.QueryFirstOrDefault<T>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return entity;
            }
        }

        /// <summary>
        /// Thêm mới bản ghi 
        /// </summary>
        /// <param name="entity"> Thông tin cần thêm</param>
        /// <returns>trả về thông tin bản ghi được thêm</returns>
        public int Insert(T entity)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_Insert{tableName}";
                var rowEffects = dbConnection.Execute(sql, entity, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi cần cập nhật</param>
        /// <returns>Trả về thông tin bản ghi đã được cập nhật</returns>
        /// CreatedBy: NVDAT(07/05/2021)
        public int Update(T entity)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_Update{tableName}";
                var rowEffects = dbConnection.Execute(sql, entity, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }
        /// <summary>
        /// Phân trang đối tượng.
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang.</param>
        /// <param name="pageIndex">Trang số bao nhiêu.</param>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NVDAT(07/05/2021)
        public IEnumerable<T> GetMISAEntities(int pageSize, int pageIndex)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_Get{tableName}Paging";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PageIndex", pageIndex);
                dynamicParameters.Add("@PageSize", pageSize);
                var entities = dbConnection.Query<T>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }
        #endregion
    }
}
