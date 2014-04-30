using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using TrabalhoADO.Areas.Painel.Models;

namespace TrabalhoADO.Connect
{
    public class EmpresaApp
    {
        private Contexto _contexto;

        public void Insert(Empresa empresa)
        {
            var strQuery = "";
            strQuery += "INSERT INTO EMPRESA(NOME, TELEFONE, ENDERECO, CATEGORIAID)";
            strQuery += String.Format(" VALUES('{0}', '{1}','{2}','{3}')",
                empresa.Nome, empresa.Telefone, empresa.Endereco, empresa.CategoriaId);

            #region Destruindo conexão atual aberta

            using (_contexto = new Contexto())
            {
                _contexto.ExecutaComando(strQuery);
            }

            #endregion
        }

        public void Update(Empresa empresa, int id)
        {
            using (_contexto = new Contexto())
            {
                var strQuery =
                    string.Format(
                        "UPDATE EMPRESA SET NOME = '{0}', TELEFONE = '{1}', ENDERECO = '{2}', CATEGORIAID = '{3}' WHERE EMPRESAID = '{4}'", empresa.Nome, empresa.Telefone, empresa.Endereco, empresa.CategoriaId, id);
                _contexto.ExecutaComando(strQuery);
            }
        }

        public void Delete(int id)
        {
            using (_contexto = new Contexto())
            {
                var strQuery = String.Format("DELETE FROM EMPRESA WHERE EMPRESAID = {0}", id);
                _contexto.ExecutaComando(strQuery);
            }
        }

        public List<Empresa> Detalhes(int id)
        {
            using (_contexto = new Contexto())
            {
                string strQuery = string.Format("SELECT E.EMPRESAID, E.NOME, E.TELEFONE, E.ENDERECO, C.NOMECATEGORIA FROM EMPRESA E, CATEGORIA C WHERE E.CATEGORIAID = C.CATEGORIAID AND EMPRESAID = '{0}'", id);
                var dadosEmpresa = _contexto.ExecutaCmdComRetorno(strQuery);
                return ConvertReaderToList(dadosEmpresa);
            }
        }

        public List<Empresa> ListarTodasAsEmpresas()
        {
            using (_contexto = new Contexto())
            {
                const string strQuery = "SELECT E.EMPRESAID, E.NOME, E.TELEFONE, E.ENDERECO, C.NOMECATEGORIA FROM EMPRESA E, CATEGORIA C WHERE E.CATEGORIAID = C.CATEGORIAID";
                var dadosEmpresa = _contexto.ExecutaCmdComRetorno(strQuery);
                return ConvertReaderToList(dadosEmpresa);
            }
        }

        private List<Empresa> ConvertReaderToList(SqlDataReader reader)
        {
            var empresa = new List<Empresa>();

            while (reader.Read())
            {
                var tempEmpresa = new Empresa()
                {
                    EmpresaId = int.Parse(reader["EmpresaId"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Telefone = reader["Telefone"].ToString(),
                    Endereco = reader["Endereco"].ToString(),
                    NomeCategoria = reader["NomeCategoria"].ToString()
                };

                empresa.Add(tempEmpresa);
            }
            reader.Close();
            return empresa;
        }
    }
}