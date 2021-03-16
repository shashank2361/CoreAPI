using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Models
{
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public string NameSpace { get; set; }
        public string Parameters { get; set; }
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }

    }
}
