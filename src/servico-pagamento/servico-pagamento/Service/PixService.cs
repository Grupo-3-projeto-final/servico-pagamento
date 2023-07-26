using servico_pagamento.Core;
using servico_pagamento.Core.Interface;
using servico_pagamento.Model.Request;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento.Service
{
    public class PixService : IPixService
    {
        private readonly IHtmlParaPdf _htmlParaPdf;
        private readonly DateTime DATA_VENCIMENTO_PIX = DateTime.Now.AddDays(1);

        public PixService(IHtmlParaPdf htmlParaPdf)
        {
            _htmlParaPdf = htmlParaPdf;
        }

        public byte[] GerarPdfPix(DadosPagamentoRequest dadosPagamento)
        {
            string htmlPix = GeradorHtmlPix.MontarHtmlPix(dadosPagamento, DATA_VENCIMENTO_PIX);
            byte[] pdfPix = _htmlParaPdf.ConverterHtmlParaPdf(htmlPix);

            return pdfPix;
        }
    }
}
