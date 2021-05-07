using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.EmplAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyRequired : Attribute
    {
        public string MsgErrorEmpty;
        public PropertyRequired(string msgErrorEmpty)
        {
            MsgErrorEmpty = msgErrorEmpty;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMaxLength : Attribute
    {
        /// <summary>
        /// ĐỘ dài lớn nhất của chuỗi.
        /// </summary>
        public int MaxLength;

        /// <summary>
        /// Chuỗi message lỗi khi nhập quá độ dài maxlength.
        /// </summary>
        public string MsgError_MaxLength;
        public PropertyMaxLength(int maxLength, string msgError_MaxLength)
        {
            MaxLength = maxLength;
            MsgError_MaxLength = msgError_MaxLength;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyEmail : Attribute
    {
        /// <summary>
        /// message email lỗi
        /// </summary>
        public string MsgErrorEmail;
        public PropertyEmail(string msgErrorEmail)
        {
            MsgErrorEmail = msgErrorEmail;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyPhone : Attribute
    {
        /// <summary>
        /// message số điện thoại trùng
        /// </summary>
        public string MsgErrorPhone;
        public PropertyPhone(string msgErrorPhone)
        {
            MsgErrorPhone = msgErrorPhone;
        }
    }
}
