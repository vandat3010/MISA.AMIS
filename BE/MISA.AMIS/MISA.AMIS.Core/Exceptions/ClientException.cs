using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Exceptions
{
    /// <summary>
    /// Class ngoại lệ của khách hàng.
    /// </summary>
    /// CreatedBy: NVDAT(07/05/2021)
    public class ClientException : Exception
    {
        public ClientException(string msg) : base(msg)
        {

        }
    }
}
