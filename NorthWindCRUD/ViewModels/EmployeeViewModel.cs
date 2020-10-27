using NorthWindCRUD.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWindCRUD.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeDto employeeDto;
        public List<ReportCandidate> reportCandidates;
    }

    public class ReportCandidate
    {
        public int ReportToId { get; set; }
        public string ReportToName { get; set; }
    }
}