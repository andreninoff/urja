using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        
        private urjaEntities db = new urjaEntities();

        public ActionResult Index()
        {

            var listaDeProdutos = (from prod in db.produto orderby prod.id descending select prod).ToList();

            var categorias = new[]
            {
                new SelectListItem {Value = "", Text = "Selecione"},
                new SelectListItem {Value = "Bebidas", Text = "Bebidas"},
                new SelectListItem {Value = "Farmacia", Text = "Farmacia"},
                new SelectListItem {Value = "Laticinios", Text = "Laticinios"},
                new SelectListItem {Value = "Limpeza", Text = "Limpeza"},
                new SelectListItem {Value = "Padaria", Text = "Padaria"},
            };

            ViewBag.catSelecionada = "";
            ViewBag.categorias = new SelectList(categorias, "Value", "Text");
            ViewBag.Produtos = listaDeProdutos;
            ViewBag.feedbackUser = TempData["feedbackUser"];
            
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(produto postProd)
        {

            string categoria = Request.Form["categoria"];

            var listaDeProdutos = (from prod in db.produto orderby prod.id descending where prod.categoria.Contains(categoria) select prod).ToList();

            var categorias = new[]
           {
                new SelectListItem {Value = "", Text = "Selecione"},
                new SelectListItem {Value = "Bebidas", Text = "Bebidas"},
                new SelectListItem {Value = "Farmacia", Text = "Farmacia"},
                new SelectListItem {Value = "Laticinios", Text = "Laticinios"},
                new SelectListItem {Value = "Limpeza", Text = "Limpeza"},
                new SelectListItem {Value = "Padaria", Text = "Padaria"},
            };

            ViewBag.catSelecionada = categoria;
            ViewBag.categorias = new SelectList(categorias, "Value", "Text");
            ViewBag.Produtos = listaDeProdutos;

            if (categoria != "")
            {

                TempData["feedbackUser"] = "Voce filtrou por: " + categoria;
                ViewBag.feedbackUser = TempData["feedbackUser"];

            }

            return View();

        }

        public ActionResult Cadastrar()
        {

            var categorias = new[]
            {
                new SelectListItem {Value = "", Text = "Selecione"},
                new SelectListItem {Value = "Bebidas", Text = "Bebidas"},
                new SelectListItem {Value = "Farmacia", Text = "Farmacia"},
                new SelectListItem {Value = "Laticinios", Text = "Laticinios"},
                new SelectListItem {Value = "Limpeza", Text = "Limpeza"},
                new SelectListItem {Value = "Padaria", Text = "Padaria"},
            };

            ViewBag.categorias = new SelectList(categorias, "Value", "Text");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(produto postProd)
        {

            postProd.data_cadastro = DateTime.UtcNow.AddHours(-3);
            postProd.categoria = postProd.categoria.Trim();
            postProd.nome = postProd.nome.Trim();

            db.produto.Add(postProd);
            db.SaveChanges();

            TempData["feedbackUser"] = "Registro inserido com sucesso";

            return RedirectToAction("index");

        }

        public ActionResult Editar(int id)
        {
            produto objetoProduto = db.produto.Where(p => p.id == id).First();

            var categorias = new[]
            {
                new SelectListItem {Value = "", Text = "Selecione"},
                new SelectListItem {Value = "Bebidas", Text = "Bebidas"},
                new SelectListItem {Value = "Farmacia", Text = "Farmacia"},
                new SelectListItem {Value = "Laticinios", Text = "Laticinios"},
                new SelectListItem {Value = "Limpeza", Text = "Limpeza"},
                new SelectListItem {Value = "Padaria", Text = "Padaria"},
            };

            ViewBag.objeto = objetoProduto;
            ViewBag.categorias = new SelectList(categorias, "Value", "Text");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, produto postProd)
        {

            produto objetoProduto = db.produto.Where(p => p.id == id).First();

            objetoProduto.categoria = Request.Form["categoria"];
            objetoProduto.nome = Request.Form["nome"];
            objetoProduto.preco = Decimal.Parse(Request.Form["preco"]);

            db.SaveChanges();

            TempData["feedbackUser"] = "Registro alterado com sucesso";

            return RedirectToAction("index");

        }

        public ActionResult Excluir(int id)
        {

            produto objetoProduto = db.produto.Where(p => p.id == id).First();

            db.produto.Remove(objetoProduto);
            db.SaveChanges();

            TempData["feedbackUser"] = "Registro excluido com sucesso";

            return RedirectToAction("index");

        }

    }
}