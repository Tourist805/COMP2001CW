using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWork.Models
{
    public class PasswordModel
    {
        public int PasswordId { get; set; }
        public int UserID { get; set; }
        public string PreviousPassword { get; set; }
        public DateTime dateTrans { get; set; }
    }
}
