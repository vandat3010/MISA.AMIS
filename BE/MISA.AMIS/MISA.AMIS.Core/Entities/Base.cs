using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Thông rin chung
    /// </summary>
    /// CreatedBy: NVDAT(07/05/2021)
    public class Base
    {
        /// <summary>
        /// Người thêm vào.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày thêm vào.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa lần cuối.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa lần cuối.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
