using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSOIConsultaFolha.Controllers
{
    public class FolhaCorretorasController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("DadosFolhaCorretores")]
        public DataTable DadosFolhaCorretores(string ds_usuario, string ds_senha, int id_tp, int id_franquia, DateTime dt_inicial, DateTime dt_final)
        {

            DataTable dadosAcesso;
            DataTable dadosFolhaCorretora;

            dao.DaoConsultaFolha daoCarregaDados = new dao.DaoConsultaFolha();

            dadosAcesso = daoCarregaDados.GetAcesso(ds_usuario, ds_senha);

            string fl_ativo = dadosAcesso.Rows[0]["fl_ativo"].ToString();

            try
            {
                if (fl_ativo == "1")
                {
                    dadosFolhaCorretora = daoCarregaDados.GetDadosFolhaCorretora(id_tp, id_franquia, dt_inicial, dt_final);
                    return dadosFolhaCorretora;
                }
                else
                {
                    id_tp = 9;
                    id_franquia = 0;
                    dt_inicial = Convert.ToDateTime("1900-01-01".ToString());
                    dt_final = Convert.ToDateTime("1900-01-01".ToString());

                    dadosFolhaCorretora = daoCarregaDados.GetDadosFolhaCorretora(id_tp, id_franquia, dt_inicial, dt_final);
                    return dadosFolhaCorretora;
                }
            }
            catch (Exception ex)
            {
                throw new global::System.Data.StrongTypingException("'Procure o Administrador'", ex);
            }
        }

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}