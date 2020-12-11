using Newtonsoft.Json;
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
            Search();

        }

        private void Search()
        {
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;

            string strStatus = "0";
            if (radioButton1.Checked)
                strStatus = "0";

            if (radioButton2.Checked)
                strStatus = "1";

            if (radioButton3.Checked)
                strStatus = "2";


            string strQuery = $"SELECT id_apiped, dt_proc, id_ped, id_psp, cd_json, ds_ret, status FROM tb_apiped " +
                $"where TRUNC(dt_proc) >= to_Date('{dt1.ToString("dd/MM/yyyy")}', 'DD/MM/YYYY') " +
                $"and TRUNC(dt_proc) <= to_Date('{dt2.ToString("dd/MM/yyyy")}', 'DD/MM/YYYY' ) " +
                $"and status = {strStatus}";


            Db db = new Db();
            DataTable dt = db.Load(strQuery);
            dataGridView1.DataSource = dt;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                string json = r.Cells["CD_JSON"].Value.ToString();
                Order order = JsonConvert.DeserializeObject<Order>(json);
                FrmEditarPedido frmEditarPedido = new FrmEditarPedido();
                frmEditarPedido.SetOrder(order);
                frmEditarPedido.Show();
            }
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
            dataGridView1.DataSource = null;

            this.Cursor = Cursors.WaitCursor;
            ErpBridge erpBridge = new ErpBridge();
            erpBridge.processaPedido();

            this.Cursor = Cursors.Default;

            Search();
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

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
