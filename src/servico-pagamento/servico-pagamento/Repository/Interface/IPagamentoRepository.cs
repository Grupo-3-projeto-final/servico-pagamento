using servico_pagamento.Model;

namespace servico_pagamento.Repository.Interface
{
    public interface IPagamentoRepository
    {
        Task<string> BuscarUrlExistente(int idAluno, int idCurso);
        Task<int> SalvarInformacoesPagamento(InformacoesPagamento informacoesPagamento);
        Task<int> AtualizarStatusPagamento(int idAluno, int idCurso, int statusPagamento);
    }
}
