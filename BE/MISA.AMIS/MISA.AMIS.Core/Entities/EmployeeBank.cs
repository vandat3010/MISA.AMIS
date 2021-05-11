using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// thông tin tài khoản ngân hàng
    /// </summary>
    /// CreatedBy: NVDAT(07/05/2021)
    public class EmployeeBank: Base
    {
        /// <summary>
        /// Id tài khoản ngân hàng
        /// </summary>
        public Guid EmployeeBankId { get; set; }

        /// <summary>
        /// số tài khoản
        /// </summary>
        public string EmployeeBankNumber { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// chi nhánh
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// trạng thái của tài khoản
        /// </summary>
        public int StatusAccount { get; set; }
    }
}
