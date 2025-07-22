using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public class ValidationResponse<T>
    {
        public T Data { get; set; } 

        public string Message { get; set; } = string.Empty;

        public bool isSuccess { get; set; }
    }

    public class ValidationResponse
    {
        public string Message { get; set; } = string.Empty;

        public bool isSuccess { get; set; }
    }
}
