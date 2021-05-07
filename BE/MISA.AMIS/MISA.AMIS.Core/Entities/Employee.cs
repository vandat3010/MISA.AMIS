using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    /// CreatedBy: NVDAT(06/05/2021)
    public class Employee : Base
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// ID bộ phận, phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Số CMTND, CCCD
        /// </summary>
        public string IdentifyNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime IdentifyDate { get; set; }
        /// <summary>
        /// Địa điểm cấp
        /// </summary>
        public string IdentifyPlace { get; set; }
        /// <summary>
        /// Vị trí chức vụ nhân viên
        /// </summary>
        public string EmployeePosition { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string EmployeeAddress { get; set; }
        /// <summary>
        /// SĐT
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// SĐT cố định
        /// </summary>
        public string TeleNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

    }
}
