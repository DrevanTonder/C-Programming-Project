using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Item Class
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Whether the Item is On Order or not
        /// </summary>
        public bool OnOrder { get; set; }
        /// <summary>
        /// A Description of the Item
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Current Count of the amount of this Item in stock
        /// </summary>
        public int CurrentCount { get; set; }
        /// <summary>
        /// The Item's Code
        /// </summary>
        public string Code { get; set; }
    }
}
