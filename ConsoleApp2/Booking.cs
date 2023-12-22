using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Booking
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int SpaceID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
    }
}
