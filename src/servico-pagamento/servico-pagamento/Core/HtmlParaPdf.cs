using servico_pagamento.Core.Interface;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace servico_pagamento.Core
{
    public class HtmlParaPdf : IHtmlParaPdf
    {
        private readonly IConverter _converterPdf;

        public HtmlParaPdf(IConverter converterPdf)
        {
            _converterPdf = converterPdf;
        }

        public byte[] ConverterHtmlParaPdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlContent,
                        WebSettings= { DefaultEncoding = "utf-8"}
                    }
                }
            };

            return _converterPdf.Convert(doc);
        }
    }
}
