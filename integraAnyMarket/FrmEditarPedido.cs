using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace integraAnyMarket
{
    public partial class FrmEditarPedido : Form
    {
        public Order order;

        public FrmEditarPedido()
        {
            InitializeComponent();

            this.order = new Order();
        }

        private void FrmEditarPedido_Load(object sender, EventArgs e)
        {

        }

        public void SetOrder( Order order)
        {
            this.order = order;
            propertyGrid1.SelectedObject = order;
            propertyGrid2.SelectedObject = order.buyer;
            propertyGrid3.SelectedObject = order.shipping;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            if (db.ProcessaPedido(this.order))
            {
                MessageBox.Show("Sucesso");
            } else
            {
                MessageBox.Show("Ocorreram problemas na importação");
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
