using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBankController : BaseController<EmployeeBank>
    {
        #region property

        private IEmployeeBankService _employeeBankService;
        private IEmployeeBankRepository _employeeBankRepository;

        #endregion

        #region constructor
        public EmployeeBankController(IEmployeeBankService employeeBankService, IEmployeeBankRepository employeeBankRepository) : base(employeeBankService, employeeBankRepository)
        {
            _employeeBankRepository = employeeBankRepository;
            _employeeBankService = employeeBankService;
        }
        #endregion
    }
}
