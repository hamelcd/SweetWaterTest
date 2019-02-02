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

        public frmSweetH20()
        {
            InitializeComponent();
        }

        private void btnTask1_Click(object sender, EventArgs e)
        {
            List<OrderData> orders = OrderDataMethods.GetAll();
            _Categories = OrderDataMethods.GetCommentCategory(orders);

            bindingSourceOrders.DataSource = _Categories;
            dataGridViewCategories.Refresh();
        }
    }
}
