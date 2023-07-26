using servico_pagamento.Model.Request;

namespace servico_pagamento.Service.Interfaces
{
    public interface IPixService
    {
        byte[] GerarPdfPix(DadosPagamentoRequest dadosPagamento);
    }
}