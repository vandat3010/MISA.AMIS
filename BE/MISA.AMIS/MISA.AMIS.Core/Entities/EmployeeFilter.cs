using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Bộ lọc dữ liệu
    /// </summary>
    /// CreatedBy: NVDAT(10/05/2021)
    public class EmployeeFilter
    {
        /// <summary>
        /// trang hiện tại
        /// </summary>
        public int pageIndex { get; set; } = 1;

        /// <summary>
        /// số bản ghi trên một trang
        /// </summary>
        public int pageSize { get; set; } = 10;

        /// <summary>
        /// từ khóa lọc (mã nhân viên, tên nhân viên)
        /// </summary>
        public string filter { get; set; }
    }
}
