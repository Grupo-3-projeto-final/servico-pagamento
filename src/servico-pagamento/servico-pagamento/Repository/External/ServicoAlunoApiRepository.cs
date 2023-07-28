using servico_pagamento.Model;
using servico_pagamento.Repository.Interface;
using System.Text;
using System.Text.Json;

namespace servico_pagamento.Repository.External
{
    public class ServicoAlunoApiRepository : IServicoAlunoApiRepository
    {
        private readonly HttpClient _httpClient;

        public ServicoAlunoApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> EnviarStatusPagamento(StatusPagamento statusPagamento)
        {
            string requestUrl = "pagamentoAluno/atualizar-status";
            string jsonStatusPagamento = JsonSerializer.Serialize(statusPagamento);

            using (var httpContent = new StringContent(jsonStatusPagamento, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync(requestUrl, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }         
        }

    }
}
