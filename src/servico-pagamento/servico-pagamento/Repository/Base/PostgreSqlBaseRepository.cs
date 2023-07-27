using Npgsql;
using System.Data;

namespace servico_pagamento.Repository.Base 
{ 
    public class PostgreSqlBaseRepository
    {
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;

        public PostgreSqlBaseRepository(ILogger<PostgreSqlBaseRepository> logger,
                                        IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            try
            {
                string connectionString = _configuration["CONNECTION_POSTGRES"];
                
                IDbConnection connection = new NpgsqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar conexão", "PostgreSqlDatabase");
                throw;
            }
        }
    }
}
