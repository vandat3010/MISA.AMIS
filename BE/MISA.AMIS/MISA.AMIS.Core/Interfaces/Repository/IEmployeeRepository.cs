using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        // <summary>
        /// Kiểm tra trùng attribute của đối tượng nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã code nhân viên</param>
        /// <param name="employeeId">Mã ID nhân viên</param>
        /// <param name="http">Phương thứ PUT hay POST</param>
        /// <param name="attributeValue">Giá trị của attribute</param>
        /// <returns>TRUE hoặc FALSE</returns>
        /// Created By: NVDAT(07/05/2021)
        public bool CheckEmployeeAttributeExist(string employeeCode, Guid? employeeId, HTTPType http, string attributeValue);

        /// <summary>
        /// Lấy ra mã Employeecode lớn nhất
        /// </summary>
        /// <returns>trả về mã khách hàng lớn nhất</returns>
        /// CreatedBy: NVDAT(08/05/2021)
        public string GetEmployeeCodeMax();

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc nhân viên</param>
        /// <returns></returns>
        ///CreatedBy: NVDAT(10/05/2021)
        public Pagging<Employee> GetEmployeesFilter(EmployeeFilter employeeFilter);

        /// <summary>
        /// Tạo mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NVDAT(11/05/2021)
        public string GetNewEmployeeCode();
    }
}
