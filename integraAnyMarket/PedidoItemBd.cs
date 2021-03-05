using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;

namespace integraAnyMarket
{

    public partial class PedidoItemBd
    {
        public PedidoItemBd()
        {
        }

        private void addParameter(string name, OracleType tp, int size, object value, OracleCommand con)
        {
            var parameter = new OracleParameter(name, tp, size);
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Value = value;
            con.Parameters.Add(parameter);
        }

        public void InsertItem(OracleCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SP_API_PEDS02_WEB";
            cmd.CommandType = CommandType.StoredProcedure;
            addParameter("P_ID_PEDIDO", OracleType.VarChar, 15, id_pedido, cmd);
            addParameter("P_ID_PRODUTO", OracleType.VarChar, 15, id_produto, cmd);
            addParameter("P_ID_EMPRESA", OracleType.VarChar, 15, 1, cmd);
            addParameter("P_QT_PEDIDA", OracleType.VarChar, 14, qt_pedida, cmd);
            addParameter("P_VL_PREVENU", OracleType.VarChar, 10, vl_prevenu, cmd);
            addParameter("P_VL_PREVENT", OracleType.VarChar, 15, vl_prevent, cmd);
            addParameter("P_VL_FRETE", OracleType.VarChar, 15, vl_frete, cmd);
            addParameter("P_CD_PRODUTO", OracleType.VarChar, 20, cd_produto, cmd);

            cmd.ExecuteNonQuery();
        }

        

        public int id_ps02
        {
            get
            {
                return m_id_ps02;
            }

            set
            {
                m_id_ps02 = value;
            }
        }

        private int m_id_ps02;

        public object id_pedido
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

        private object m_id_pedido;

        public object id_produto
        {
            get
            {
                return m_id_produto;
            }

            set
            {
                m_id_produto = value;
            }
        }

        private object m_id_produto;

        public object cd_ez
        {
            get
            {
                return m_cd_ez;
            }

            set
            {
                m_cd_ez = value;
            }
        }

        private object m_cd_ez;

        public object ds_produto
        {
            get
            {
                return m_ds_produto;
            }

            set
            {
                m_ds_produto = value;
            }
        }

        private object m_ds_produto;

        public object ds_unidade
        {
            get
            {
                return m_ds_unidade;
            }

            set
            {
                m_ds_unidade = value;
            }
        }

        private object m_ds_unidade;

        public object qt_pedida
        {
            get
            {
                return m_qt_pedida;
            }

            set
            {
                m_qt_pedida = value;
            }
        }

        private object m_qt_pedida;

        public object qt_sdfat
        {
            get
            {
                return m_qt_sdfat;
            }

            set
            {
                m_qt_sdfat = value;
            }
        }

        private object m_qt_sdfat;

        public object vl_prevenu
        {
            get
            {
                return m_vl_prevenu;
            }

            set
            {
                m_vl_prevenu = value;
            }
        }

        private object m_vl_prevenu;

        public object vl_prevent
        {
            get
            {
                return m_vl_prevent;
            }

            set
            {
                m_vl_prevent = value;
            }
        }

        private object m_vl_prevent;

        public object vl_prefab
        {
            get
            {
                return m_vl_prefab;
            }

            set
            {
                m_vl_prefab = value;
            }
        }

        private object m_vl_prefab;

        public object vl_alicms
        {
            get
            {
                return m_vl_alicms;
            }

            set
            {
                m_vl_alicms = value;
            }
        }

        private object m_vl_alicms;

        public object vl_baseicms
        {
            get
            {
                return m_vl_baseicms;
            }

            set
            {
                m_vl_baseicms = value;
            }
        }

        private object m_vl_baseicms;

        public object vl_redb
        {
            get
            {
                return m_vl_redb;
            }

            set
            {
                m_vl_redb = value;
            }
        }

        private object m_vl_redb;

        public object vl_icms
        {
            get
            {
                return m_vl_icms;
            }

            set
            {
                m_vl_icms = value;
            }
        }

        private object m_vl_icms;

        public object vl_Alipi
        {
            get
            {
                return m_vl_Alipi;
            }

            set
            {
                m_vl_Alipi = value;
            }
        }

        private object m_vl_Alipi;

        public object vl_baseipi
        {
            get
            {
                return m_vl_baseipi;
            }

            set
            {
                m_vl_baseipi = value;
            }
        }

        private object m_vl_baseipi;

        public object vl_ipi
        {
            get
            {
                return m_vl_ipi;
            }

            set
            {
                m_vl_ipi = value;
            }
        }

        private object m_vl_ipi;

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

        public object vl_mva
        {
            get
            {
                return m_vl_mva;
            }

            set
            {
                m_vl_mva = value;
            }
        }

        private object m_vl_mva;

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

        public object cd_org
        {
            get
            {
                return m_cd_org;
            }

            set
            {
                m_cd_org = value;
            }
        }

        private object m_cd_org;

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

        public string cd_produto { get; set; }
    }

   
}
