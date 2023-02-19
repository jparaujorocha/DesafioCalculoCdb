using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using DesafioCalculoCdb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace DesafioCalculoCdb.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class InvestimentosController : Controller
    {
        private readonly IInvestimentoService _investimentoService;

        public InvestimentosController(IInvestimentoService investimentoService)
        {
            _investimentoService = investimentoService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> Get()
        {
            var investimentos = await _investimentoService.GetInvestimentosAtivos();
            if (investimentos == null && !investimentos.Any())
            {
                return new NotFoundObjectResult("Investimentos não disponíveis nesse momento. Contate o seu gerente!");
            }
            return new OkObjectResult(investimentos);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Post([FromBody] InvestimentoDto investimentoDto)
        {
            if (investimentoDto == null || investimentoDto.Id == 0)
                return new BadRequestObjectResult("É necessário selecionar um investimento!");

            await _investimentoService.CalculaSimulacaoInvestimentos(investimentoDto);

            return new OkObjectResult(investimentoDto);
        }
    }
}
