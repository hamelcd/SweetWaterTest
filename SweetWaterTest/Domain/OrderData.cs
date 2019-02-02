using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetWaterTest.Domain
{
    public class OrderData
    {
        public OrderData(DbDataReader Reader)
        {
            OrderId = Reader.GetInt32(0);
            Comments = Reader.GetString(1);
            ExpectedShipDate = Reader.GetDateTime(2);
        }


        public int OrderId { get; private set; }
        public string Comments { get; private set; }
        public DateTime ExpectedShipDate { get; private set; }
    }
}
