using servico_pagamento.Core.Templates;
using servico_pagamento.Model.Request;
using System.Drawing;
using System.Drawing.Imaging;
using Zen.Barcode;

namespace servico_pagamento.Core
{
    public static class GeradorHtmlBoleto
    {
        public static string MontarHtmlBoleto(DadosPagamentoRequest dadosPagamento, DateTime dataVencimento)
        {
            string htmlBoleto = BoletoHtml.BOLETO_HTML;
            string styleHtmlBoleto = BoletoHtml.STYLE_HTML_BOLETO;
            string logoBanco = BoletoHtml.LOGO_BANCO_BOLETO;

            string base64CodigoDeBarras = GerarBase64CodigoDeBarras();

            return string.Format(htmlBoleto,
                                 dataVencimento.ToString(),
                                 dadosPagamento.ValorCurso.ToString(),
                                 dadosPagamento.NomeAluno,
                                 dadosPagamento.CpfAluno,
                                 base64CodigoDeBarras,
                                 styleHtmlBoleto,
                                 logoBanco
                                 );
        }

        private static string GerarBase64CodigoDeBarras()
        {
            BarcodeDraw barcodeDraw = BarcodeDrawFactory.Code128WithChecksum;

            string guidString = Guid.NewGuid().ToString();

            // 50 é a largura da imagem do código de barras
            System.Drawing.Image barcodeImage = barcodeDraw.Draw(guidString, 50);
            string base64CodigoDeBarras = ImageParaBase64(barcodeImage);

            return base64CodigoDeBarras;
        }

        private static string ImageParaBase64(System.Drawing.Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
