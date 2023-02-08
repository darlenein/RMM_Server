using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    // This is the model for participant table in dB
    public class Participant
    {
        public int Progress_Bar { get; set; }
        public int Research_id { get; set; }
        public string Student_id { get; set; }
    }
}
