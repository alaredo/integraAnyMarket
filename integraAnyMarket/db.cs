using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.OracleClient;
using Newtonsoft.Json;

using System.Data;

namespace integraAnyMarket
{
    public class Prod_aux
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public string Endereco { get; set; }
    }

    public class Db
    {
        private Int32 id_pessoa;
        private Int32 id_localidade;
        private Int32 id_localidadeCobrança;
        private string id_revenda;
        private string id_vendedor;
        private string id_transportadora;
        private string id_marketPlace;

        private enum TipoEndereco
        {
            Comercial,
            Residencial,
            Entrega,
            Faturamento,
            Cobranca
        }


        private string connectionString = "User ID='SOIE';password='SNITRAMM19642008MDFTOTAL';Data Source=MMARTINS";

        public OracleConnection GetConnection()
        {
            try
            {
                //connectionStr = "user Id=gnfs;password=gnfs11072621;Data Source=ORCL";

                OracleConnection con;
                con = new OracleConnection(connectionString);
                con.Open();
                string message = con.ServerVersion;
                con.Close();

                return con;

            }
            catch (Exception e)
            {
                //message = e.Message;
                return null;
            }
        }



       


        public DataTable LoadProdutos()
        {
            DataTable dataTable = new DataTable();
            bool ret = true;
            try
            {
                string queryString = Convert.ToString("select id_psp from tb_psplace where id_psp in (1, 2, 3) order by id_psp");
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    OracleCommand command = new OracleCommand(queryString, connection);
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                        ret = true;
                    else
                        ret = false;

                    DataTable dtSchema = reader.GetSchemaTable();
                    List<DataColumn> listCols = new List<DataColumn>();

                    if (dtSchema != null)
                    {
                        foreach (DataRow drow in dtSchema.Rows)
                        {
                            string columnName = System.Convert.ToString(drow["ColumnName"]);
                            DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
                            //column.Unique = (bool)drow["IsUnique"];
                            //column.AllowDBNull = (bool)drow["AllowDBNull"];
                            //column.AutoIncrement = (bool)drow["IsAutoIncrement"];
                            listCols.Add(column);
                            dataTable.Columns.Add(column);
                        }
                    }
                    do
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < listCols.Count; i++)
                        {
                            dataRow[((DataColumn)listCols[i])] = reader[i];
                        }
                        dataTable.Rows.Add(dataRow);
                    } while (reader.Read());





                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }
            return dataTable;

            /*

            DataTable dataTable = new DataTable();
            try
            {
                string queryString = @"select id_Produto AS COD, cd_fabricante AS FAB, ds_Produto AS DESCRICAO from tb_produtos WHERE ds_Produto like '%JAQUETA%' ";
                //string queryString = @"select * from tb_psplace where id_psp in (1, 2, 3, 4, 9)";
                
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    OracleCommand command = new OracleCommand(queryString, connection);
                    OracleDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        dataTable.Load(reader);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }
            */
            return dataTable;
        }

        public bool CheckPed(string ped)
        {
            bool ret = true;
            try
            {
                string queryString = Convert.ToString("SELECT cd_pedorg FROM tb_ps01 WHERE cd_pedorg = '") + ped + "'";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    OracleCommand command = new OracleCommand(queryString, connection);
                    OracleDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                        ret = true;
                    else
                        ret = false;


                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }
            return ret;
        }

        public bool CheckPF(string cpf)
        {
            bool ret = true;

            string queryString = Convert.ToString("SELECT a.id_Pessoa, a.ds_pessoa, b.cd_cpf, b.id_localidade FROM tb_Pessoa  a, tb_Fisicos  b WHERE a.id_Pessoa = b.id_Pessoa AND b.cd_cpf = '") + cpf + "'";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                OracleCommand command = new OracleCommand(queryString, connection);
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        ret = true;

                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1) + ", " + reader.GetString(2));
                        id_pessoa = reader.GetInt32(0);
                    }
                    else
                        ret = false;
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
            return ret;
        }

        public bool CheckPJ(string cnpj)
        {
            bool ret = true;

            string queryString = Convert.ToString("SELECT a.id_Pessoa, a.ds_pessoa, b.cd_cnpj FROM tb_Pessoa  a, tb_Juridicos  b WHERE a.id_Pessoa = b.id_Pessoa AND b.cd_cnpj = ") + cnpj;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                OracleCommand command = new OracleCommand(queryString, connection);
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        ret = true;
                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1) + ", " + reader.GetString(2));
                        id_pessoa = reader.GetInt32(0);
                    }
                    else
                        ret = false;
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
            return ret;
        }

        public Int32 GetIdLocalidade(string nome, string uf)
        {
            Int32 ret = 0;

            nome = RemoveAccents(nome);

            string queryString = (Convert.ToString((Convert.ToString("SELECT id_localidade FROM tb_localidades WHERE TRIM(ds_localidade) = UPPER('") + nome) + "') and TRIM( CD_UF ) = UPPER('") + uf) + "')";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                        ret = reader.GetInt32(0);
                    else
                        ret = 107001;// Nao localizado
                }
                finally
                {
                    reader.Close();
                }
            }
            return ret;
        }



        private void addParameter(string name, OracleType tp, int size, object value, OracleCommand cmd)
        {
            OracleParameter parameter = new OracleParameter(name, size);
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Value = value;

            cmd.Parameters.Add(parameter);
        }


        public void InsertPF(string nome, string rg, DateTime dtEmissao, string orgaoEmissor, string cpf, System.Nullable<DateTime> dtNascimento, string sexo, string pai, string mae, string telefone, string telefone2)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleCommand cmd = new OracleCommand();

                cmd.Connection = connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_COM_PFISICA_INS";

                OracleParameter retval = new OracleParameter("P_ID_PESSOA", OracleType.Double, 9);
                retval.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(retval);

                addParameter("P_DS_PESSOA", OracleType.VarChar, 150, nome.ToUpper(), cmd);
                addParameter("P_TP_PESSOA", OracleType.VarChar, 1, "F", cmd);
                addParameter("P_TP_COMERCIAL", OracleType.VarChar, 1, "C", cmd);
                addParameter("P_USUARIO", OracleType.VarChar, 20, "PedidoWeb", cmd);
                addParameter("P_ID_LOCALIDADE", OracleType.Double, 9, 1, cmd);
                addParameter("P_CD_RG", OracleType.VarChar, 20, rg, cmd);
                addParameter("P_DT_EMISSAORG", OracleType.DateTime, 0, dtEmissao, cmd);
                // dtEmissao.ToString("dd/MM/yyyy")
                addParameter("P_CD_ORGAOEMISSORRG", OracleType.VarChar, 10, orgaoEmissor, cmd);
                addParameter("P_CD_CPF", OracleType.VarChar, 11, cpf, cmd);
                addParameter("P_DT_NASCIMENTO", OracleType.DateTime, 10, "", cmd);
                // dtNascimento.ToString("dd/MM/yyyy")
                addParameter("P_TP_SEXO", OracleType.VarChar, 1, sexo, cmd);
                addParameter("P_DS_PAI", OracleType.VarChar, 80, pai, cmd);
                addParameter("P_DS_MAE", OracleType.VarChar, 80, pai, cmd);

                OracleString oracleStr = new OracleString();
                int tot = cmd.ExecuteNonQuery();
                id_pessoa = Convert.ToInt32(retval.Value);
            

                insertTelefone(telefone.Substring(0, 2), telefone.Substring(2, telefone.Length - 2), cmd);


                insertEmail("nfe@b2online.com.br", cmd);
            }
        }


        public void insertTelefone(string ddd, string telefone, OracleCommand cmd)
        {
            if (telefone != null)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SP_COM_TELEFONE_INS";
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter retval = new OracleParameter("P_ID_TELEFONE", OracleType.Double, 9);
                retval.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(retval);

                addParameter("P_ID_PESSOA", OracleType.Double, 9, id_pessoa, cmd);
                addParameter("P_ID_TIPOTELEFONE", OracleType.Double, 9, 3, cmd);
                addParameter("P_CD_DDD", OracleType.VarChar, 4, ddd, cmd);
                addParameter("P_CD_TELEFONE", OracleType.VarChar, 20, telefone, cmd);
                addParameter("P_CD_RAMAL", OracleType.VarChar, 10, "", cmd);

                OracleString oracleStr = new OracleString();
                int tot = cmd.ExecuteNonQuery();
            }
        }

        public void insertEmail(string email, OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_EMAIL_INS";
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter retval = new OracleParameter("P_ID_EMAIL", OracleType.Double, 9);
            retval.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(retval);

            addParameter("P_ID_PESSOA", OracleType.Double, 9, id_pessoa, cmd);
            addParameter("P_DS_EMAIL", OracleType.VarChar, 50, email, cmd);

            OracleString oracleStr = new OracleString();
            int tot = cmd.ExecuteNonQuery();
        }

        public void updateEndereco(int tipoEndereco, int id_localidade, string logradouro, string numero, string complemento, string bairro, string cep, OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_ENDERECO_UPD";
            cmd.CommandType = CommandType.StoredProcedure;

            addParameter("P_ID_PESSOA", OracleType.Double, 9, id_pessoa, cmd);
            addParameter("P_ID_TIPOENDERECO", OracleType.Double, 9, tipoEndereco, cmd);
            addParameter("P_ID_LOCALIDADE", OracleType.Double, 9, id_localidade, cmd);
            addParameter("P_DS_LOGRADOURO", OracleType.VarChar, 150, logradouro, cmd);
            addParameter("P_DS_NUMERO", OracleType.VarChar, 15, numero, cmd);
            addParameter("P_DS_COMPLEMENTO", OracleType.VarChar, 80, complemento, cmd);
            addParameter("P_DS_BAIRRO", OracleType.VarChar, 80, bairro, cmd);
            addParameter("P_CD_CEP", OracleType.VarChar, 8, cep, cmd);

            cmd.ExecuteNonQuery();
        }

        public void insertEndereco(int tipoEndereco, int id_localidade, string logradouro, string numero, string complemento, string bairro, string cep, OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_ENDERECO_INS";
            cmd.CommandType = CommandType.StoredProcedure;

            addParameter("P_ID_PESSOA", OracleType.Double, 9, id_pessoa, cmd);
            addParameter("P_ID_TIPOENDERECO", OracleType.Double, 9, tipoEndereco, cmd);
            addParameter("P_ID_LOCALIDADE", OracleType.Double, 9, id_localidade, cmd);
            addParameter("P_DS_LOGRADOURO", OracleType.VarChar, 150, logradouro, cmd);
            addParameter("P_DS_NUMERO", OracleType.VarChar, 15, numero, cmd);
            addParameter("P_DS_COMPLEMENTO", OracleType.VarChar, 80, complemento, cmd);
            addParameter("P_DS_BAIRRO", OracleType.VarChar, 80, bairro, cmd);
            addParameter("P_CD_CEP", OracleType.VarChar, 8, cep, cmd);

            cmd.ExecuteNonQuery();
        }

        public void insertClientes(OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_CLIENTES_INS";
            cmd.CommandType = CommandType.StoredProcedure;


            id_transportadora = "193064";

            // id_vendedor = "";

            addParameter("P_ID_PESSOA", OracleType.Double, 22, id_pessoa, cmd);
            addParameter("P_ID_TRANSPORTADORA", OracleType.VarChar, 22, id_transportadora, cmd);
            addParameter("P_ID_VENDEDOR", OracleType.VarChar, 22, id_vendedor, cmd);
            addParameter("P_ID_REVENDA", OracleType.VarChar, 22, id_revenda, cmd);
            addParameter("P_USUARIO", OracleType.VarChar, 15, "PedidosWeb", cmd);

            cmd.ExecuteNonQuery();
        }

        public void updateClientes(OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_CLIENTES_UPD";
            cmd.CommandType = CommandType.StoredProcedure;


            id_transportadora = "193064";

            addParameter("P_ID_PESSOA", OracleType.Double, 22, id_pessoa, cmd);
            addParameter("P_ID_TRANSPORTADORA", OracleType.VarChar, 22, id_transportadora, cmd);
            addParameter("P_ID_VENDEDOR", OracleType.VarChar, 22, id_vendedor, cmd);
            addParameter("P_ID_REVENDA", OracleType.VarChar, 22, id_revenda, cmd);
            addParameter("P_USUARIO", OracleType.VarChar, 15, "PedidosWeb", cmd);

            cmd.ExecuteNonQuery();
        }

        public void insertPJ(string nome, int retIss, int ramoAtividade, string cnpj, string inscrEstadual, string inscrMunicipal, string nomeFantasia, DateTime dtAbertura, string telefone, string telefone2)
        {
            OracleConnection con = new OracleConnection(connectionString);
            con.Open();
            OracleCommand cmd = new OracleCommand("SP_COM_PJURIDICA_INS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter retval = new OracleParameter("P_ID_PESSOA", OracleType.Int32, 22);
            retval.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(retval);

            addParameter("P_DS_PESSOA", OracleType.VarChar, 150, nome.ToUpper(), cmd);
            addParameter("P_TP_PESSOA", OracleType.VarChar, 1, "J", cmd);
            addParameter("P_TP_COMERCIAL", OracleType.VarChar, 1, "C", cmd);
            addParameter("P_CD_RETISS", OracleType.Double, 22, retIss, cmd);
            addParameter("P_USUARIO", OracleType.VarChar, 20, "PedidoWeb", cmd);
            addParameter("P_ID_RAMOATIVIDADE", OracleType.Double, 22, ramoAtividade, cmd);
            addParameter("P_CD_CNPJ", OracleType.VarChar, 14, cnpj, cmd);
            addParameter("P_CD_INSCESTADUAL", OracleType.VarChar, 20, inscrEstadual, cmd);
            addParameter("P_CD_INSCMUNICIPAL", OracleType.VarChar, 20, inscrMunicipal, cmd);
            addParameter("P_DS_FANTASIA", OracleType.VarChar, 40, nomeFantasia, cmd);
            addParameter("P_DT_ABERTURA", OracleType.DateTime, 0, dtAbertura, cmd);

            OracleString oracleStr = new OracleString();
            int tot = cmd.ExecuteNonQuery();
            id_pessoa = Convert.ToInt32(retval.Value);

            insertTelefone(telefone.Substring(0, 2), telefone.Substring(2, telefone.Length - 2), cmd);


            insertEmail("comercial@belmicro.com.br", cmd);
        }


        public bool ProcessaPedido(Order order)
        {


            bool retorno;
            retorno = false;

            BuscaVendedor(order);

            if (CheckPed(order.marketPlaceNumber))
            {
               //throw new Exception("Ja existe um pedido cadastrado com este codigo - " + order.marketPlaceNumber);
                Log.Set("Ja existe um pedido cadastrado com este codigo - " + order.marketPlaceNumber);
                setPedLog(order.marketPlaceNumber, order.marketPlaceNumber, JsonConvert.SerializeObject(order), "Ja existe um pedido cadastrado com este codigo", id_marketPlace, "2");
                return retorno;
            }

            

            foreach (Item i in order.items )
            {
                string cod_produto = i.sku.partnerId;
                Item it = i;
                Prod_aux prodAux =  buscaProdutoCodigo(it.sku.partnerId, ref it);

                
                if ((i.sku.partnerId != prodAux.Codigo) && (prodAux.Codigo != null))
                    cod_produto = prodAux.Codigo;

                string strQuery = $"select * from tb_produto where id_produto = '{cod_produto}'";
                DataTable dtProduto = Load(strQuery);
                if (dtProduto.Rows.Count == 0)
                {
                    Log.Set("Produto nao cadastrado - " + cod_produto);
                    setPedLog(order.marketPlaceNumber, order.marketPlaceNumber, JsonConvert.SerializeObject(order), "Produto nao cadastrado - " + cod_produto, id_marketPlace, "2");
                    return retorno;
                }



            }

            OracleConnection con = new OracleConnection(connectionString);
            con.Open();
            OracleCommand cmd = new OracleCommand("SP_COM_PFISICA_INS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleTransaction myTrans;
            myTrans = con.BeginTransaction();
            cmd.Transaction = myTrans;

            try
            {
                
                Shipping entrega = order.shipping;
                if (entrega.comment == null)
                    entrega.comment = "";

                Anymarketaddress cobranca = order.anymarketAddress;

                BuscaVendedor(order);
                if (id_vendedor == "755873") {
                    int length = order.marketPlaceNumber.Length;
                    order.marketPlaceNumber = order.marketPlaceNumber.Substring(0, length - 2);
                    order.marketPlaceNumber = "0" + order.marketPlaceNumber.Substring(0, 1) + "-" + order.marketPlaceNumber.Substring(1, length - 2);
                }

                if ((order.buyer.documentType == "CPF"))
                {
                    if (CheckPF(order.buyer.document))
                    {
                        id_localidade = GetIdLocalidade(order.shipping.city, order.shipping.state);

                        DataTable dtFind = Load($"select * from tb_endereco where id_pessoa={id_pessoa}");
                        if (dtFind.Rows.Count > 0)
                        {
                            updateEndereco(2, id_localidade, entrega.street, entrega.number, entrega.comment, entrega.neighborhood, entrega.zipCode, cmd);
                        } else
                        {
                            insertEndereco(2, id_localidade, entrega.street, entrega.number, entrega.comment, entrega.neighborhood, entrega.zipCode, cmd);
                        }

                        updateClientes(cmd);
                    }
                    else
                    {
                        string strSexo = "M";
                        id_localidade = GetIdLocalidade(entrega.city, entrega.state);

                        if ((order.buyer.phone == null) && (order.buyer.cellPhone == null))
                        {
                            order.buyer.phone = "999999999";
                        }

                        InsertPF(order.buyer.name, order.buyer.document, DateTime.Now, "", order.buyer.document, DateTime.Now, strSexo, " ", " ", order.buyer.phone, order.buyer.cellPhone);

                        id_localidade = GetIdLocalidade(entrega.city, entrega.state);
                        DataTable dtFind = Load($"select * from tb_endereco where id_pessoa={id_pessoa}");
                        if (dtFind.Rows.Count > 0)
                        {
                            updateEndereco(2, id_localidade, entrega.street, entrega.number, entrega.comment, entrega.neighborhood, entrega.zipCode, cmd);
                        } else {
                            insertEndereco(2, id_localidade, entrega.street, entrega.number, entrega.address, entrega.neighborhood, entrega.zipCode, cmd);
                        } 

                        BuscaVendedor(order);
                        insertClientes(cmd);
                    }
                }
                else if (CheckPJ(order.buyer.document))
                {
                    id_localidade = GetIdLocalidade(entrega.city, entrega.state);

                    DataTable dtFind = Load($"select * from tb_endereco where id_pessoa={id_pessoa}");
                    if (dtFind.Rows.Count > 0)
                    {
                        updateEndereco(1, id_localidade, entrega.street, entrega.number, entrega.comment, entrega.neighborhood, entrega.zipCode, cmd);
                    }
                    else
                    {
                        insertEndereco(1, id_localidade, entrega.street, entrega.number, entrega.comment, entrega.neighborhood, entrega.zipCode, cmd);
                    }

                    updateClientes(cmd);
                }
                else
                {
                    id_localidade = GetIdLocalidade(entrega.city, entrega.state);

                    if ((order.buyer.phone == null) && (order.buyer.cellPhone == null))
                    {
                        order.buyer.phone = "999999999";

                    }

                    insertPJ(order.buyer.name, 0, 1, order.buyer.document, "", "", order.buyer.name, DateTime.Now, order.buyer.phone, order.buyer.cellPhone);
                    id_localidade = GetIdLocalidade(entrega.city, entrega.state);
                    DataTable dtFind = Load($"select * from tb_endereco where id_pessoa={id_pessoa}");
                    if (dtFind.Rows.Count > 0)
                    {
                        updateEndereco(1, id_localidade, entrega.street, entrega.number, entrega.comment, entrega.neighborhood, entrega.zipCode, cmd);
                    }
                    else
                    {
                        insertEndereco(1, id_localidade, entrega.street, entrega.number, entrega.address, entrega.neighborhood, entrega.zipCode, cmd);
                    }
                    BuscaVendedor(order);
                    insertClientes(cmd);
                }

                string idPedido = cadastraOrcamento(order, cmd);
                CadastraItemOrcamento(order, idPedido, cmd);
                setObservacao(idPedido, order.marketPlaceNumber, order, cmd);

                // pedido_1.SavePedido(pedido_1)

                myTrans.Commit();
                //order.statusSiad = "Importado";
                retorno = true;
                // SaveOrderFileOk(order);

                setPedLog(order.marketPlaceNumber, order.marketPlaceNumber, JsonConvert.SerializeObject(order), "Sucesso", id_marketPlace, "1");
            }
            catch (Exception ex)
            {
                Log.Set($"Erro - Pedido: {order.anymarketAddress} - {ex.Message}");

                setPedLog(order.marketPlaceNumber, order.marketPlaceNumber, JsonConvert.SerializeObject(order), ex.Message, id_marketPlace, "2");

                myTrans.Rollback();
                //order.statusSiad = "Erro";
                //Form1.SaveOrderFile(order);
                retorno = false;

            }
            finally
            {
                con.Close();
            }
            return retorno;
        }




        public string Ver_UFF(String ssU) {
            string retorno = "";

            if ((ssU == "") || (ssU != null))
                return "";
            ssU = this.RemoveAccents(ssU);
            ssU = ssU.ToUpper();
            if (ssU == "ACRE")
                return "AC";

            if (ssU == "ALAGOAS")
                return "AL";

            if (ssU == "AMAPA")
                return "AP";

            if (ssU == "AMAZONAS")
                return "AM";

            if (ssU == "BAHIA")
                return "BA";

            if (ssU == "CEARA")
                return "CE";

            if (ssU == "DISTRITO FEDERAL")
                return "DF";

            if (ssU == "ESPÍRITO SANTO")
                return "ES";

            if (ssU == "GOIAS")
                return "GO";

            if (ssU == "MARANHAO")
                return "MA";

            if (ssU == "MATO GROSSO")
                return "MT";

            if (ssU == "MATO GROSSO DO SUL")
                return "MS";

            if (ssU == "MINAS GERAIS")
                return "MG";

            if (ssU == "PARA")
                return "PA";

            if (ssU == "PARAIBA")
                return "PB";

            if (ssU == "PARANÁ")
                return "PR";

            if (ssU == "PERNAMBUCO")
                return "PE";

            if (ssU == "PIAUI")
                return "PI";

            if (ssU == "RIO DE JANEIRO")
                return "RJ";

            if (ssU == "RIO GRANDE DO NORTE")
                return "RN";


            if (ssU == "RIO GRANDE DO SUL")
                return "RS";

            if (ssU == "RONDONIA")
                return "RO";

            if (ssU == "RORAIMA")
                return "RR";

            if (ssU == "SANTA CATARINA")
                return "SC";

            if (ssU == "SAO PAULO")
                return "SP";

            if (ssU == "SERGIPE") 
                return "SE";

            if (ssU == "TOCANTINS")
                return "TO";

            return "";
        }
   

        public void BuscaVendedor(Order pedido)
        {
            string channel;
            channel = pedido.marketPlace.ToUpper();
            bool achou = false;

            string strQuery = $"select id_vendedor, id_transportadora, id_revenda, id_psp from tb_psplace where ds_psp = '{channel}'";
            DataTable dtChannel = Load(strQuery);
            if ( dtChannel.Rows.Count > 0)
            {
                foreach (DataRow r in dtChannel.Rows)
                {
                    achou = true;
                    id_vendedor = r["id_vendedor"].ToString();
                    id_transportadora = r["id_transportadora"].ToString();
                    id_revenda = r["id_revenda"].ToString();
                    id_marketPlace = r["id_psp"].ToString(); 
                }
            } else
            {
                setPedLog(pedido.marketPlaceNumber, pedido.marketPlaceNumber, JsonConvert.SerializeObject(pedido), "Market Place nao encontrado", "0", "2");
                achou = false;
            }

/*            if ((channel == "B2W NOVA API") || (channel == "B2W_NEW_API"))
            {
                achou = true;
                id_vendedor = "755873";
                id_transportadora = "193064";
                id_revenda = "646742";
                id_marketPlace = "1";
            }

            if ((channel == "VIA VAREJO") || (channel == "CNOVA"))
            {
                achou = true;
                id_vendedor = "755874";
                id_transportadora = "193064";
                id_revenda = "1551861";
                id_marketPlace = "2";
            }

            if ((channel == "ECOMMERCE"))
            {
                achou = true;
                id_vendedor = "1552768";
                id_transportadora = "193064";
                id_revenda = "1552769";
                id_marketPlace = "3";
            }

            if ((channel == "NETSHOES"))
            {
                achou = true;
                id_vendedor = "1552768";
                id_transportadora = "193064";
                id_revenda = "1552769";
                id_marketPlace = "3";
            }

            if ((channel == "WALMART"))
            {
                achou = true;
                id_vendedor = "1555841";
                id_transportadora = "193064";
                id_revenda = "1555842";
                id_marketPlace = "4";
            }

            if ((channel == "MERCADO LIVRE") || (channel == "MERCADO_LIVRE"))
            {
                achou = true;
                id_vendedor = "1556241";
                id_transportadora = "193064";
                id_revenda = "1556242";
                id_marketPlace = "5";
            }

            if ((channel == "MAGAZINE LUIZA"))
            {
                achou = true;
                id_vendedor = "1567962";
                id_transportadora = "193064";
                id_revenda = "1567963";
                id_marketPlace = "6";
            }

            if ((channel == "CENTAURO"))
            {
                achou = true;
                id_vendedor = "1641661";
                id_transportadora = "193064";
                id_revenda = "1641662";
                id_marketPlace = "7";
            }

            if ((channel == "CENTAURO"))
            {
                achou = true;
                id_vendedor = "1641661";
                id_transportadora = "193064";
                id_revenda = "1641662";
                id_marketPlace = "7";
            }
            */

            if (!achou)
            {
                id_vendedor = "-1";
            }

        }

        private void setObservacao(string id_pedido, string pedidoMktPlace, Order order, OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_PEDS04_INS";
            cmd.CommandType = CommandType.StoredProcedure;
            addParameter("P_ID_PEDIDO", OracleType.Double, 9, id_pedido, cmd);
            addParameter("p_ds_Linha01", OracleType.VarChar, 30, "", cmd);
            addParameter("p_ds_Linha02", OracleType.VarChar, 30, $"Número Pedido...: {pedidoMktPlace}", cmd);
            addParameter("p_ds_Linha03", OracleType.VarChar, 30, "", cmd);
            addParameter("p_ds_Linha04", OracleType.VarChar, 30, "", cmd);
            addParameter("p_Usuario", OracleType.VarChar, 20, "pedidoWeb", cmd);
            cmd.ExecuteNonQuery();

            if ( order.items.Length > 1)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SP_COM_PEDIDO_UPD2";
                cmd.CommandType = CommandType.StoredProcedure;
                addParameter("P_ID_PEDIDO", OracleType.Double, 9, id_pedido, cmd);
                cmd.ExecuteNonQuery();
            }
        }

        private void setPedLog(string id_pedido, string pedidoMktPlace, string str_json, string descricao, string id_psp, string status)
        {
            Double id_psp_official = 0;
            
            if (id_psp != "0")
            {
                id_psp_official = Convert.ToDouble(id_psp);
            }
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleCommand cmd = new OracleCommand("SP_API_APIPED_INS", connection);
                cmd.Parameters.Clear();
                cmd.CommandText = "SP_API_APIPED_INS";
                cmd.CommandType = CommandType.StoredProcedure;
                var retval = new OracleParameter("P_ID_APIPED", OracleType.Double, 9);
                retval.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(retval);
                addParameter("P_ID_PED", OracleType.VarChar, 30, id_pedido, cmd);
                addParameter("p_ID_PSP", OracleType.Double, 9, id_psp_official, cmd);
                addParameter("p_CD_JSON", OracleType.VarChar,4000, str_json, cmd);
                addParameter("p_DS_RET", OracleType.VarChar,1200, descricao, cmd);
                addParameter("p_STATUS", OracleType.VarChar, 2, status, cmd);
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void setProdLog(string cd_ret, string ds_ret)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleCommand cmd = new OracleCommand("SP_API_APIPED_INS", connection);
                cmd.Parameters.Clear();
                cmd.CommandText = "SP_API_SDPROD_INS";
                cmd.CommandType = CommandType.StoredProcedure;
                addParameter("p_CD_RET", OracleType.VarChar, 4000, "", cmd);
                addParameter("p_DS_RET", OracleType.VarChar, 1200, "", cmd);
                addParameter("p_STATUS", OracleType.VarChar, 2, "", cmd);
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        private string cadastraOrcamento(Order pedido, OracleCommand cmd)
        {
            Buyer cliente = pedido.buyer;
            Shipping entrega = pedido.shipping;
            Anymarketaddress cobranca = pedido.anymarketAddress;

            PedidoBd pedidoBel = new PedidoBd();
            pedidoBel.empresa = 1;
            pedidoBel.ped2 = null;
            pedidoBel.id_almoxarifado = 1;
            pedidoBel.id_pps01 = 1;
            pedidoBel.id_pc = 221;
            pedidoBel.id_pessoa = id_pessoa;
            pedidoBel.id_vendedor = id_vendedor;
            pedidoBel.id_revenda = id_revenda;
            pedidoBel.id_transportadora = id_transportadora;
            pedidoBel.id_cfop = pedido.shipping.state == "SP" ? 1 : 2;

            pedidoBel.id_limite = "0";
            pedidoBel.tp_po = "O";
            pedidoBel.dt_orc = pedido.createdAt;
            pedidoBel.dt_emissao = DateTime.Now;
            pedidoBel.dt_finalLib = "";
            pedidoBel.tp_cq = 0;
            pedidoBel.tp_pi = 0;
            pedidoBel.tp_frete = 1;
            pedidoBel.cd_titulo = 1;
            pedidoBel.ret_iss = 0;
            pedidoBel.cd_preco = 0;
            pedidoBel.usuario = "PedidoWeb";
            pedidoBel.id_localidade = id_localidade;

            pedidoBel.cd_consrev = "Consumidor";
            pedidoBel.cpfcnpj = cliente.document;
            pedidoBel.cd_ie = "";
            pedidoBel.ds_pessoa = cliente.name;
            pedidoBel.tipo_endereco = "Entrega";
            pedidoBel.ds_logradouro = entrega.street;
            pedidoBel.ds_numero = entrega.number;
            pedidoBel.ds_complemento = entrega.address;
            pedidoBel.ds_bairro = entrega.state;
            pedidoBel.cd_CEP = entrega.zipCode;
            pedidoBel.vl_total = pedido.total;
            pedidoBel.vl_produtos = pedido.gross;
            pedidoBel.vl_servicos = pedido.freight;

            pedidoBel.vl_baseICMS = 0;
            pedidoBel.vl_ICMS = 0;
            pedidoBel.baseISS = 0;
            pedidoBel.vl_ISS = 0;
            pedidoBel.vl_baseIPI = 0;
            pedidoBel.vl_IPI = 0;
            pedidoBel.cd_desc = 0;
            pedidoBel.vl_airrf = 0;
            pedidoBel.vl_irrf = 0;
            pedidoBel.vl_aif = 0;
            pedidoBel.vl_if = 0;
            pedidoBel.vl_fabrica = 0;
            pedidoBel.vl_preven = 0;
            pedidoBel.vl_com = 0;
            pedidoBel.vl_over = 0;
            pedidoBel.vl_cp = 0;
            pedidoBel.vl_desc = 0;
            pedidoBel.vl_acre = 0;
            pedidoBel.vl_pzmedio = 0;
            pedidoBel.vl_txjr = 0;
            pedidoBel.vl_alicom = 0;
            pedidoBel.vl_aliover = 0;
            pedidoBel.tp_cp = "N";
            pedidoBel.vl_incilim = 0;
            pedidoBel.vl_mais = 0;
            pedidoBel.vl_menos = 0;
            pedidoBel.vl_alimais = 0;
            pedidoBel.vl_alimaisa = 0;
            pedidoBel.vl_alimenos = 0;
            pedidoBel.vl_peso = 0;
            pedidoBel.cd_vinculo = 0;
            pedidoBel.tp_vinculo = 0;
            pedidoBel.id_vinculo = 0;
            pedidoBel.cd_exped = 0;
            pedidoBel.vl_limite = 0;
            pedidoBel.vl_ped = pedido.total;
            pedidoBel.vl_tit = 0;
            pedidoBel.vl_saldo = 0;
            pedidoBel.id_almexp = 1;
            pedidoBel.vl_venliq = 0;
            pedidoBel.vl_vencm = 0;
            pedidoBel.vl_vencm2 = 0;
            pedidoBel.vl_vencom = 0;
            pedidoBel.vl_venant = 0;
            pedidoBel.vl_aliped = 0;
            pedidoBel.vl_aliped2 = 0;
            pedidoBel.vl_alipar = 0;
            pedidoBel.vl_dificms = 0;
            pedidoBel.ds_frete = "";
            pedidoBel.vl_frete = pedido.freight;
            pedidoBel.cd_recval = 0;
            pedidoBel.vl_basest = 0;
            pedidoBel.vl_st = 0;
            pedidoBel.cd_st = 0;
            pedidoBel.vl_defere = 0;
            pedidoBel.vl_stcred = 0;
            pedidoBel.cd_origem = pedido.marketPlace;

            pedidoBel.pedEz = 0;
            pedidoBel.cd_pedorg = pedido.marketPlaceNumber;
            Prod_aux p_aux = new Prod_aux();

            foreach ( Item item in pedido.items )
            {
                String cod = item.sku.partnerId;

                if (cod.IndexOf('-') > -1)
                {
                    string aux = item.sku.partnerId;
                    item.sku.partnerId = item.sku.partnerId.Substring(0, cod.IndexOf('-'));
                    item.cd_produto = aux.Substring(cod.IndexOf('-'), aux.Length);
                }

                string query = $"SELECT id_Produto, cd_Para FROM tb_ProdDePara WHERE cd_De = '{item.sku.partnerId}'";
                DataTable dtCheckItem = Load(query);
                if (dtCheckItem.Rows.Count > 0)
                {
                    if (dtCheckItem.Rows[0]["id_produto"].ToString() == "")
                        item.sku.partnerId = dtCheckItem.Rows[0]["cd_Para"].ToString();
                    else
                        item.sku.partnerId = dtCheckItem.Rows[0]["id_produto"].ToString();
                }


                Item it = item;
                p_aux = buscaProdutoCodigo(item.sku.partnerId, ref it);

            }


            pedidoBel.cd_agrupa = p_aux.Descricao + " " + p_aux.Codigo;
            pedidoBel.id_Any = pedido.id.ToString();
            pedidoBel.id_marketplace = id_marketPlace;
            pedidoBel.vl_fretePF = 0;
            pedidoBel.cd_pedorg2 = pedido.subChannel;


            return pedidoBel.insertPedido(cmd);
        }


        public void CadastraItemOrcamento(Order pedido, 
            string idPedido, 
            OracleCommand cmd)
        {
            foreach (Item item in pedido.items)
            {
                PedidoItemBd itemPedido = new PedidoItemBd();

                itemPedido.id_pedido = idPedido;
                itemPedido.id_produto = item.sku.partnerId;
                itemPedido.cd_ez = 0;
                itemPedido.ds_produto = item.product.title;
                itemPedido.ds_unidade = "UN";
                itemPedido.qt_pedida = item.amount;
                itemPedido.qt_sdfat = item.amount;
                itemPedido.vl_prevenu = item.gross;
                itemPedido.vl_prevent = item.total;
                itemPedido.vl_prefab = "0";
                itemPedido.vl_alicms = "0";
                itemPedido.vl_baseicms = "0";
                itemPedido.vl_redb = "0";
                itemPedido.vl_icms = "0";
                itemPedido.vl_Alipi = "0";
                itemPedido.vl_baseipi = "0";
                itemPedido.vl_ipi = "0";
                itemPedido.vl_com = "0";
                itemPedido.vl_over = "0";
                itemPedido.vl_cp = "0";
                itemPedido.vl_desc = "0";
                itemPedido.vl_acre = "0";
                itemPedido.vl_peso = "0";
                itemPedido.id_vinculo = "0";
                itemPedido.vl_basest = 0;
                itemPedido.vl_st = 0;
                itemPedido.vl_mva = 0;
                itemPedido.vl_defere = 0;
                itemPedido.vl_stcred = 0;
                itemPedido.cd_org = "0";
                int tot_itens = pedido.items.Length;
                itemPedido.vl_frete = (double) pedido.total / tot_itens;
                itemPedido.cd_produto = item.cd_produto;

                itemPedido.InsertItem(cmd);
            }
        }

        public DataTable Load(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string queryString = query;

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    OracleCommand command = new OracleCommand(queryString, connection);
                    OracleDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        dataTable.Load(reader);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }
            return dataTable;
        }

        public void Execute(string query, OracleCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string queryString = query;

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    OracleCommand command = new OracleCommand(queryString, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }
           
        }

        public Prod_aux buscaProdutoCodigo(string sku, ref Item item )
        {

            String cod = item.sku.partnerId;

            if (cod.IndexOf('-') > -1)
            {
                string aux = item.sku.partnerId;
                item.sku.partnerId = item.sku.partnerId.Substring(0, cod.IndexOf('-'));
                item.cd_produto = aux.Substring(cod.IndexOf('-'), aux.Length);
            }

            string query = $"SELECT id_Produto, cd_Para FROM tb_ProdDePara WHERE cd_De = '{item.sku.partnerId}'";
            DataTable dtCheckItem = Load(query);
            if (dtCheckItem.Rows.Count > 0)
            {
                if (dtCheckItem.Rows[0]["id_produto"].ToString() == "")
                    item.sku.partnerId = dtCheckItem.Rows[0]["cd_Para"].ToString();
                else
                    item.sku.partnerId = dtCheckItem.Rows[0]["id_produto"].ToString();
            }




            Prod_aux prod = new Prod_aux();
            string queryString = Convert.ToString("SELECT a.id_produto, a.ds_produto FROM tb_produtos a WHERE a.cd_fabricante = '") + sku + "'";
            DataTable dtFabricante = Load(queryString);
            if (dtFabricante.Rows.Count == 0)
            {
                queryString = Convert.ToString("SELECT a.id_produto, a.ds_produto FROM tb_produtos a WHERE a.id_produto = ") + sku;
                DataTable dtProduto = Load(queryString);
                if (dtProduto.Rows.Count == 0)
                {
                    prod = new Prod_aux();
                }
                else
                {
                    prod.Codigo = dtProduto.Rows[0]["id_produto"].ToString();
                    prod.Descricao = dtProduto.Rows[0]["ds_produto"].ToString();
                }
                    
            } else
            {
                prod.Codigo = dtFabricante.Rows[0]["id_produto"].ToString();
                prod.Descricao = dtFabricante.Rows[0]["ds_produto"].ToString();
            }

            return prod;
        }

        public DataTable LoadStock ( )
        {
            string strQuery = "SELECT id_apisd, id_sku, qt_prod, status FROM tb_apisdprod";
            DataTable dataTable = Load(strQuery);
            return dataTable;
        }
    
        /*
   
        public void SaveOrderFileOk(Order order)
        {
            JsonSerializerSettings serializerSetting = new JsonSerializerSettings();
            serializerSetting.NullValueHandling = NullValueHandling.Ignore;
            string strProd;
            try
            {
                strProd = JsonConvert.SerializeObject(order, Newtonsoft.Json.Formatting.Indented, serializerSetting);
                if ((My.Computer.FileSystem.FileExists(".//Importados//" + order.code + ".ord")))
                    My.Computer.FileSystem.DeleteFile(@".\Importados\" + order.code + ".ord");

                System.IO.StreamWriter file;
                file = My.Computer.FileSystem.OpenTextFileWriter(@".\Importados\" + order.code + ".ord", true);
                file.WriteLine(strProd);
                file.Close();
            }
            catch (Exception ex)
            {
                var strErroMessage = ex.Message;
            }
        }

    */

        public bool CheckPedido(string pId)
        {
            bool ret = true;


            string queryString = Convert.ToString("SELECT a.cd_ez FROM tb_ps01 a WHERE a.cd_ez = ") + pId;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleCommand command = new OracleCommand(queryString, connection);

                OracleDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.Read())
                        ret = true;
                    else
                        ret = false;
                }
                finally
                {

                    // always call Close when done reading.

                    reader.Close();
                }
            }

            return ret;
        }



        public string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();

            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            return sbReturn.ToString();
        }
    }

}