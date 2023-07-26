using NSubstitute;
using servico_pagamento.Core.Interface;
using servico_pagamento.Service;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento_test.Factories
{
    public class PixServiceFactory
    {
        internal readonly IHtmlParaPdf _htmlParaPdf = Substitute.For<IHtmlParaPdf>();

        public PixServiceFactory DadoPdfPix(byte[] pdfPix)
        {
            _htmlParaPdf.ConverterHtmlParaPdf(Arg.Any<string>()).Returns(pdfPix);
            return this;
        }

        public IPixService CriarServico()
        {
            return new PixService(_htmlParaPdf);
        }

    }
}
