using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;

namespace integraAnyMarket
{
    public partial class PedidoBd
    {
        public PedidoBd()
        {
        }

        private void addParameter(string name, OracleType tp, int size, object value, OracleCommand con)
        {
            var parameter = new OracleParameter(name, tp, size);
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Value = value;
            con.Parameters.Add(parameter);
        }

        public void insertFinanceiroPedido(string idPedido, string vlPedido, OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_PEDS04B_INS";
            cmd.CommandType = CommandType.StoredProcedure;
            addParameter("p_id_TipoDoc", OracleType.Double, 9, 181, cmd);
            addParameter("p_id_pedido", OracleType.Double, 9, Convert.ToDouble(idPedido), cmd);
            addParameter("p_vl_Parcela", OracleType.VarChar, 30, Convert.ToString(vlPedido), cmd);
            addParameter("p_vl_Dias", OracleType.Double, 3, 0, cmd);
            addParameter("p_cd_Libera", OracleType.Double, 1, 0, cmd);
            addParameter("p_vl_per", OracleType.Double, 100, 3, cmd);
            cmd.ExecuteNonQuery();
        }

        public string insertPedido(OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_COM_PEDS01_WEB";
            cmd.CommandType = CommandType.StoredProcedure;
            var retval = new OracleParameter("P_ID_PEDIDO", OracleType.Double, 9);
            retval.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(retval);
            addParameter("P_ID_EMPRESA", OracleType.Double, 9, empresa, cmd);
            addParameter("P_ID_PED2", OracleType.Double, 9, ped2, cmd);
            addParameter("P_ID_ALMOXARIFADO", OracleType.VarChar, 30, id_almoxarifado, cmd);
            addParameter("P_ID_PPS01", OracleType.VarChar, 30, id_pps01, cmd);
            addParameter("P_ID_PC", OracleType.VarChar, 30, id_pc, cmd);
            addParameter("P_ID_PESSOA", OracleType.VarChar, 30, id_pessoa, cmd);
            addParameter("P_ID_VENDEDOR", OracleType.VarChar, 30, id_vendedor, cmd);
            addParameter("P_ID_REVENDA", OracleType.VarChar, 30, id_revenda, cmd);
            addParameter("P_ID_TRANSPORTADORA", OracleType.VarChar, 30, id_transportadora, cmd);
            addParameter("P_ID_CFOP", OracleType.VarChar, 30, id_cfop, cmd);
            addParameter("P_ID_LIMITE", OracleType.VarChar, 9, id_limite, cmd);
            addParameter("P_TP_PO", OracleType.VarChar, 1, tp_po, cmd);
            addParameter("P_DT_ORC", OracleType.DateTime, 0, dt_orc, cmd);
            addParameter("P_DT_EMISSAO", OracleType.DateTime, 0, dt_emissao, cmd);
            addParameter("P_DT_FINALLIB", OracleType.VarChar, 30, dt_finalLib, cmd);
            addParameter("P_TP_CQ", OracleType.VarChar, 1, tp_cq, cmd);
            addParameter("P_TP_PI", OracleType.Char, 1, tp_pi, cmd);
            addParameter("P_TP_FRETE", OracleType.Char, 1, tp_frete, cmd);
            addParameter("P_CD_TITULO", OracleType.Char, 1, cd_titulo, cmd);
            addParameter("P_CD_RETISS", OracleType.Double, 1, ret_iss, cmd);
            addParameter("P_CD_PRECO", OracleType.Double, 1, cd_preco, cmd);
            addParameter("P_USUARIO", OracleType.VarChar, 20, usuario, cmd);
            addParameter("P_ID_LOCALIDADE", OracleType.VarChar, 30, id_localidade, cmd);
            addParameter("P_CD_CONSREV", OracleType.VarChar, 15, cd_consrev, cmd);
            addParameter("P_CD_CPFCNPJ", OracleType.VarChar, 14, cpfcnpj, cmd);
            addParameter("P_CD_IE", OracleType.VarChar, 20, cd_ie, cmd);
            addParameter("P_DS_PESSOA", OracleType.VarChar, 80, ds_pessoa.ToString().ToUpper(), cmd);
            addParameter("P_DS_TIPOENDERECO", OracleType.VarChar, 20, tipo_endereco, cmd);
            addParameter("P_DS_LOGRADOURO", OracleType.VarChar, 20, ds_logradouro, cmd);
            addParameter("P_DS_NUMERO", OracleType.VarChar, 20, ds_numero, cmd);
            addParameter("P_DS_COMPLEMENTO", OracleType.VarChar, 20, ds_complemento, cmd);
            addParameter("P_DS_BAIRRO", OracleType.VarChar, 20, ds_bairro, cmd);
            addParameter("P_CD_CEP", OracleType.VarChar, 20, cd_CEP, cmd);
            addParameter("P_VL_TOTAL", OracleType.VarChar, 20, vl_total, cmd);
            addParameter("P_VL_PRODUTOS", OracleType.VarChar, 20, vl_produtos, cmd);
            addParameter("P_VL_SERVICOS", OracleType.VarChar, 20, vl_servicos, cmd);
            addParameter("P_VL_BASEICMS", OracleType.VarChar, 20, vl_baseICMS, cmd);
            addParameter("P_VL_ICMS", OracleType.VarChar, 20, vl_ICMS, cmd);
            addParameter("P_VL_BASEISS", OracleType.VarChar, 20, baseISS, cmd);
            addParameter("P_VL_ISS", OracleType.VarChar, 30, vl_ISS, cmd);
            addParameter("P_VL_BASEIPI", OracleType.VarChar, 30, vl_baseIPI, cmd);
            addParameter("P_VL_IPI", OracleType.VarChar, 30, vl_IPI, cmd);
            addParameter("P_CD_DESC", OracleType.VarChar, 30, cd_desc, cmd);
            addParameter("P_VL_AIRRF", OracleType.VarChar, 30, vl_airrf, cmd);
            addParameter("P_VL_IRRF", OracleType.VarChar, 30, vl_irrf, cmd);
            addParameter("P_VL_AIF", OracleType.VarChar, 30, vl_aif, cmd);
            addParameter("P_VL_IF", OracleType.VarChar, 30, vl_if, cmd);
            addParameter("P_VL_FABRICA", OracleType.VarChar, 30, vl_fabrica, cmd);
            addParameter("P_VL_PREVEN", OracleType.VarChar, 30, vl_preven, cmd);
            addParameter("P_VL_COM", OracleType.VarChar, 30, vl_com, cmd);
            addParameter("P_VL_OVER", OracleType.VarChar, 30, vl_over, cmd);
            addParameter("P_VL_CP", OracleType.VarChar, 30, vl_cp, cmd);
            addParameter("P_VL_DESC", OracleType.VarChar, 30, vl_desc, cmd);
            addParameter("P_VL_ACRE", OracleType.VarChar, 30, vl_acre, cmd);
            addParameter("P_VL_PZMEDIO", OracleType.VarChar, 30, vl_pzmedio, cmd);
            addParameter("P_VL_TXJR", OracleType.VarChar, 30, vl_txjr, cmd);
            addParameter("P_VL_ALICOM", OracleType.VarChar, 30, vl_alicom, cmd);
            addParameter("P_VL_ALIOVER", OracleType.VarChar, 30, vl_aliover, cmd);
            addParameter("P_TP_CP", OracleType.VarChar, 1, tp_cp, cmd);
            addParameter("P_VL_INCILIM", OracleType.VarChar, 30, vl_incilim, cmd);
            addParameter("P_VL_MAIS", OracleType.VarChar, 30, vl_mais, cmd);
            addParameter("P_VL_MAISA", OracleType.VarChar, 30, vl_maisa, cmd);
            addParameter("P_VL_MENOS", OracleType.VarChar, 30, vl_menos, cmd);
            addParameter("P_VL_ALIMAIS", OracleType.VarChar, 30, vl_alimais, cmd);
            addParameter("P_VL_ALIMAISA", OracleType.VarChar, 30, vl_alimaisa, cmd);
            addParameter("P_VL_ALIMENOS", OracleType.VarChar, 30, vl_alimenos, cmd);
            addParameter("P_VL_PESO", OracleType.VarChar, 30, vl_peso, cmd);
            addParameter("P_CD_VINCULO", OracleType.VarChar, 1, cd_vinculo, cmd);
            addParameter("P_TP_VINCULO", OracleType.VarChar, 1, tp_vinculo, cmd);
            addParameter("P_ID_VINCULO", OracleType.VarChar, 30, id_vinculo, cmd);
            addParameter("P_CD_EXPED", OracleType.VarChar, 1, cd_exped, cmd);
            addParameter("P_VL_LIMITE", OracleType.VarChar, 30, vl_limite, cmd);
            addParameter("P_VL_PED", OracleType.VarChar, 30, vl_ped, cmd);
            addParameter("P_VL_TIT", OracleType.VarChar, 30, vl_tit, cmd);
            addParameter("P_VL_SALDO", OracleType.VarChar, 30, vl_saldo, cmd);
            addParameter("P_ID_ALMEXP", OracleType.VarChar, 30, id_almexp, cmd);
            addParameter("P_VL_VENLIQ", OracleType.Double, 17, vl_venliq, cmd);
            addParameter("P_VL_VENCM", OracleType.Double, 17, vl_vencm, cmd);
            addParameter("P_VL_VENCM2", OracleType.Double, 20, vl_vencm2, cmd);
            addParameter("P_VL_VENCOM", OracleType.Double, 20, vl_vencom, cmd);
            addParameter("P_VL_VENANT", OracleType.Double, 20, vl_venant, cmd);
            addParameter("P_VL_ALIPED", OracleType.Double, 20, vl_aliped, cmd);
            addParameter("P_VL_ALIPED2", OracleType.Double, 20, vl_aliped2, cmd);
            addParameter("P_VL_ALIPAR", OracleType.Double, 20, vl_alipar, cmd);
            addParameter("P_VL_DIFICMS", OracleType.Double, 20, vl_dificms, cmd);
            addParameter("P_DS_FRETE", OracleType.VarChar, 20, ds_frete, cmd);
            addParameter("P_VL_FRETE", OracleType.Double, 20, vl_frete, cmd);
            addParameter("P_CD_RECVAL", OracleType.Double, 20, cd_recval, cmd);
            addParameter("P_VL_BASEST", OracleType.Double, 20, vl_basest, cmd);
            addParameter("P_VL_ST", OracleType.Double, 20, vl_st, cmd);
            addParameter("P_CD_ST", OracleType.Double, 1, cd_st, cmd);
            addParameter("P_VL_DEFERE", OracleType.Double, 20, vl_defere, cmd);
            addParameter("P_VL_STCRED", OracleType.Double, 20, vl_stcred, cmd);
            addParameter("P_CD_EZ", OracleType.Double, 12, 0, cmd);
            addParameter("P_CD_PEDORG", OracleType.VarChar, 30, cd_pedorg, cmd);
            addParameter("P_CD_PEDENT", OracleType.VarChar, 30, "0", cmd);
            addParameter("P_VL_BASEICMSDEST", OracleType.Double, 1, 0, cmd);
            addParameter("P_VL_ICMSFCP", OracleType.Double, 1, 0, cmd);
            addParameter("P_VL_ICMSDEST", OracleType.Double, 1, 0, cmd);
            addParameter("P_VL_ICMSREM", OracleType.Double, 1, 0, cmd);
            addParameter("P_VL_ALICMSPART", OracleType.Double, 1, 0, cmd);
            addParameter("P_VL_ALICMSFCP", OracleType.Double, 1, 0, cmd);
            addParameter("P_VL_OD", OracleType.Double, 1, 0, cmd);
            addParameter("p_cd_Origem", OracleType.VarChar, 25, cd_origem, cmd);
            addParameter("p_cd_Agrupa", OracleType.VarChar, 120, 0, cmd);
            addParameter("p_cd_CST060", OracleType.Double, 1, 0, cmd);
            addParameter("p_id_Any", OracleType.VarChar, 30, id_Any, cmd);

            cmd.ExecuteNonQuery();
            id_pedido = Convert.ToInt32(retval.Value);
            insertFinanceiroPedido(Convert.ToString(id_pedido), Convert.ToString(vl_total), cmd);
            return Convert.ToString(id_pedido);
        }

