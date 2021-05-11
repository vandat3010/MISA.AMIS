using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IBaseService<T> where  T:class
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu bản ghi
        /// </summary>
        /// <returns>Trả về dữ liệu bản ghi</returns>
        /// CreatedBy: NVDAT(07/08/2021)
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy bản ghi có id là entityId
        /// </summary>
        /// <param name="entityId">id đối tượng</param>
        /// <returns>Trả về bản ghi có id là entityId</returns>
        /// CreatedBy: NVDAT(07/08/2021)
        public T GetById(Guid entityId);

        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>Thêm thành công bản ghi mới hay không</returns>
        /// CreatedBy: NVDAT(07/08/2021)
        public int Insert(T entity);

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>Cập nhật bản ghi thành công hay không</returns>
        /// CreatedBy: NVDAT(07/08/2021)
        public int Update(T entity);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityId">id đối tượng</param>
        /// <returns>Xóa bản ghi thành công hay không</returns>
        /// CreatedBy: NVDAT(07/08/2021)
        public int Delete(Guid entityId);

        /// <summary>
        /// Phân trang đối tượng.
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang.</param>
        /// <param name="pageIndex">Trang số bao nhiêu.</param>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NVDAT(07/08/2021)
        public IEnumerable<T> GetMISAEntities(int pageSize, int pageIndex);
    }
}
