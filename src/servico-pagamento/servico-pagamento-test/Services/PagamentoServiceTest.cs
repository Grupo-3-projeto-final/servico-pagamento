using AutoFixture;
using NSubstitute;
using servico_pagamento.Enum;
using servico_pagamento.Model.Request;
using servico_pagamento.Service.Interfaces;
using servico_pagamento_test.Factories;

namespace servico_pagamento_test.Services
{
    public class PagamentoServiceTest
    {
        private readonly PagamentoServiceFactory _factory;
        private Fixture _fixture = new Fixture();

        public PagamentoServiceTest()
        {
            _factory = new PagamentoServiceFactory();
        }

        [Fact]
        public async Task ObterUrlPagamentoPix_DeveRetornarUrlPagamentoPix()
        {
            byte[] pixBoleto = _fixture.Create<byte[]>();
            var dadosPagamentoPix = _fixture.Create<DadosPagamentoRequest>();
            dadosPagamentoPix.IdTipoPagamento = (int)ETipoPagamento.Pix;

            IPagamentoService service = _factory.DadoSemUrlExistente()
                                                .DadoPixPdf(pixBoleto)
                                                .DadoUrlAws("UrlAws")
                                                .DadoSalvarInformacoesPagamento()
                                                .CriarServico();

            var response = await service.EtapasGeracaoUrl(dadosPagamentoPix);

            _factory._awsS3Service.Received(1).EnviarPdfParaAwsS3(Arg.Any<byte[]>(), Arg.Any<string>());

            Assert.True(!string.IsNullOrEmpty(response));
        }

        [Fact]
        public async Task ObterUrlPagamentoBoleto_DeveRetornarUrlPagamentoBoleto()
        {
            byte[] pixBoleto = _fixture.Create<byte[]>();
            var dadosPagamentoPix = _fixture.Create<DadosPagamentoRequest>();
            dadosPagamentoPix.IdTipoPagamento = (int)ETipoPagamento.Boleto;

            IPagamentoService service = _factory.DadoSemUrlExistente()
                                                .DadoPixPdf(pixBoleto)
                                                .DadoUrlAws("UrlAws")
                                                .DadoSalvarInformacoesPagamento()
                                                .CriarServico();

            var response = await service.EtapasGeracaoUrl(dadosPagamentoPix);

            _factory._awsS3Service.Received(1).EnviarPdfParaAwsS3(Arg.Any<byte[]>(), Arg.Any<string>());

            Assert.True(!string.IsNullOrEmpty(response));
        }

        [Fact]
        public async Task ObterUrlExistente_DeveRetornarUrlExistenteNoBanco()
        {
            var dadosPagamentoPix = _fixture.Create<DadosPagamentoRequest>();

            IPagamentoService service = _factory.DadoUrlExistente("UrlAwsExistente")
                                                .CriarServico();

            var response = await service.EtapasGeracaoUrl(dadosPagamentoPix);

            _factory._awsS3Service.DidNotReceive().EnviarPdfParaAwsS3(Arg.Any<byte[]>(), Arg.Any<string>());

            Assert.True(!string.IsNullOrEmpty(response));
        }
    }
}
