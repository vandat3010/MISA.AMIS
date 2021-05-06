using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Thông tin phòng ban bộ phận
    /// </summary>
    /// CreatedBy: NVDAT(06/05/2021)
    public class Department: Base
    {
        /// <summary>
        /// Id bộ phân phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên bộ phận phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// trách nhiệm đơn vị
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Quyền lợi của đơn v
        /// </summary>
        public string Profit { get; set; }
    }
}
