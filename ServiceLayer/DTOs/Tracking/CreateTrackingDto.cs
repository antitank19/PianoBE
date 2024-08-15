using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.Tracking
{
    public class CreateTrackingDto
    {
        public int SheetId { get; set; }
        public int Point { get; set; }
    }
}
