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
    public class DepartmentController : BaseController<Department>
    {
        #region property

        private IDepartmentService _departmentService;
        private IDepartmentRepository _departmentRepository;

        #endregion

        #region constructor
        public DepartmentController(IDepartmentService departmentService,IDepartmentRepository departmentRepository ) : base(departmentService, departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
        }
        #endregion
    }
}
