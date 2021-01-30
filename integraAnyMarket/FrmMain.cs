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
            dataGridView1.DataSource = null;

            this.Cursor = Cursors.WaitCursor;
            Db db = new Db();
            DataTable dt = db.LoadFaturados();
            
            foreach (DataRow dr in dt.Rows)
            {
                Invoice invoice = new Invoice();
                //b.id_ANY AS ORDER_ID, 'INVOICED' AS STATUS, a.cd_Serie AS SERIES, a.cd_NF AS NUMBER, cd_Chave AS ACCESSKEY, 1, a.data
                 
                AnyFaturados faturado = new AnyFaturados();
                // faturado.order_id = dr["ORDER_ID"].ToString();

                faturado.status = "INVOICED";
                invoice.number = dr["NUMBERO"].ToString();
                invoice.series = dr["SERIES"].ToString();
                invoice.date = dr["data"].ToString();
                invoice.accessKey = dr["ACCESSKEY"].ToString();
                invoice.cfop = dr["cd_cfop"].ToString();
                invoice.companyStateTaxId = dr["cd_ie"].ToString();
                faturado.orderInvoice = invoice;

                AnyMarket anyMarket = new AnyMarket();
                if ( anyMarket.SetFaturado(dr["order_id"].ToString(), faturado))
                {
                    if (dr["ds_xml"].ToString() != "")
                    {
                        anyMarket.PutXML(dr["ds_xml"].ToString(), dr["ds_xml"].ToString());
                    } else {
                        String xml = getXmlStr(dr["ACCESSKEY"].ToString() + "-procNFe.xml");
                        if (xml != "")
                        {
                            anyMarket.PutXML(dr["order_id"].ToString(), xml);
                        }
                    }
                    db.setFaturado(dr["id_nfs01"].ToString(), "200", "sucesso", "1");
                } else
                {
                    db.setFaturado(dr["id_nfs01"].ToString(), "400", "erro", "2");
                }
            }
            dataGridView1.DataSource = dt;
            this.Cursor = Cursors.Default;

        }

        private String getXmlStr(string name)
        {
            String ret = "";
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\sideErp\xmlp\" + name);
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    // Console.WriteLine("\t" + line);
                    ret += String.Format("\t" + line);
                }
            } catch ( Exception ex)
            {
                ret = "";
                Log.Set(ex.Message);
            }
            return ret;
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
                $"and status = {strStatus} order by id_apiped DESC";


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
            dataGridView1.DataSource = null;

            this.Cursor = Cursors.WaitCursor;
            Db db = new Db();
            DataTable dt = db.LoadEnviados();

            foreach (DataRow dr in dt.Rows)
            {
                Invoice invoice = new Invoice();
                // SELECT b.id_ANY, 'PAID_WAITING_DELIVERY', c.dt_Lanc, a.dt_Exped, b.dt_Prev,
                //                            d.ds_Responsavel, d.ds_Comentario
                AnyTransito transito = new AnyTransito();
                transito.order_id = dr["id_ANY"].ToString();
                transito.status = "PAID_WAITING_DELIVERY";

                Tracking tracking = new Tracking();
                tracking.date = dr["dt_Lanc"].ToString();
                tracking.number = dr["id_any"].ToString();
                tracking.shippedDate = dr["dt_Exped"].ToString();
                tracking.estimateDate = dr["dt_Prev"].ToString();
                tracking.carrier = dr["ds_Responsavel"].ToString();
                tracking.url = dr["ds_Comentario"].ToString();

                transito.tracking = tracking;
                AnyMarket anyMarket = new AnyMarket();
                if (anyMarket.SetEnviado(transito.order_id, transito))
                {
                    db.setEnviado(dr["id_nfs01"].ToString(), "200", "sucesso", "1");
                } else
                {
                    db.setEnviado(dr["id_nfs01"].ToString(), "400", "erro", "2");
                }
               
            }
            dataGridView1.DataSource = dt;
            this.Cursor = Cursors.Default;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            label3.Text = "Pedidos Entregues";
            dataGridView1.DataSource = null;

            this.Cursor = Cursors.WaitCursor;
            Db db = new Db();
            DataTable dt = db.LoadEntregues();

            foreach (DataRow dr in dt.Rows)
            {
                Invoice invoice = new Invoice();
                // SELECT b.id_ANY, 'PAID_WAITING_DELIVERY', c.dt_Lanc, a.dt_Exped, b.dt_Prev,
                //                            d.ds_Responsavel, d.ds_Comentario
                AnyEntregue entregue = new AnyEntregue();
                entregue.status = "CONCLUDED";

                TrackingEntregue tracking = new TrackingEntregue();
                tracking.deliveredDate = dr["dt_Entrega"].ToString();

                entregue.tracking = tracking;
                AnyMarket anyMarket = new AnyMarket();
                if (anyMarket.SetEntregue(dr["id_any"].ToString(), entregue))
                {
                    db.setEntregue(dr["id_nfs01"].ToString(), "200", "sucesso", "1");
                }
                else
                {
                    db.setEntregue(dr["id_nfs01"].ToString(), "400", "erro", "2");
                }

            }
            dataGridView1.DataSource = dt;
            this.Cursor = Cursors.Default;
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
