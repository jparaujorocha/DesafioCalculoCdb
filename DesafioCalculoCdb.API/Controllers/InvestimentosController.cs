using DesafioCalculoCdb.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace DesafioCalculoCdb.Api.Controllers
{
    public class InvestimentosController : Controller
    {
        IInvestimentoService _investimentoService;

        public InvestimentosController(IInvestimentoService investimentoService)
        {
            _investimentoService = investimentoService;
        }


        // GET: api/Investimentos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Investimentos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Investimentos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Investimentos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Investimentos/5
        public void Delete(int id)
        {
        }
    }
}
