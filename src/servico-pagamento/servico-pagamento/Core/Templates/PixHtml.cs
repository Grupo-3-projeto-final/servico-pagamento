namespace servico_pagamento.Core.Templates
{
    public class PixHtml
    {
        public const string PIX_HTML = @"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>PIX Payment</title>
                {0}
            </head>
            <body>
                <div class=""container"">
                    <div class=""qr-code"">
                        <img src=""https://ps.w.org/doqrcode/assets/icon-256x256.png?rev=2143781"" alt=""QR Code PIX""> 
                    </div>
                    <div class=""chave-pix"">
                        Chave PIX: {1}
                    </div>
                    <div class=""info"">
                        <span>Beneficiário:</span> Ânima Educação
                    </div>
                    <div class=""info"">
                        <span>Data de Vencimento:</span> {2}
                    </div>
                    <div class=""info"">
                        <span>Valor:</span> R$ {3}
                    </div>
                    <div class=""info"">
                        <span>Pagante:</span> {4}
                    </div>
                </div>
            </body>
            </html>";


        public const string STYLE_HTML_PIX = @"<style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f5f5f5;
                        text-align: center;
                        padding: 20px;
                    }

                    .container {
                        max-width: 400px;
                        margin: 0 auto;
                        border: 1px solid #ccc;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 10px;
                    }

                    .qr-code {
                        margin-bottom: 20px;
                    }

                    .chave-pix {
                        font-size: 18px;
                        font-weight: bold;
                        margin-bottom: 10px;
                    }

                    .info {
                        font-size: 14px;
                        margin-bottom: 10px;
                    }

                    .info span {
                        font-weight: bold;
                    }
                </style>";
    }

}
