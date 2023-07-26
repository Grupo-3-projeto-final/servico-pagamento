using servico_pagamento.Enum;

namespace servico_pagamento.Model.Request
{
    public class DadosPagamentoRequest
    {
        public int IdAluno { get; set; }
        public string NomeAluno { get; set; }
        public string CpfAluno { get; set; }
        public int IdCurso { get; set; }
        public double ValorCurso { get; set; }
        public int IdTipoPagamento { get; set; }
    }
}
