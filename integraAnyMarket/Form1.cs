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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //    AnyMarket anyMarket = new AnyMarket();
            //    RootObject root = anyMarket.GetCategories();
            //    dataGridView1.DataSource= root.content;
            Db db = new Db();
            dataGridView1.DataSource = db.LoadProdutos();
            this.Cursor = Cursors.Default;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            AnyMarket anyMarket = new AnyMarket();
            RootProduto root = anyMarket.GetProdutos();
            dataGridView1.DataSource = root.produtos;
            this.Cursor = Cursors.Default;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
