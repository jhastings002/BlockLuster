using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLuster.Common.Shared.ResponsesAndRequests
{
    public class RentMoviesRequest
    {
        public string UserId { get; set; }
        public string[] MovieIds { get; set; } 
    }
}
