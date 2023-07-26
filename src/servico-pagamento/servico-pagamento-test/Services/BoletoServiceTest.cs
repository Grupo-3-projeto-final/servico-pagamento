using AutoFixture;
using servico_pagamento.Model.Request;
using servico_pagamento.Service.Interfaces;
using servico_pagamento_test.Factories;

namespace servico_pagamento_test.Services
{
    public class BoletoServiceTest
    {
        private readonly BoletoServiceFactory _factory;
        private Fixture _fixture = new Fixture();

        public BoletoServiceTest()
        {
            _factory = new BoletoServiceFactory();
        }

        [Fact]
        public void ObterPdfBoleto_DeveRetornarPdfBoleto()
        {
            byte[] pdfBoleto = _fixture.Create<byte[]>();
            var dadosPagamento = _fixture.Create<DadosPagamentoRequest>();

            IBoletoService service = _factory.DadoPdfBoleto(pdfBoleto)
                                                .CriarServico();

            var response = service.GerarPdfBoleto(dadosPagamento);

            Assert.True(response.Any());
        }
    }
}
