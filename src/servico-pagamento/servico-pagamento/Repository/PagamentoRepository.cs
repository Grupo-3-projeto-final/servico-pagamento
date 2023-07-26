using Dapper;
using servico_pagamento.Model;
using servico_pagamento.Repository.Base;
using servico_pagamento.Repository.Interface;
using System.Data;

namespace servico_pagamento.Repository
{
    public class PagamentoRepository : PostgreSqlBaseRepository, IPagamentoRepository
    {
        private readonly ILogger<PagamentoRepository> _logger;
        public PagamentoRepository(ILogger<PagamentoRepository> logger, IConfiguration configuration) : base(logger, configuration)
        {
            _logger = logger;
        }

        public async Task<string> BuscarUrlExistente(int idAluno, int idCurso)
        {
            try
            {
                string sql = @"select 
                                alp.url_arquivo 
                           from aluno_pagamento alp
                           where alp.id_aluno = @idAluno
                           and alp.id_curso = @idCurso";

                using IDbConnection connection = GetDbConnection();
                return await connection.QueryFirstOrDefaultAsync<string>(sql, new { idAluno, idCurso });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar URL existente para idAluno: {idAluno} e idCurso: {idCurso}");
                throw;
            }
        }

        public async Task<int> SalvarInformacoesPagamento(InformacoesPagamento informacoesPagamento)
        {
            try
            {
                string sql = @"insert into aluno_pagamento 
                            (id_aluno, id_curso, url_arquivo, data_vencimento, valor, id_status_pagamento, id_tipo_pagamento)
                            values (:IdAluno, :IdCurso, :UrlArquivo, :DataVencimento, :ValorCurso, :IdStatusPagamento, :IdTipoPagamento)";

                using IDbConnection connection = GetDbConnection();
                return await connection.ExecuteAsync(sql, informacoesPagamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao salvar informaçoes de pagamento para idAluno: " +
                    $"{informacoesPagamento.IdAluno} e idCurso: {informacoesPagamento.IdCurso}");
                throw;
            }
        }

        public async Task<int> AtualizarStatusPagamento(int idAluno, int idCurso, int statusPagamento)
        {
            try
            {
                string sql = @"update aluno_pagamento 
                                   set
                                       id_status_pagamento = :statusPagamento
                                   where
                                       id_aluno = :idAluno
                                       and id_curso  = :idCurso";

                using IDbConnection connection = GetDbConnection();
                return await connection.ExecuteAsync(sql, new {idAluno, idCurso, statusPagamento});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar status de pagamento para idAluno: " +
                    $"{idAluno} e idCurso: {idCurso}");
                throw;
            }
        }


    }
}
