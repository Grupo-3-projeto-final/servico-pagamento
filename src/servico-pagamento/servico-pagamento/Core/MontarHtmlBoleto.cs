using IronBarCode;
using servico_pagamento.Core.Templates;
using servico_pagamento.Model.Request;
using System.Drawing.Imaging;

namespace servico_pagamento.Core
{
    public static class GeradorHtmlBoleto
    {
        public static string MontarHtmlBoleto(DadosPagamentoRequest dadosPagamento, DateTime dataVencimento)
        {
            string htmlBoleto = BoletoHtml.BOLETO_HTML;
            string styleHtmlBoleto = BoletoHtml.STYLE_HTML_BOLETO;
            string logoBanco = BoletoHtml.LOGO_BANCO_BOLETO;

            return string.Format(htmlBoleto,
                                 dataVencimento.ToString(),
                                 dadosPagamento.ValorCurso.ToString(),
                                 dadosPagamento.NomeAluno,
                                 dadosPagamento.CpfAluno,
                                 styleHtmlBoleto,
                                 logoBanco
                                 );
        }
    }
}
