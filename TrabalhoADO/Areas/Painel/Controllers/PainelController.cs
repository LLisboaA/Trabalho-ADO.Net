using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrabalhoADO.Areas.Painel.Models;
using TrabalhoADO.Connect;
using Microsoft.AspNet.Identity;

namespace TrabalhoADO.Areas.Painel.Controllers
{
    public class PainelController : Controller
    {
        readonly Contexto contexto = new Contexto();
        readonly EmpresaApp empresaApp = new EmpresaApp();
        readonly CategoriaApp _categoriaApp = new CategoriaApp();

        public ActionResult _Index()
        {
            return View(empresaApp.ListarTodasAsEmpresas());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {

            #region Selecionando os Usuários no DB

            const string strQuery = "SELECT * FROM USUARIO";
            var dados = contexto.ExecutaCmdComRetorno(strQuery);

            #endregion

            #region Pegando os valores
            var listaDeUsuarios = new List<Usuario>();

            while (dados.Read())
            {
                var tempUser = new Usuario
                {
                    UsuarioId = int.Parse(dados["UsuarioId"].ToString()),
                    UserName = dados["UserName"].ToString(),
                    Pass = dados["Pass"].ToString()
                };

                listaDeUsuarios.Add(tempUser);
            }
            #endregion

            if (listaDeUsuarios.Any(item => ModelState.IsValid && item.UserName.Equals(usuario.UserName) && item.Pass.Equals(usuario.Pass)))
                return RedirectToAction("_Index");
            return View();
        }

        public ActionResult NovoCadastro()
        {
            ViewBag.Categorias = _categoriaApp.ListarTodasAsCategorias();
            return View(new Categoria());
        }

        [HttpPost]
        public ActionResult NovoCadastro(Empresa empresa)
        {
            if (ModelState == null || !ModelState.IsValid) ;
            empresaApp.Insert(empresa);
            return RedirectToAction("_Index");
        }

        public ActionResult Delete(int id)
        {
            empresaApp.Delete(id);
            return RedirectToAction("_Index");
        }

        public ActionResult Update(int id)
        {
            ViewBag.listaCategorias = _categoriaApp.ListarTodasAsCategorias();
            return View(empresaApp.Detalhes(id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Update(Empresa empresa, int id)
        {
            if (ModelState.IsValid)
            {
                empresaApp.Update(empresa, id);
                return RedirectToAction("_Index");
            }

            ViewBag.listaCategoria = _categoriaApp.ListarTodasAsCategorias();
            return View(empresa);
        }

        public ActionResult Detalhes(int id)
        {
            return View(empresaApp.Detalhes(id).FirstOrDefault());
        }

    }
}