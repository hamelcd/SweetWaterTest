using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using SweetWaterTest.Domain;
using System.Text.RegularExpressions;

namespace SweetWaterTest.Database
{
    public static class OrderDataMethods
    {
        public static List<OrderData> GetAll()
        {
            List<OrderData> results = new List<OrderData>();

            using (NpgsqlConnection conn = new NpgsqlConnection(Postgres.ConnectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(Properties.Sql.OrderDataGetAll, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            results.Add(new OrderData(reader));
                    }
                }

                conn.Close();
            }

            return results;
        }

        public static List<CommentCategory> GetCommentCategory(List<OrderData> Orders)
        {
            List<CommentCategory> categories = new List<CommentCategory>();

            foreach (OrderData order in Orders)
            {
                CommentCategory cat = categories.Find(x => x.Category == order.CommentCategory);
                if (cat == null)
                {
                    cat = new CommentCategory() { Category = order.CommentCategory, Orders = new List<OrderData>() { order } };
                    categories.Add(cat);
                }
                else
                {
                    cat.Orders.Add(order);
                }
            }

            return categories;
        }

        private static Regex _ExtractDate = new Regex(@"Expected Ship Date:? ?(\d\d/\d\d/\d+)");

        public static void ParseExpectedShipDate(List<OrderData> Orders)
        {
            foreach (OrderData order in Orders)
            {
                Match m = _ExtractDate.Match(order.Comments);
                if (m.Success)
                {
                    DateTime expShip;

                    if (DateTime.TryParse(m.Groups[1].Value, out expShip))
                    {
                        if (order.ExpectedShipDate == null || order.ExpectedShipDate != expShip)
                            order.ExpectedShipDate = expShip;
                    }
                }
            }
        }
    }
}