        public object empresa
        {
            get
            {
                return m_empresa;
            }

            set
            {
                m_empresa = value;
            }
        }

        private object m_empresa;

        public string cd_agrupa { get; set; }

        public string id_Any { get; set; }

        public object ped2
        {
            get
            {
                return m_ped2;
            }

            set
            {
                m_ped2 = value;
            }
        }

        private object m_ped2;

        public object id_almoxarifado
        {
            get
            {
                return m_id_almoxarifado;
            }

            set
            {
                m_id_almoxarifado = value;
            }
        }

        private object m_id_almoxarifado;

        public object id_pps01
        {
            get
            {
                return m_id_pps01;
            }

            set
            {
                m_id_pps01 = value;
            }
        }

        private object m_id_pps01;

        public object id_pc
        {
            get
            {
                return m_id_pc;
            }

            set
            {
                m_id_pc = value;
            }
        }

        private object m_id_pc;

        public object id_pessoa
        {
            get
            {
                return m_id_pessoa;
            }

            set
            {
                m_id_pessoa = value;
            }
        }

        private object m_id_pessoa;

        public object id_vendedor
        {
            get
            {
                return m_id_vendedor;
            }

            set
            {
                m_id_vendedor = value;
            }
        }

        private object m_id_vendedor;

        public object id_revenda
        {
            get
            {
                return m_id_revenda;
            }

