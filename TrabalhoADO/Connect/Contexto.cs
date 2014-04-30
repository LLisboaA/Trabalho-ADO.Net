using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TrabalhoADO.Areas.Painel.Models;

namespace TrabalhoADO.Connect
{
    public class Contexto : IDisposable
    {
        private readonly SqlConnection _connection;

        public Contexto()
        {
            #region Conexão com Banco de Dados
            //TODO: 1º Criar conexão com banco de dadsos
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TrabalhoADO_Config"].ConnectionString);
            _connection.Open();
            #endregion
        }

        public void ExecutaComando(string strQuery)
        {
            var cmd = new SqlCommand 
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = _connection
            };

            cmd.ExecuteNonQuery();
        }

        public SqlDataReader ExecutaCmdComRetorno(string strQuery)
        {
            var cmd = new SqlCommand(strQuery, _connection);
            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}