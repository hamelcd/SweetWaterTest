using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            if (!Reader.IsDBNull(1))
                Comments = Reader.GetString(1);
            if (!Reader.IsDBNull(2))
                ExpectedShipDate = Reader.GetDateTime(2);

            this.SetCategory();
        }


        [DisplayName("Order Id")]
        public int OrderId { get; private set; }
        public string Comments { get; private set; }
        [DisplayName("Expect Ship")]
        public Nullable<DateTime> ExpectedShipDate { get; private set; }

        /*
        ## Task 1 - Write a report that will display the comments from the table

        Display the comments and group them into the following sections based on what the comment was about:
        - Comments about candy
        - Comments about call me / don't call me
        - Comments about who referred me
        - Comments about signature requirements upon delivery
        - Miscellaneous comments(everything else)
        */

        private void SetCategory()
        {
            if (string.IsNullOrEmpty(Comments))
            {
                CommentCategory = "Miscellaneous";
                return;
            }

            string comments = Comments.ToLower();

            if (comments.Contains("candy"))
                CommentCategory = "Candy";
            else if (comments.Contains("call me"))
                CommentCategory = "Call/Don't Call Me";
            else if (comments.Contains("refer"))
                CommentCategory = "Referrals";
            else if (comments.Contains("sign") && comments.Contains("deliver"))
                CommentCategory = "Delivery Signature";
            else
                CommentCategory = "Miscellaneous";
        }

        public string CommentCategory { get; private set; }
    }
}
