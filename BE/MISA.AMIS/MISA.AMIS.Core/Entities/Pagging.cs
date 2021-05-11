using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <typeparam name="T">thực thể</typeparam>
    /// createdBy: NVDAT(10/05/2021)
   public class Pagging<T> where T: class
    {
        /// <summary>
        /// tổng số bản ghi
        /// </summary>
        public int? TotalRecord { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int? TotalPages
        {

            get
            {
                return (int)Math.Ceiling((decimal)TotalRecord / pageSize);
            }


        }
        /// <summary>
        /// trang hiện tại
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// kích thước trang
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// dữ liệu trả ra
        /// </summary>
        public IEnumerable<T>? data { get; set; }
    }
}
