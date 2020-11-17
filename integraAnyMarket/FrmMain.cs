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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            label3.Text = "Pedidos Faturados";
        }

        private void CmdProd1_Click(object sender, EventArgs e)
        {
            label3.Text = "Produtos ERP";
            this.Cursor = Cursors.WaitCursor;
            //    AnyMarket anyMarket = new AnyMarket();
            //    RootObject root = anyMarket.GetCategories();
            //    dataGridView1.DataSource= root.content;
            Db db = new Db();
            dataGridView1.DataSource = db.LoadProdutos();
            this.Cursor = Cursors.Default;

        }

        private void CmdProd2_Click(object sender, EventArgs e)
        {
            label3.Text = "Atualizar Preços";
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
 
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            label3.Text = "Produtos ANY";
            this.Cursor = Cursors.WaitCursor;
            AnyMarket anyMarket = new AnyMarket();
            RootProduto root = anyMarket.GetProdutos();
            dataGridView1.DataSource = root.produtos;

            List<FieldsProduto> lstProdutosFormatados = new List<FieldsProduto>();

            foreach (Produto p in root.produtos)
            {

                // lstProdutosFormatados.Add(fieldsProduto);
                foreach (Sku sku in p.skus)
                {
                    FieldsProduto fieldsProduto = new FieldsProduto();
                    fieldsProduto.IdProduto = p.id;
                    fieldsProduto.Product = p.title;
                    // FieldsProduto f = new FieldsProduto();
                    fieldsProduto.PartnerId = sku.partnerId;
                    fieldsProduto.IdSku = sku.id;
                    fieldsProduto.Title = sku.title;
                    fieldsProduto.Price = sku.price;
                    fieldsProduto.Amount = sku.amount;
                    lstProdutosFormatados.Add(fieldsProduto);
                }

            }
            dataGridView1.DataSource = lstProdutosFormatados;

            this.Cursor = Cursors.Default;

        }

        private void CmdPed1_Click(object sender, EventArgs e)
        {
            label3.Text = "Pedidos Pagos";

            this.Cursor = Cursors.WaitCursor;
            AnyMarket anyMarket = new AnyMarket();
            RootOrder root = anyMarket.GetPedidos();

            foreach( Order order in root.produtos)
            {
                ErpBridge erpBridge = new ErpBridge();
                erpBridge.processaPedido(order);
            }

        }

        private void CmdProd3_Click(object sender, EventArgs e)
        {
            label3.Text = "Atualizar Estoque";
            ErpBridge erpBridge = new ErpBridge();
            erpBridge.processaEstoque();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            label3.Text = "Pedidos Enviados";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            label3.Text = "Pedidos Entregues";
        }
    }
}
