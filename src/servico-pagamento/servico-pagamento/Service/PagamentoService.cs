using servico_pagamento.Enum;
using servico_pagamento.Enums;
using servico_pagamento.Model;
using servico_pagamento.Model.Request;
using servico_pagamento.Repository.Interface;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento.Service
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IAwsS3Service _awsS3Service;
        private readonly IPagamentoRepository _pagamentoRepository;
        public readonly IServicoAlunoApiRepository _servicoAlunoApiRepository;
        private readonly IBoletoService _boletoService;
        private readonly IPixService _pixService;

        public PagamentoService(IAwsS3Service awsS3Service, IPagamentoRepository PagamentoRepository, 
                                IBoletoService boletoService, IPixService pixService, IServicoAlunoApiRepository servicoAlunoApiRepository)
        {
            _awsS3Service = awsS3Service;
            _pagamentoRepository = PagamentoRepository;
            _boletoService = boletoService;
            _pixService = pixService;
            _servicoAlunoApiRepository = servicoAlunoApiRepository;
        }

        public async Task<string> EtapasGeracaoUrl(DadosPagamentoRequest dadosPagamento)
        {
            string urlExistente = await _pagamentoRepository.BuscarUrlExistente(dadosPagamento.IdAluno, dadosPagamento.IdCurso);

            if (!string.IsNullOrEmpty(urlExistente))
            {
                return urlExistente;
            }

            string urlArquivo = GerarUrl(dadosPagamento);

            var informacoesPagamento = MontarInformacoesPagamento(dadosPagamento, urlArquivo);
            await _pagamentoRepository.SalvarInformacoesPagamento(informacoesPagamento);

            return urlArquivo;
        }

        private string GerarUrl(DadosPagamentoRequest dadosPagamento)
        {
            string nomeArquivo;
            byte[] pdfBoleto;

            if (dadosPagamento.IdTipoPagamento == (int)ETipoPagamento.Boleto)
            {
                nomeArquivo = $"Boleto{dadosPagamento.IdAluno}_{DateTime.Now}.pdf";
                pdfBoleto = _boletoService.GerarPdfBoleto(dadosPagamento);
            }
            else
            {
                nomeArquivo = $"Pix{dadosPagamento.IdAluno}_{DateTime.Now}.pdf";
                pdfBoleto = _pixService.GerarPdfPix(dadosPagamento);

            }

            string result = _awsS3Service.EnviarPdfParaAwsS3(pdfBoleto, nomeArquivo);
            return result;
        }

        public async Task<bool> AtualizarStatusPagamento(StatusPagamento statusPagamento)
        {
            bool result = await _servicoAlunoApiRepository.EnviarStatusPagamento(statusPagamento);

            int status = statusPagamento.Pago ? (int)EStatusPagamento.Aprovado : (int)EStatusPagamento.Recusado;
            await _pagamentoRepository.AtualizarStatusPagamento(statusPagamento.IdAluno, statusPagamento.IdCurso, status);

            return result;
        }

        private InformacoesPagamento MontarInformacoesPagamento(DadosPagamentoRequest dadosPagamento, string urlArquivo)
        {
            return new InformacoesPagamento()
            {
                IdAluno = dadosPagamento.IdAluno,
                CpfAluno = dadosPagamento.CpfAluno,
                NomeAluno = dadosPagamento.NomeAluno,
                IdCurso = dadosPagamento.IdCurso,
                ValorCurso = dadosPagamento.ValorCurso,
                DataVencimento = dadosPagamento.IdTipoPagamento == (int)ETipoPagamento.Boleto ? 
                                                                        DateTime.Now.AddDays(7) : 
                                                                        DateTime.Now.AddDays(1),
                UrlArquivo = urlArquivo,
                IdStatusPagamento = (int)EStatusPagamento.Pendente,
                IdTipoPagamento = dadosPagamento.IdTipoPagamento
            };

        }
    }
}
