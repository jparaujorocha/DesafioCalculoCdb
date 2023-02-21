using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace DesafioCalculoCdb.Api.Controllers
{
    /// <summary>
    /// Classe para ações envolvendo investimentos
    /// </summary>
    ///
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ApiController]
    [Produces("application/json")]
    public class InvestimentosController : System.Web.Mvc.Controller
    {
        private readonly IInvestimentoService _investimentoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="investimentoService"></param>
        public InvestimentosController(IInvestimentoService investimentoService)
        {
            _investimentoService = investimentoService;
        }

        /// <summary>
        /// Busca de investimentos ativos
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [System.Web.Http.HttpGet]
        public async Task<System.Web.Mvc.ActionResult> GetRecuperarInvestimentosAtivos()
        {
            var investimentos = await _investimentoService.GetInvestimentosAtivos();

            if (investimentos == null || !investimentos.Any())
            {
                throw new Exception("Lista de investimentos indispoível no momento.");
            }

            var jsonResult = new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = investimentos
            };

            return jsonResult;
        }

        /// <summary>
        /// Calculo de investimentos
        /// </summary>
        /// <param name="investimentoDto"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ActionResult> PostCalcularInvestimentos([Microsoft.AspNetCore.Mvc.FromBody] InvestimentoDto investimentoDto)
        {
            if (investimentoDto == null || investimentoDto.Id == 0)
                throw new Exception("É necessário selecionar um investimento!");

            await _investimentoService.CalculaSimulacaoInvestimentos(investimentoDto);

            var jsonResult = new JsonResult()
            {
                Data = investimentoDto
            };

            return jsonResult;
        }
    }
}
