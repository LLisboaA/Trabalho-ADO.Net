using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoADO.Areas.Painel.Models;

namespace TrabalhoADO.Connect
{
    public class CategoriaApp
    {
        private Contexto _contexto;

        public List<Categoria> ListarTodasAsCategorias()
        {
            using (_contexto = new Contexto())
            {
                const string strQuery = "SELECT CATEGORIAID, NOMECATEGORIA FROM CATEGORIA";
                var dadosCategoria = _contexto.ExecutaCmdComRetorno(strQuery);
                return ConvertReaderToList(dadosCategoria);
            }
        }

        private List<Categoria> ConvertReaderToList(SqlDataReader reader)
        {
            var categoria = new List<Categoria>();

            while (reader.Read())
            {
                var tempCategoria = new Categoria()
                {
                    CategoriaId = int.Parse(reader["CategoriaId"].ToString()),
                    NomeCategoria = reader["NomeCategoria"].ToString()
                };

                categoria.Add(tempCategoria);
            }
            reader.Close();
            return categoria;
        }
    }
}