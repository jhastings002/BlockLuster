using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLuster.ResponsesAndRequests
{
    public class SigninResponse
    {
        public bool Success { get; set; } = false;
        public string Token { get; set; } = string.Empty;
    }
}
