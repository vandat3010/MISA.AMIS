using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region property

        private IEmployeeService _employeeService;
        private IEmployeeRepository _employeeRepository;

        #endregion

        #region constructor
        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository) : base(employeeService, employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }
        #endregion
    }
}
