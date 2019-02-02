using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SweetWaterTest.Domain;
using SweetWaterTest.Database;

namespace SweetWaterTest
{
    public partial class frmSweetH20 : Form
    {
        private List<CommentCategory> _Categories;
        private List<OrderData> _Orders;

        public frmSweetH20()
        {
            InitializeComponent();
        }

        private void btnTask1_Click(object sender, EventArgs e)
        {
            btnTask1.Enabled = false;

            _Orders = OrderDataMethods.GetAll();
            _Categories = OrderDataMethods.GetCommentCategory(_Orders);

            bindingSourceOrders.DataSource = _Categories;

            btnTask2.Enabled = true;
        }

        private void btnTask2_Click(object sender, EventArgs e)
        {
            OrderDataMethods.ParseExpectedShipDate(_Orders);

            bindingSourceOrders.DataSource = typeof(CommentCategory);
            bindingSourceOrders.DataSource = _Categories;

            MessageBox.Show("Expected Delivery Dates Updated");
        }
    }
}