            set
            {
                m_id_revenda = value;
            }
        }

        private object m_id_revenda;

        public object id_transportadora
        {
            get
            {
                return m_id_transportadora;
            }

            set
            {
                m_id_transportadora = value;
            }
        }

        private object m_id_transportadora;

        public object id_cfop
        {
            get
            {
                return m_id_cfop;
            }

            set
            {
                m_id_cfop = value;
            }
        }

        private object m_id_cfop;

        public object id_limite
        {
            get
            {
                return m_id_limite;
            }

            set
            {
                m_id_limite = value;
            }
        }

        private object m_id_limite;

        public object tp_po
        {
            get
            {
                return m_tp_po;
            }

            set
            {
                m_tp_po = value;
            }
        }

        private object m_tp_po;

        public object dt_orc
        {
            get
            {
                return m_dt_orc;
            }

            set
            {
                m_dt_orc = value;
            }
        }

        private object m_dt_orc;

        public object dt_emissao
        {
            get
            {
                return m_dt_emissao;
            }

            set
            {
                m_dt_emissao = value;
            }
        }

        private object m_dt_emissao;

        public object dt_finalLib
        {
            get
            {
                return m_dt_finalLib;
            }

            set
            {
                m_dt_finalLib = value;
            }
        }

        private object m_dt_finalLib;

