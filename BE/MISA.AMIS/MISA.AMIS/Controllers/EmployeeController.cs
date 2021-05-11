using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region property

        private IEmployeeService _employeeService;
        private IEmployeeRepository _employeeRepository;

        #endregion

        #region constructor
        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository) : base(employeeService, employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }
        #endregion
        #region method

        /// <summary>
        /// Lọc dữ liệu
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc dữ liệu</param>
        /// <returns>danh sách đã lọc:
        /// 200- thanh cong
        /// 204 - No content
        /// 400 - loi client
        /// 500 - loi server
        /// </returns>
        /// CreatedBy: NVDAT(10/05/2021)
        [HttpGet("Filter")]
        public IActionResult GetEmployeeFilter([FromQuery] EmployeeFilter employeeFilter)
        {
            var res = _employeeService.GetEmployeesFilter(employeeFilter);
            if (res.data.Any() && res.TotalRecord != null)
            {
                return Ok(res);
            }
            return NoContent();
        }
        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>
        /// 200 - Có dữ liệu trả về.
        /// 500 - Lỗi server.
        /// </returns>
        /// CreatedBy: NVDAT (11/05/2021)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            return Ok(_employeeService.GetNewEmployeeCode());
        }
        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc danh sách nhân viên.</param>
        /// <returns>
        /// 200 - thành công.
        /// 500 - lỗi server.
        /// </returns>
        /// CreatedBy: NVDAT(11/05/2021)
        [HttpGet("Export")]
        public IActionResult Export([FromQuery] EmployeeFilter employeeFilter)
        {
            var stream = _employeeService.ExportExcel(employeeFilter);
            string excelName = $"Danh-sach-nhan-vien-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        #endregion
    }
}
