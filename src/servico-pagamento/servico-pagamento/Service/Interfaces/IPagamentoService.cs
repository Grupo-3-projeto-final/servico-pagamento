using servico_pagamento.Model;
using servico_pagamento.Model.Request;

namespace servico_pagamento.Service.Interfaces
{
    public interface IPagamentoService
    {
        Task<string> EtapasGeracaoUrl(DadosPagamentoRequest dadosPagamento);
        Task<bool> AtualizarStatusPagamento(StatusPagamento statusPagamento);
    }
}