        public object tp_cq
        {
            get
            {
                return m_tp_cq;
            }

            set
            {
                m_tp_cq = value;
            }
        }

        private object m_tp_cq;

        public object tp_pi
        {
            get
            {
                return m_tp_pi;
            }

            set
            {
                m_tp_pi = value;
            }
        }

        private object m_tp_pi;

        public object tp_frete
        {
            get
            {
                return m_tp_frete;
            }

            set
            {
                m_tp_frete = value;
            }
        }

        private object m_tp_frete;

        public object cd_titulo
        {
            get
            {
                return m_cd_titulo;
            }

            set
            {
                m_cd_titulo = value;
            }
        }

        private object m_cd_titulo;

        public object ret_iss
        {
            get
            {
                return m_ret_iss;
            }

            set
            {
                m_ret_iss = value;
            }
        }

        private object m_ret_iss;

        public object cd_preco
        {
            get
            {
                return m_cd_preco;
            }

            set
            {
                m_cd_preco = value;
            }
        }

        private object m_cd_preco;

        public object usuario
        {
            get
            {
                return m_usuario;
            }

            set
            {
                m_usuario = value;
            }
        }

        private object m_usuario;

        public object id_localidade
        {
            get
            {
                return m_id_localidade;
            }

            set
            {
                m_id_localidade = value;
            }
        }

        private object m_id_localidade;

        public object cd_consrev
        {
            get
            {
                return m_cd_consrev;
            }

            set
            {
                m_cd_consrev = value;
            }
        }

        private object m_cd_consrev;

        public object cpfcnpj
        {
            get
            {
                return m_cpfcnpj;
            }

            set
            {
                m_cpfcnpj = value;
            }
        }

        private object m_cpfcnpj;

        public object cd_ie
        {
            get
            {
                return m_cd_ie;
            }

            set
            {
                m_cd_ie = value;
            }
        }

        private object m_cd_ie;

        public object ds_pessoa
        {
            get
            {
                return m_ds_pessoa;
            }

