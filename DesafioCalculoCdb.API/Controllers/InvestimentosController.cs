using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.API.Controllers
{
    /// <summary>
    /// Teste
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class InvestimentosController : ControllerBase
    {
        private readonly IInvestimentoService _investimentoService;

        /// <summary>
        /// Controller
        /// </summary>
        public InvestimentosController(IInvestimentoService investimentoService)
        {
            _investimentoService = investimentoService;
        }

        /// <summary>
        /// Recupera a lista de investimentos ativos
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetInvestimentosAtivos")]
        public async Task<ActionResult<IEnumerable<InvestimentoDTO>>> Get()
        {
            var listInvestimentosAtivos = await _investimentoService.GetInvestimentosAtivos();

            if (listInvestimentosAtivos == null || listInvestimentosAtivos.Count() == 0)
            {
                return NotFound("Não foram encontrados investimentos ativos.");
            }
            return Ok(listInvestimentosAtivos);
        }

        /// <summary>
        /// Realiza a simulação de um investimento
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "PostSimulacaoInvestimento")]
        public async Task<ActionResult> Post([FromBody] InvestimentoDTO investimentoDTO)
        {
            if (investimentoDTO == null)
                return BadRequest("Requisição inválida");

            var dadosSimulacaoInvestimento = await _investimentoService.CalculaSimulacaoInvestimentos(investimentoDTO);

            return Ok(dadosSimulacaoInvestimento);
        }
    }
}
