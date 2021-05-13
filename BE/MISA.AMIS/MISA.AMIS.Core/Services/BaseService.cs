using MISA.AMIS.Core.EmplAttribute;
using MISA.AMIS.Core.Enum;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Xóa dữ liệu có Id
        /// </summary>
        /// <param name="entityId">ID bản ghi được chọn</param>
        /// CreatedBy: NVDAT(07/05/2021)
        public int Delete(Guid entityId)
        {
            return _baseRepository.Delete(entityId);
        }

        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// CreatedBy: NVDAT(07/05/2021)
        public IEnumerable<T> GetAll()
        {
            return _baseRepository.GetAll();
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns>Trả về bản ghi cần lấy theo Id</returns>
        /// CreatedBy: NVDAT(07/05/2021)
        public T GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        /// <summary>
        /// thêm một bản ghi
        /// </summary>
        /// <param name="entity">thông tin bản ghi cần thêm mới</param>
        /// <returns>thông tin bản ghi được thêm</returns>
        /// CreatedBy: NVDAT(07/05/2021)
        public int Insert(T entity)
        {
            
             Validate(entity, HTTPType.POST);
            return _baseRepository.Insert(entity);
        }
        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">thông tin bản ghi cần cập nhật</param>
        /// <returns>Thông tin bản ghi được caaph nhật</returns>
        /// CreatedBy: NVDAT(07/05/2021)
        public int Update(T entity)
        {
            Validate(entity, HTTPType.PUT);
            return _baseRepository.Update(entity);
        }
        /// <summary>
        /// validate dữ liệu
        /// </summary>
        /// <param name="entity">Dối tượng  cần validate</param>
        /// <param name="http">phương thức</param>
        /// CreatedBy:NVDAT(07/05/2021)
        private void Validate(T entity, HTTPType http)
        {
            // Lấy ra tất cả property của đối tượng
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                // Lấy ra attribute của đối tượng
                var requiredAttribute = property.GetCustomAttributes(typeof(PropertyRequired), true);
                if (requiredAttribute.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Kiểm tra nếu giá trị null thì gán thành empty.
                    if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        // Lấy ra message lỗi của attribute.
                        var msgError = (requiredAttribute[0] as PropertyRequired).MsgErrorEmpty;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            //msgError = $"{property.Name} không được phép để trống!";
                            msgError = string.Format(Properties.ValidResources.MsgErrorRequired, property.Name);
                        }
                        throw new ClientException(msgError);
                    }
                }
                // Kiểm tra độ dài mã Code của property
                var maxLengthAttribute = property.GetCustomAttributes(typeof(PropertyMaxLength), true);
                if (maxLengthAttribute.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Lấy ra giá trị truyền vào của MISAMaxLength
                    var maxLength = (maxLengthAttribute[0] as PropertyMaxLength).MaxLength;
                    // Kiểm tra độ dài
                    if (propertyValue.ToString().Length > maxLength)
                    {
                        var msgError = (maxLengthAttribute[0] as PropertyMaxLength).MsgError_MaxLength;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            //msgError = $"Độ dài của {property.Name} phải nhỏ hơn {maxLength}";
                            msgError = string.Format(Properties.ValidResources.MsgErrorMaxLength,property.Name, maxLength);
                        }
                        throw new ClientException(msgError);
                    }
                }
                // Kiểm tra email
                /*var emailAttribute = property.GetCustomAttributes(typeof(PropertyEmail), true);
                if (emailAttribute.Length > 0)
                {
                    // Lấy giá trị email
                    var emailValue = property.GetValue(entity);
                    // Khởi tạo regex và kiểm tra
                    Regex regex = new Regex(Properties.ValidResources.Regex_String);
                    if (!regex.IsMatch(emailValue.ToString()))
                    {

                        var msgErrorEmail = (emailAttribute[0] as PropertyEmail).MsgErrorEmail;
                        if (string.IsNullOrEmpty(msgErrorEmail.ToString()))
                        {
                            msgErrorEmail = $"{property.Name} không đúng định dạng!";
                        }
                        throw new ClientException(msgErrorEmail);
                    }
                }*/
            }
            CustomValidate(entity, http);
        }
        /// <summary>
        /// Phương thức dùng để cho valid của các trường hợp riêng biệt.
        /// </summary>
        /// <param name="t">Một thực thể</param>
        /// <param name="http">Xác định Post hoặc Put</param>
        /// CreatedBy: NVDAT(07/05/2021)
        protected virtual void CustomValidate(T t, HTTPType http)
        {

        }

        /// <summary>
        /// Phân trang đối tượng.
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang.</param>
        /// <param name="pageIndex">Trang số bao nhiêu.</param>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NVDAT(07/05/2021)
        public IEnumerable<T> GetMISAEntities(int pageSize, int pageIndex)
        {
            return _baseRepository.GetMISAEntities(pageSize, pageIndex);
        }
    }

}
