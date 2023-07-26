using servico_pagamento.Core;
using servico_pagamento.Core.Interface;
using servico_pagamento.Model.Request;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento.Service
{
    public class BoletoService : IBoletoService
    {
        private readonly IHtmlParaPdf _htmlParaPdf;
        private readonly DateTime DATA_VENCIMENTO_BOLETO = DateTime.Now.AddDays(7);

        public BoletoService(IHtmlParaPdf htmlParaPdf)
        {
            _htmlParaPdf = htmlParaPdf;
        }

        public byte[] GerarPdfBoleto(DadosPagamentoRequest dadosPagamento)
        {
            string htmlBoleto = GeradorHtmlBoleto.MontarHtmlBoleto(dadosPagamento, DATA_VENCIMENTO_BOLETO);
            byte[] pdfBoleto = _htmlParaPdf.ConverterHtmlParaPdf(htmlBoleto);

            return pdfBoleto;
        }

    }
}
