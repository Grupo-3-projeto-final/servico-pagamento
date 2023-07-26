using NSubstitute;
using NSubstitute.ReturnsExtensions;
using servico_pagamento.Model;
using servico_pagamento.Model.Request;
using servico_pagamento.Repository.Interface;
using servico_pagamento.Service;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento_test.Factories
{
    public class PagamentoServiceFactory
    {
        internal readonly IAwsS3Service _awsS3Service = Substitute.For<IAwsS3Service>();
        internal readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
        internal readonly IServicoAlunoApiRepository _servicoAlunoApiRepository = Substitute.For<IServicoAlunoApiRepository>();
        internal readonly IBoletoService _boletoService = Substitute.For<IBoletoService>();
        internal readonly IPixService _pixService = Substitute.For<IPixService>();

        public PagamentoServiceFactory DadoUrlExistente(string urlExistente)
        {
            _pagamentoRepository.BuscarUrlExistente(Arg.Any<int>(), Arg.Any<int>()).Returns(urlExistente);
            return this;
        }

        public PagamentoServiceFactory DadoSemUrlExistente()
        {
            _pagamentoRepository.BuscarUrlExistente(Arg.Any<int>(), Arg.Any<int>()).ReturnsNull();
            return this;
        }

        public PagamentoServiceFactory DadoBoletoPdf(byte[] pdfBoleto)
        {
            _boletoService.GerarPdfBoleto(Arg.Any<DadosPagamentoRequest>()).Returns(pdfBoleto);
            return this;
        }

        
        public PagamentoServiceFactory DadoPixPdf(byte[] pdfPix)
        {
            _pixService.GerarPdfPix(Arg.Any<DadosPagamentoRequest>()).Returns(pdfPix);
            return this;
        }

        public PagamentoServiceFactory DadoUrlAws(string urlArquivo)
        {
            _awsS3Service.EnviarPdfParaAwsS3(Arg.Any<byte[]>(), Arg.Any<string>()).Returns(urlArquivo);
            return this;
        }

        public PagamentoServiceFactory DadoSalvarInformacoesPagamento()
        {
            _pagamentoRepository.SalvarInformacoesPagamento(Arg.Any<InformacoesPagamento>());
            return this;
        }

        public IPagamentoService CriarServico()
        {
            return new PagamentoService(_awsS3Service, 
                                        _pagamentoRepository, 
                                        _boletoService, 
                                        _pixService, 
                                        _servicoAlunoApiRepository);
        }

    }
}
