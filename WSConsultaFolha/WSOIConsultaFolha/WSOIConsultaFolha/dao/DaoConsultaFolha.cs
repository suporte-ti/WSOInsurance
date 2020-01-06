using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WSOIConsultaFolha.dao
{
    public class DaoConsultaFolha
    {
        ConnectionStringSettings getString = WebConfigurationManager.ConnectionStrings["connOinsurance"] as ConnectionStringSettings;

        public DataTable GetDadosFolhaCorretora(int id_tp, int id_franquia, DateTime dt_inicial, DateTime dt_final)
        {
            DataTable dadosGuincho = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("[WebApi].[pro_getFolhaCorretoras]", conn);

                    cmd.CommandTimeout = 160;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tp", id_tp);
                    cmd.Parameters.AddWithValue("@idfranquia", id_franquia);
                    cmd.Parameters.AddWithValue("@dataInicial", Convert.ToDateTime(dt_inicial.ToString()));
                    cmd.Parameters.AddWithValue("@dataFinal", Convert.ToDateTime(dt_final.ToString()));
                    
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dadosGuincho);

                }
            }
            catch (SqlException ex)
            {
                throw new global::System.Data.StrongTypingException("'Procure o Administrador'", ex);
            }

            return dadosGuincho;
        }

        public DataTable GetAcesso(string ds_usuario, string ds_senha)
        {
            DataTable dadosAcesso = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("[Seguranca].[pro_getAutenticacaoWebService]", conn);

                    cmd.Parameters.AddWithValue("@ds_usuario", ds_usuario.ToString());
                    cmd.Parameters.AddWithValue("@ds_senha", ds_senha.ToString());

                    cmd.CommandTimeout = 160;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dadosAcesso);
                }
            }
            catch (SqlException ex)
            {
                throw new global::System.Data.StrongTypingException("'Procure o Administrador'", ex);
            }

            return dadosAcesso;
        }
    }
}