            set
            {
                m_ds_pessoa = value;
            }
        }

        private object m_ds_pessoa;

        public object tipo_endereco
        {
            get
            {
                return m_tipo_endereco;
            }

            set
            {
                m_tipo_endereco = value;
            }
        }

        private object m_tipo_endereco;

        public object cd_cpfcnpj
        {
            get
            {
                return m_cd_cpfcnpj;
            }

            set
            {
                m_cd_cpfcnpj = value;
            }
        }

        private object m_cd_cpfcnpj;

        public object ds_endereco
        {
            get
            {
                return m_ds_endereco;
            }

            set
            {
                m_ds_endereco = value;
            }
        }

        private object m_ds_endereco;

        public object ds_logradouro
        {
            get
            {
                return m_ds_logradouro;
            }

            set
            {
                m_ds_logradouro = value;
            }
        }

        private object m_ds_logradouro;

        public object ds_numero
        {
            get
            {
                return m_ds_numero;
            }

            set
            {
                m_ds_numero = value;
            }
        }

        private object m_ds_numero;

        public object ds_complemento
        {
            get
            {
                return m_ds_complemento;
            }

            set
            {
                m_ds_complemento = value;
            }
        }

        private object m_ds_complemento;

        public object ds_bairro
        {
            get
            {
                return m_ds_bairro;
            }

            set
            {
                m_ds_bairro = value;
            }
        }

        private object m_ds_bairro;

        public object cd_CEP
        {
            get
            {
                return m_cd_CEP;
            }

            set
            {
                m_cd_CEP = value;
            }
        }

        private object m_cd_CEP;

        public object vl_total
        {
            get
            {
                return m_vl_total;
            }

            set
            {
                m_vl_total = value;
            }
        }

        private object m_vl_total;

        public object vl_produtos
        {
            get
            {
                return m_vl_produtos;
            }

            set
            {
                m_vl_produtos = value;
            }
        }

        private object m_vl_produtos;

        public object vl_servicos
        {
            get
            {
                return m_vl_servicos;
            }

            set
            {
                m_vl_servicos = value;
            }
        }

        private object m_vl_servicos;

        public object vl_baseICMS
        {
            get
            {
                return m_vl_baseICMS;
            }

            set
            {
                m_vl_baseICMS = value;
            }
        }

        private object m_vl_baseICMS;

        public object vl_ICMS
        {
            get
            {
                return m_vl_ICMS;
            }

            set
            {
                m_vl_ICMS = value;
            }
        }

        private object m_vl_ICMS;

        public object baseISS
        {
            get
            {
                return m_baseISS;
            }

            set
            {
                m_baseISS = value;
            }
        }

        private object m_baseISS;

        public object vl_ISS
        {
            get
            {
                return m_vl_ISS;
            }

            set
            {
                m_vl_ISS = value;
            }
        }

        private object m_vl_ISS;

        public object vl_baseIPI
        {
            get
            {
                return m_vl_baseIPI;
            }

            set
            {
                m_vl_baseIPI = value;
            }
        }

        private object m_vl_baseIPI;

        public object vl_IPI
        {
            get
            {
                return m_vl_IPI;
            }

            set
            {
                m_vl_IPI = value;
            }
        }

        private object m_vl_IPI;

        public object cd_desc
        {
            get
            {
                return m_cd_desc;
            }

            set
            {
                m_cd_desc = value;
            }
        }

        private object m_cd_desc;

        public object vl_airrf
        {
            get
            {
                return m_vl_airrf;
            }

            set
            {
                m_vl_airrf = value;
            }
        }

        private object m_vl_airrf;

        public object vl_irrf
        {
            get
            {
                return m_vl_irrf;
            }

            set
            {
                m_vl_irrf = value;
            }
        }

        private object m_vl_irrf;

        public object vl_aif
        {
            get
            {
                return m_vl_aif;
            }

            set
            {
                m_vl_aif = value;
            }
        }

        private object m_vl_aif;

        public object vl_if
        {
            get
            {
                return m_vl_if;
            }

            set
            {
                m_vl_if = value;
            }
        }

        private object m_vl_if;

        public object vl_fabrica
        {
            get
            {
                return m_vl_fabrica;
            }

            set
            {
                m_vl_fabrica = value;
            }
        }

