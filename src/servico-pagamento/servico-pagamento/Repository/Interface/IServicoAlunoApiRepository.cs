using servico_pagamento.Model;

namespace servico_pagamento.Repository.Interface
{
    public interface IServicoAlunoApiRepository
    {
        Task<bool> EnviarStatusPagamento(StatusPagamento statusPagamento);
    }
}
