using servico_pagamento.Core;
using servico_pagamento.Core.Interface;
using servico_pagamento.Repository;
using servico_pagamento.Repository.External;
using servico_pagamento.Repository.Interface;
using servico_pagamento.Service;
using servico_pagamento.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

// AWS
var appSettings = builder.Configuration.GetSection("AWSCredentials");
string accessKey = appSettings["AccessKey"];
string secretKey = appSettings["SecretKey"];

// Inje��o de deped�ncia
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddScoped<IAwsS3Service>(sp => new AwsS3Service(accessKey, secretKey));
builder.Services.AddScoped<IHtmlParaPdf, HtmlParaPdf>();
builder.Services.AddScoped<IBoletoService, BoletoService>();
builder.Services.AddScoped<IPixService, PixService>();


// Repositories 
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();


// Servico Aluno Api
builder.Services.AddHttpClient<IServicoAlunoApiRepository, ServicoAlunoApiRepository>()
.ConfigureHttpClient((sp, options) =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("ServicoAlunoApiSettings:UrlApi").Value);
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
