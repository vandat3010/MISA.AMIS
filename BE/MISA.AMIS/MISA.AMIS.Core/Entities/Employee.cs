using MISA.AMIS.Core.EmplAttribute;
using MISA.AMIS.Core.Enum;
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
        [PropertyRequired("")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [PropertyRequired("")]
        [PropertyMaxLength(50, "")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        public GenderEnum? Gender { get; set; }

        /// <summary>
        /// ID bộ phận, phòng ban
        /// </summary>
        [PropertyRequired("")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Số CMTND, CCCD
        /// </summary>
        public string IdentifyNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime DateOfIN { get; set; }
        /// <summary>
        /// Địa điểm cấp
        /// </summary>
        public string PlaceOfIN { get; set; }
        /// <summary>
        /// Vị trí chức vụ nhân viên
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// SĐT
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// SĐT cố định
        /// </summary>
        public string TelephoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        
        public string Email { get; set; }
        /// <summary>
        /// số tài khoản
        /// </summary>
        public string BankAccountNumber { get; set; }
        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// tên chi nhánh
        /// </summary>
        public string BankBranchName { get; set; }
        /// <summary>
        /// tỉnh thành
        /// </summary>
        public string BankProvinceName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string GenderName
        {
            get
            {
                return Gender switch
                {
                    GenderEnum.MALE => "Nam",
                    GenderEnum.FEMALE => "Nữ",
                    GenderEnum.OTHER => "Khác",
                    _ => "Không xác định"
                };
            }
        }

    }
}
