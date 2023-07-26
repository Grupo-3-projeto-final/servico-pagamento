using servico_pagamento.Model.Request;

namespace servico_pagamento.Service.Interfaces
{
    public interface IBoletoService
    {
        byte[] GerarPdfBoleto(DadosPagamentoRequest dadosPagamento);
    }
}