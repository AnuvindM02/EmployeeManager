﻿using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class EmployeeResponse
    {
        public Guid? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email {  get; set; }
        public string? Gender { get; set;}
        public string? Position { get; set; }

        public EmployeeUpdateRequest ToEmployeeUpdateRequest()
        {
            return new EmployeeUpdateRequest()
            {
                EmployeeID = EmployeeID,
                EmployeeName = EmployeeName,
                Email = Email,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
                Position = Position
            };
        }
    }
}
