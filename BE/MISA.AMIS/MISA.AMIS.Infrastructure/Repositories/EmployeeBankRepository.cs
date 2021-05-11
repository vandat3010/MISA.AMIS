using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class EmployeeBankRepository: BaseRepository<EmployeeBank>, IEmployeeBankRepository
    {
        public EmployeeBankRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
