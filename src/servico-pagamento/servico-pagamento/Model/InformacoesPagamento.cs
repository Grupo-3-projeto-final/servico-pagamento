namespace servico_pagamento.Model
{
    public class InformacoesPagamento
    {
        public int IdAluno { get; set; }
        public string NomeAluno { get; set; }
        public string CpfAluno { get; set; }
        public int IdCurso { get; set; }
        public double ValorCurso { get; set; }
        public string UrlArquivo { get; set; }
        public DateTime DataVencimento { get; set; }
        public int IdStatusPagamento { get; set; }
        public int IdTipoPagamento { get; set; }
    }
}
