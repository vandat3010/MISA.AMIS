using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Exceptions
{
    /// <summary>
    /// ClientException
    /// </summary>
    /// CreatedBy: NVDAT (09/05/2021)
    public class ClientException: Exception
    {
        public IDictionary? DataErr { get; set; }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="msg">Thông báo lỗi</param>
        public ClientException(string msg, IDictionary? data): base(msg)
        {
            DataErr = data;
        }
    }
}
