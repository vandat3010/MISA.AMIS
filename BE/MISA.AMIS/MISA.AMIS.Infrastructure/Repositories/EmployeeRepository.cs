using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enum;
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
    public class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration): base(configuration)
        {
              
        }

        /// <summary>
        /// Kiểm tra trùng attribute của đối tượng nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã code nhân viên</param>
        /// <param name="employeeId">Mã ID nhân viên</param>
        /// <param name="http">Phương thứ PUT hay POST</param>
        /// <param name="attributeValue">Giá trị của attribute</param>
        /// <returns>TRUE hoặc FALSE</returns>
        public bool CheckEmployeeAttributeExist(string attribute, Guid? employeeId, HTTPType http, string attributeValue)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                if (http == HTTPType.POST)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@employeeId", null);
                }
                else if (http == HTTPType.PUT)
                {
                    parameters.Add($"@{attribute}", attributeValue);
                    parameters.Add("@employeeId", employeeId);
                }
                var sqlCommand = $"Proc_Check{attribute}Exist";
                var check = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, parameters, commandType: CommandType.StoredProcedure);
                return check;
            }
        }
    }
}
