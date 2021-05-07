using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T: class
    {
        #region property

        IBaseRepository<T> _baseRepository;
        IBaseService<T> _baseService;
        static string tableName = typeof(T).Name;

        #endregion

        #region constructor
        public BaseController(IBaseService<T> baseService,
            IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả các bản ghi
        /// </summary>
        /// <returns>trả về tất cả các bản ghi có trong DB</returns>
        /// CreatedBy: NVDAT
        [HttpGet]

        public IActionResult Get()
        {
            var entities = _baseRepository.GetAll();
            if (entities.Count() > 0)
            {
                return Ok(entities);
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// Lấy ra 1 đối tượng theo Id
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns>entity: đối tượng có mã entityId</returns>
        /// CreatedBy: NVDAT (22/04/2021)
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            var entity = _baseRepository.GetById(entityId);
            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Phân trang đối tượng
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <returns>
        ///     - Thành công: 200 - Danh sách đối tượng
        ///     - NoContent: 204
        /// </returns>
        [HttpGet("Paging")]
        public IActionResult Filters(int pageSize, int pageIndex)
        {
            //Lấy tất cả bản ghi trong DB
            var limit = _baseRepository.GetAll().Count();
            //Kiểm tra nếu số khách trên trang hoặc vị trí trang < 1 thì trả về BadRequest
            if (pageSize < 1 || pageIndex < 1)
            {
                return BadRequest();
            }
            // Kiểm tra nếu số khách/trang * vị trí trang < tổng khách + số khách/trang thì trả về NoContent.
            else if (pageSize * pageIndex >= (limit + pageSize))      //limit =245 total =250        245+10
            {
                return NoContent();
            }
            var entity = _baseService.GetMISAEntities(pageSize, pageIndex);
            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// Thêm 1 đối tượng 
        /// </summary>
        /// <param name="entity">đối tượng cần thêm</param>
        /// <returns></returns>
        /// CreatedBy: NVDAT
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            var rowAffects = _baseService.Insert(entity);
            if (rowAffects > 0)
            {
                return StatusCode(201, rowAffects);
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// Sửa 1 đối tượng theo Id
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <param name="entity">đối tượng cần sửa</param>
        /// <returns>
        ///     -Thành công: trả về bản ghi đã sửa.
        ///     -Thất bại: NoContent
        /// </returns>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] T entity)
        {
            //lấy tất cả property của đối tượng
            var properties = typeof(T).GetProperties();
            //Duyệt tất cả property của đối tượng
            foreach (var property in properties)
            {
                //kiểm tra tên của property với entityId thì gán giá trị property = id
                if (property.Name == $"{tableName}Id")
                {
                    property.SetValue(entity, id);
                }
            }
            var rowAffects = _baseService.Update(entity);
            if (rowAffects > 0)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// Xóa 1 đối tượng
        /// </summary>
        /// <param name="entityId">Mã đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: NVDAT (22/04/2021)
        [HttpDelete]
        public IActionResult Delete(Guid entityId)
        {
            var rowAffects = _baseService.Delete(entityId);
            if (rowAffects > 0)
            {
                return Ok();

            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Gán id cho 1 đối tượng
        /// </summary>
        /// <param name="id">Mã ID cần gán</param>
        /// <param name="entity">Đối tượng cần gán</param>
        public static void AssignEntityIdInEntity(Guid id, T entity)
        {
            // lấy tất cả property của đối tượng;
            var properties = typeof(T).GetProperties();
            // Duyệt tất cả property của đối tượng
            foreach (var property in properties)
            {
                // Kiểm tra tên của property trùng với entityId thì gán giá trị của property = id;
                if (property.Name == $"{tableName}Id")
                {
                    property.SetValue(entity, id);
                }
            }
        }
        #endregion
    }
}
