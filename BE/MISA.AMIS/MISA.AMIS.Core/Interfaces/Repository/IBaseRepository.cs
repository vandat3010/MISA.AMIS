using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<T> where T: class
    {
        /// <summary>
        /// lay ta ca cac du lieu
        /// </summary>
       public IEnumerable<T> GetAll();

        /// <summary>
        /// lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
       public T GetById(Guid entityId);

        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="entity">thông tin được thêm</param>
        public int Insert(T entity);

        /// <summary>
        /// cập nhật bản ghi
        /// </summary>
        /// <param name="entity">bản ghi cần cập nhật</param>
        /// <returns></returns>
        public int Update(T entity);

        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi cần xóa</param>
        /// <returns></returns>

        public int Delete(Guid entityId);
        /// <summary>
        /// Phân trang đối tượng.
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang.</param>
        /// <param name="pageIndex">Trang số bao nhiêu.</param>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (07/05/2021)
        public IEnumerable<T> GetMISAEntities(int pageSize, int pageIndex);
    }
}
