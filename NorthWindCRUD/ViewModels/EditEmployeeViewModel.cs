using NorthWindCRUD.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWindCRUD.ViewModels
{
    public class EditEmployeeViewModel
    {
        public EmployeeDto employeeDto;
        public Dictionary<int, string> reportCandidates;
    }
}