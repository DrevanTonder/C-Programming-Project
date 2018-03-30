using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Item
    {
        public bool OnOrder { get; set; }
        public string Description { get; set; }
        public int CurrentCount { get; set; }
        public string ItemCode { get; set; }
    }
}