        private object m_vl_fabrica;

        public object vl_preven
        {
            get
            {
                return m_vl_preven;
            }

            set
            {
                m_vl_preven = value;
            }
        }

        private object m_vl_preven;

        public object vl_com
        {
            get
            {
                return m_vl_com;
            }

            set
            {
                m_vl_com = value;
            }
        }

        private object m_vl_com;

        public object vl_over
        {
            get
            {
                return m_vl_over;
            }

            set
            {
                m_vl_over = value;
            }
        }

        private object m_vl_over;

        public object vl_cp
        {
            get
            {
                return m_vl_cp;
            }

            set
            {
                m_vl_cp = value;
            }
        }

        private object m_vl_cp;

        public object vl_desc
        {
            get
            {
                return m_vl_desc;
            }

            set
            {
                m_vl_desc = value;
            }
        }

        private object m_vl_desc;

        public object vl_acre
        {
            get
            {
                return m_vl_acre;
            }

            set
            {
                m_vl_acre = value;
            }
        }

        private object m_vl_acre;

        public object vl_pzmedio
        {
            get
            {
                return m_vl_pzmedio;
            }

            set
            {
                m_vl_pzmedio = value;
            }
        }

        private object m_vl_pzmedio;

        public object vl_txjr
        {
            get
            {
                return m_vl_txjr;
            }

            set
            {
                m_vl_txjr = value;
            }
        }

        private object m_vl_txjr;

        public object vl_alicom
        {
            get
            {
                return m_vl_alicom;
            }

            set
            {
                m_vl_alicom = value;
            }
        }

        private object m_vl_alicom;

        public object vl_aliover
        {
            get
            {
                return m_vl_aliover;
            }

            set
            {
                m_vl_aliover = value;
            }
        }

        private object m_vl_aliover;

        public object tp_cp
        {
            get
            {
                return m_tp_cp;
            }

            set
            {
                m_tp_cp = value;
            }
        }

        private object m_tp_cp;

        public object vl_incilim
        {
            get
            {
                return m_vl_incilim;
            }

            set
            {
                m_vl_incilim = value;
            }
        }

        private object m_vl_incilim;

        public object vl_mais
        {
            get
            {
                return m_vl_mais;
            }

            set
            {
                m_vl_mais = value;
            }
        }

        private object m_vl_mais;

        public object vl_maisa
        {
            get
            {
                return m_vl_maisa;
            }

            set
            {
                m_vl_maisa = value;
            }
        }

        private object m_vl_maisa;

        public object vl_menos
        {
            get
            {
                return m_vl_menos;
            }

            set
            {
                m_vl_menos = value;
            }
        }

        private object m_vl_menos;

        public object vl_alimais
        {
            get
            {
                return m_vl_alimais;
            }

            set
            {
                m_vl_alimais = value;
            }
        }

        private object m_vl_alimais;

        public object vl_alimaisa
        {
            get
            {
                return m_vl_alimaisa;
            }

            set
            {
                m_vl_alimaisa = value;
            }
        }

        private object m_vl_alimaisa;

        public object vl_alimenos
        {
            get
            {
                return m_vl_alimenos;
            }

            set
            {
                m_vl_alimenos = value;
            }
        }

        private object m_vl_alimenos;

        public object vl_peso
        {
            get
            {
                return m_vl_peso;
            }

            set
            {
                m_vl_peso = value;
            }
        }

        private object m_vl_peso;

        public object cd_vinculo
        {
            get
            {
                return m_cd_vinculo;
            }

            set
            {
                m_cd_vinculo = value;
            }
        }

        private object m_cd_vinculo;

        public object tp_vinculo
        {
            get
            {
                return m_tp_vinculo;
            }

            set
            {
                m_tp_vinculo = value;
            }
        }

        private object m_tp_vinculo;

        public object id_vinculo
        {
            get
            {
                return m_id_vinculo;
            }

            set
            {
                m_id_vinculo = value;
            }
        }

        private object m_id_vinculo;

        public object cd_exped
        {
            get
            {
                return m_cd_exped;
            }

            set
            {
                m_cd_exped = value;
            }
        }

        private object m_cd_exped;

        public object vl_limite
        {
            get
            {
                return m_vl_limite;
            }

            set
            {
                m_vl_limite = value;
            }
        }

        private object m_vl_limite;

