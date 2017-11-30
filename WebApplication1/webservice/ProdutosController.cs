using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication1.webservice
{
    public class ProdutosController : ApiController
    {
        private urjaEntities db = new urjaEntities();

        public IEnumerable<produto> Get()
        {
            var listaDeProdutos = (from prod in db.produto orderby prod.id descending select prod).ToList();
            return listaDeProdutos;
        }

    }
}