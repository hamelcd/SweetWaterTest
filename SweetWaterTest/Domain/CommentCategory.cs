using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetWaterTest.Domain
{
    public class CommentCategory
    {
        public string Category { get; set; }

        [DisplayName("# Orders")]
        public int OrderCount
        {
            get { return Orders == null ? 0 : Orders.Count; }
        }

        public List<OrderData> Orders { get; set; }
    }
}
