using NSubstitute;
using servico_pagamento.Core.Interface;
using servico_pagamento.Service;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento_test.Factories
{
    public class BoletoServiceFactory
    {
        internal readonly IHtmlParaPdf _htmlParaPdf = Substitute.For<IHtmlParaPdf>();

        public BoletoServiceFactory DadoPdfBoleto(byte[] pdfBoleto)
        {
            _htmlParaPdf.ConverterHtmlParaPdf(Arg.Any<string>()).Returns(pdfBoleto);
            return this;
        }

        public IBoletoService CriarServico()
        {
            return new BoletoService(_htmlParaPdf);
        }

    }
}
