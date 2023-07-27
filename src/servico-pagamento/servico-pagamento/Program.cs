using servico_pagamento.Core;
using servico_pagamento.Core.Interface;
using servico_pagamento.Repository;
using servico_pagamento.Repository.External;
using servico_pagamento.Repository.Interface;
using servico_pagamento.Service;
using servico_pagamento.Service.Interfaces;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Configuration.AddEnvironmentVariables(); // Carrega as variáveis de ambiente
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// AWS

// Injeção de depedência
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddSingleton<IAwsS3Service, AwsS3Service>();
builder.Services.AddScoped<IHtmlParaPdf, HtmlParaPdf>();
builder.Services.AddScoped<IBoletoService, BoletoService>();
builder.Services.AddScoped<IPixService, PixService>();


// Repositories 
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();

// Servico Aluno Api
builder.Services.AddHttpClient<IServicoAlunoApiRepository, ServicoAlunoApiRepository>()
.ConfigureHttpClient((sp, options) =>
{
    options.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ServicoAlunoApi"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
