using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoADO.Areas.Painel.Models;
using TrabalhoADO.Connect;

namespace TrabalhoADO.Controllers
{
    public class HomeController : Controller
    {
        readonly EmpresaApp empresaApp = new EmpresaApp();
        public ActionResult Index()
        {
            return View(empresaApp.ListarTodasAsEmpresas());
        }
    }
}