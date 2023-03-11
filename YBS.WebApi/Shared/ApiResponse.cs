using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ApiResponse
    {
        public ApiResponse(string message, bool status = false)
        {
            this.Message = message;
            this.Status = status;
        }

        public ApiResponse(bool status, object data)
        {
            this.Status = status;
            this.Data = data;
        }

        public string Message { get; set; }

        public bool Status { get; set; }

        public object Data { get; set; }
    }
}
