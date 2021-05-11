using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeBankService: BaseService<EmployeeBank>, IEmployeeBankService
    {
        IEmployeeBankRepository _employeeBankRepository;
        public EmployeeBankService(IEmployeeBankRepository employeeBankRepository): base(employeeBankRepository)
        {
            _employeeBankRepository = employeeBankRepository;
        }
    }
}
