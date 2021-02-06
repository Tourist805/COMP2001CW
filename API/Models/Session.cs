using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWork.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime DateStart { get; set; }
    }
}
