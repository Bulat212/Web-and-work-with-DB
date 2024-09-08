using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace laba6.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FIO { get; set; }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}