        public object vl_ped
        {
            get
            {
                return m_vl_ped;
            }

            set
            {
                m_vl_ped = value;
            }
        }

        private object m_vl_ped;

        public object vl_tit
        {
            get
            {
                return m_vl_tit;
            }

            set
            {
                m_vl_tit = value;
            }
        }

        private object m_vl_tit;

        public object vl_saldo
        {
            get
            {
                return m_vl_saldo;
            }

            set
            {
                m_vl_saldo = value;
            }
        }

        private object m_vl_saldo;

        public object id_almexp
        {
            get
            {
                return m_id_almexp;
            }

            set
            {
                m_id_almexp = value;
            }
        }

        private object m_id_almexp;

        public object vl_venliq
        {
            get
            {
                return m_vl_venliq;
            }

            set
            {
                m_vl_venliq = value;
            }
        }

        private object m_vl_venliq;

        public object vl_vencm
        {
            get
            {
                return m_vl_vencm;
            }

            set
            {
                m_vl_vencm = value;
            }
        }

        private object m_vl_vencm;

        public object vl_vencm2
        {
            get
            {
                return m_vl_vencm2;
            }

            set
            {
                m_vl_vencm2 = value;
            }
        }

        private object m_vl_vencm2;

        public object vl_vencom
        {
            get
            {
                return m_vl_vencom;
            }

            set
            {
                m_vl_vencom = value;
            }
        }

        private object m_vl_vencom;

        public object vl_venant
        {
            get
            {
                return m_vl_venant;
            }

            set
            {
                m_vl_venant = value;
            }
        }

        private object m_vl_venant;

        public object vl_aliped
        {
            get
            {
                return m_vl_aliped;
            }

            set
            {
                m_vl_aliped = value;
            }
        }

        private object m_vl_aliped;

        public object vl_aliped2
        {
            get
            {
                return m_vl_aliped2;
            }

            set
            {
                m_vl_aliped2 = value;
            }
        }

        private object m_vl_aliped2;

        public object vl_alipar
        {
            get
            {
                return m_vl_alipar;
            }

            set
            {
                m_vl_alipar = value;
            }
        }

        private object m_vl_alipar;

        public object vl_dificms
        {
            get
            {
                return m_vl_dificms;
            }

            set
            {
                m_vl_dificms = value;
            }
        }

        private object m_vl_dificms;

        public object ds_frete
        {
            get
            {
                return m_ds_frete;
            }

            set
            {
                m_ds_frete = value;
            }
        }

        private object m_ds_frete;

        public object vl_frete
        {
            get
            {
                return m_vl_frete;
            }

            set
            {
                m_vl_frete = value;
            }
        }

        private object m_vl_frete;

        public object cd_recval
        {
            get
            {
                return m_cd_recval;
            }

            set
            {
                m_cd_recval = value;
            }
        }

        private object m_cd_recval;

        public object vl_basest
        {
            get
            {
                return m_vl_basest;
            }

            set
            {
                m_vl_basest = value;
            }
        }

        private object m_vl_basest;

        public object vl_st
        {
            get
            {
                return m_vl_st;
            }

            set
            {
                m_vl_st = value;
            }
        }

        private object m_vl_st;

        public object cd_st
        {
            get
            {
                return m_cd_st;
            }

            set
            {
                m_cd_st = value;
            }
        }

        private object m_cd_st;

        public object vl_defere
        {
            get
            {
                return m_vl_defere;
            }

            set
            {
                m_vl_defere = value;
            }
        }

        private object m_vl_defere;

        public object vl_stcred
        {
            get
            {
                return m_vl_stcred;
            }

            set
            {
                m_vl_stcred = value;
            }
        }

        private object m_vl_stcred;

        public string cd_origem { get; set; }

        public object pedEz
        {
            get
            {
                return m_pedEz;
            }

            set
            {
                m_pedEz = value;
            }
        }

        private object m_pedEz;

        public int id_pedido
        {
            get
            {
                return m_id_pedido;
            }

            set
            {
                m_id_pedido = value;
            }
        }

        private int m_id_pedido;

        public string cd_pedorg
        {
            get
            {
                return m_cd_pedorg;
            }

            set
            {
                m_cd_pedorg = value;
            }
        }

        private string m_cd_pedorg;
        
    }
}
