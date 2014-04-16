using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoADO.Areas.Painel.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Insira seu Login")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Insira sua Senha")]
        public string Pass { get; set; }
    }
}