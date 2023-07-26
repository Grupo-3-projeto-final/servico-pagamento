using AutoFixture;
using servico_pagamento.Model.Request;
using servico_pagamento.Service.Interfaces;
using servico_pagamento_test.Factories;

namespace servico_pagamento_test.Services
{
    public class PixServiceTest
    {
        private readonly PixServiceFactory _factory;
        private Fixture _fixture = new Fixture();

        public PixServiceTest()
        {
            _factory = new PixServiceFactory();
        }

        [Fact]
        public void ObterPdfPix_DeveRetornarPdfPix()
        {
            byte[] pdfPix = _fixture.Create<byte[]>();
            var dadosPagamento = _fixture.Create<DadosPagamentoRequest>();

            IPixService service = _factory.DadoPdfPix(pdfPix)
                                                .CriarServico();

            var response = service.GerarPdfPix(dadosPagamento);

            Assert.True(response.Any());
        }
    }
}
