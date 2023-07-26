using servico_pagamento.Core.Templates;
using servico_pagamento.Model.Request;
using System.Text;

namespace servico_pagamento.Core
{
    public static class GeradorHtmlPix
    {
        public static string MontarHtmlPix(DadosPagamentoRequest dadosPagamento, DateTime dataVencimento)
        {
            string htmlPix = PixHtml.PIX_HTML;
            string styleHtmlPix = PixHtml.STYLE_HTML_PIX;

            string chaveAleatoria = GerarChaveAleatoria();


            return string.Format(htmlPix, 
                                styleHtmlPix, 
                                chaveAleatoria, 
                                dataVencimento, 
                                dadosPagamento.ValorCurso, 
                                dadosPagamento.NomeAluno);
        }

        private static string GerarChaveAleatoria()
        {
            string caracteresValidos = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int tamanhoChavePix = 15;

            Random random = new Random();
            StringBuilder chavePixBuilder = new StringBuilder();

            for (int i = 0; i < tamanhoChavePix; i++)
            {
                int index = random.Next(caracteresValidos.Length);
                char caractere = caracteresValidos[index];
                chavePixBuilder.Append(caractere);
            }

            string chavePix = chavePixBuilder.ToString();
            return chavePix;
        }
    }
}
