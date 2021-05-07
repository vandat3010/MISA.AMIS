﻿using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enum;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Validate dữ liệu riêng từng đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <param name="http">Phương thức POST hay PUT</param>
        /// Created By: NXCHIEN 07/05/2021
        protected override void CustomValidate(Employee entity, HTTPType http)
        {

            // Check trùng mã code
            // Khởi tạo giá trị
            var employeeCode = entity.EmployeeCode;
            var employeeId = entity.EmployeeId;
            var checkCodeExist = _employeeRepository.CheckEmployeeAttributeExist("EmployeeCode", employeeId, http, employeeCode);
            // Kiểm tra trùng hay không
            if (checkCodeExist)
            {
                throw new ClientException(Properties.ValidResources.Msg_Code_Exist);
            }


            //Check trùng IDentifyNumber
            // Khởi tạo giá trị
            var identifyNumber = entity.IdentifyNumber;
            var checkIdentifyNumberExist = _employeeRepository.CheckEmployeeAttributeExist("IdentifyNumber", employeeId, http, identifyNumber);
            // Kiểm tra trùng hay không
            if (checkIdentifyNumberExist)
            {
                throw new ClientException(Properties.ValidResources.Msg_IdentifyNumber_Exist);
            }

            // Check trùng số điện thoại
            // Khởi tạo giá trị
            var phoneNumber = entity.PhoneNumber;
            var checkphoneNumberExist = _employeeRepository.CheckEmployeeAttributeExist("PhoneNumber", employeeId, http, phoneNumber);
            // Kiểm tra trùng hay không
            if (checkphoneNumberExist)
            {
                throw new ClientException(Properties.ValidResources.Msg_Phone_Exist);
            }
        }
    }
}
