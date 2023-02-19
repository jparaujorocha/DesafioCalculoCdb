

using DesafioCalculoCdb.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class InvestimentosController : Controller
    {
        IInvestimentoService _investimentoService;

        public InvestimentosController(IInvestimentoService investimentoService)
        {
            _investimentoService = investimentoService;
        }


        [System.Web.Mvc.HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var investimentos = await _investimentoService.GetInvestimentosAtivos();
            if (investimentos == null || !investimentos.Any())
            {
                return Content(HttpStatusCode.NotFound, "Nenhum investimento encontrado ou disponível no momento");
            }
            return Ok(investimentos);
        }

    }
}
