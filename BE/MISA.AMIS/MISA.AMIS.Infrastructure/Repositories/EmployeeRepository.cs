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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
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
        /// createdBy: NVDAT(07/05/2021)
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

        /// <summary>
        /// lấy ra mã khách hàng lớn nhất
        /// </summary>
        /// <returns>mã khách hàng </returns>
        /// CreatedBy: NVDAT(07/08/2021)
        public string GetEmployeeCodeMax()
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlComand = $"Proc_GetEmployeeCodeMax";
                var employeeCode = dbConnection.QueryFirstOrDefault<string>(sqlComand, commandType: CommandType.StoredProcedure);
                return employeeCode;
            }
        }

        /// <summary>
        /// Danh sách có lọc
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc nhân viên</param>
        /// <returns>Danh sách nhân viên có lọc</returns>
        /// CreatedBy: NVDAT(10/05/2021)
        public Pagging<Employee> GetEmployeesFilter(EmployeeFilter employeeFilter)
        {
            var res = new Pagging<Employee>()
            {
                pageIndex = employeeFilter.pageIndex,
                pageSize = employeeFilter.pageSize
            };

            // Thiết lập kết nối DB.
            using var connection = new MySqlConnection(connectionString);

            // Tính tổng nhân viên.
            int? totalRecord = connection.QueryFirstOrDefault<int>("Proc_GetTotalEmployees", employeeFilter, commandType: CommandType.StoredProcedure);

            if (totalRecord == null)
            {
                return res;
            }

            res.TotalRecord = totalRecord;

            // Lấy danh sách nhân viên.
            var employees = connection.Query<Employee>("Proc_GetEmployeesFilter", employeeFilter, commandType: CommandType.StoredProcedure);

            res.data = employees;

            return res;
        }

        public string GetNewEmployeeCode()
        {
            //Thiết lập kết nối DB.
            using var connection = new MySqlConnection(connectionString);
            //Lấy mã nhân viên lớn nhất trên Db.
            string? maxEmployeeCode = connection.QueryFirstOrDefault<string>("Proc_MaxEmployeeCode", commandType: CommandType.StoredProcedure);
            if (maxEmployeeCode == null)
            {
                return "NV-0001";
            }
            string employeeCodeNumStr = string.Empty;
            for (var i = 0; i < maxEmployeeCode.Length; i++)
            {
                if (char.IsDigit(maxEmployeeCode[i]))
                {
                    employeeCodeNumStr += maxEmployeeCode[i];
                }
            }
            int employeeCodeNum = int.Parse(employeeCodeNumStr);
            employeeCodeNum++;
            return "NV-" + employeeCodeNum;
        }
    }
}
