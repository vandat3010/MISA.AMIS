using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc nhân viên</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NVDAT(10/05/2021)
        public Pagging<Employee> GetEmployeesFilter(EmployeeFilter employeeFilter);

        /// <summary>
        /// Tạo mã nhân viên mới.
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// CreatedBy: NVDAT (11/05/2021)
        public string GetNewEmployeeCode();
        /// <summary>
        /// Export file excel danh sách nhân viên có bộ lọc
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc</param>
        /// <returns>Stream</returns>
        /// CreatedBy: NVDAT (11/05/2021)
        public Stream ExportExcel(EmployeeFilter employeeFilter);
    }
}
