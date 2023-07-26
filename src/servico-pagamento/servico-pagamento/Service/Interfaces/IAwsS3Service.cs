namespace servico_pagamento.Service.Interfaces
{
    public interface IAwsS3Service
    {
        string EnviarPdfParaAwsS3(byte[] pdf, string nomeArquivo);
    }
}