namespace servico_pagamento.Core.Interface
{
    public interface IHtmlParaPdf
    {
        byte[] ConverterHtmlParaPdf(string htmlContent);
    }
}
