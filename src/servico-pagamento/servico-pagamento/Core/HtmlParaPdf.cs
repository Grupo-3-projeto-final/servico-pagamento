using servico_pagamento.Core.Interface;

namespace servico_pagamento.Core
{
    public class HtmlParaPdf : IHtmlParaPdf
    {
        public byte[] ConverterHtmlParaPdf(string htmlContent)
        {
            var renderer = new ChromePdfRenderer();

            var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

            return pdfDocument.Stream.ToArray();
        }
    }
}
