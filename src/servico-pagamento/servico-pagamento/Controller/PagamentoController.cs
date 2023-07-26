using IdentityGama.Filters;
using Microsoft.AspNetCore.Mvc;
using servico_pagamento.Model;
using servico_pagamento.Model.Request;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento.Controllers
{
    [Authorization(Role = "Administrator")]
    [Authentication]
    [Route("api/v1/pagamento")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly ILogger<PagamentoController> _logger;
        public PagamentoController(IPagamentoService pagamentoService, ILogger<PagamentoController> logger) 
        {
            _pagamentoService = pagamentoService;
            _logger = logger;
        }

        /// <summary>
        /// Geração da url do boleto 
        /// </summary>
        /// <param name="dadosPagamento"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gerar-url")]
        public async Task<ActionResult<string>> GerarUrl([FromBody] DadosPagamentoRequest dadosPagamento)
        {
            try
            {
                string urlBoleto = await _pagamentoService.EtapasGeracaoUrl(dadosPagamento);
                return Ok(urlBoleto);
            }
            catch (Exception ex)
            {
                string msg = $"Erro ao gerar o url de pagamento, IdAluno: {dadosPagamento.IdAluno}. Mensagem: " + ex.Message;
                _logger.LogError(ex, msg);
                return Content(msg);
            }
        }

        /// <summary>
        /// Atualiza o status de pagamento 
        /// </summary>
        /// <param name="statusPagamento"></param>
        /// <returns></returns>
        [Route("atualizar-status")]
        [HttpPut]
        public async Task<IActionResult> AtualizarStatus(StatusPagamento statusPagamento)
        {
            try
            {
                bool result = await _pagamentoService.AtualizarStatusPagamento(statusPagamento);

                if (result)
                {
                    return Ok("Status do pagamento enviado e atualizado com sucesso.");
                }
                else
                {
                    return StatusCode(500, "Ocorreu um erro ao enviar o status do pagamento.");
                }
            }
            catch (Exception ex)
            {
                string msg = $"Erro ao atualizar o status do pagamento, IdAluno: {statusPagamento.IdAluno}. Mensagem: " + ex.Message;
                _logger.LogError(ex, msg);
                return Content(msg);
            }
        }
    }
}
