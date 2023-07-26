using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using servico_pagamento.Service.Interfaces;

namespace servico_pagamento.Service
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly string bucketName = "repository-gama-academy";
        private readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1; 
        private readonly IAmazonS3 s3Client;
        private readonly string accessKey;
        private readonly string secretKey;

        public AwsS3Service(string accessKey, string secretKey)
        {
            this.accessKey = accessKey;
            this.secretKey = secretKey;
            s3Client = new AmazonS3Client(accessKey, secretKey, bucketRegion);
        }

        public string EnviarPdfParaAwsS3(byte[] pdf, string nomeArquivo)
        {
            try {
                using (var memoryStream = new MemoryStream(pdf))
                {
                    var fileTransferUtility = new TransferUtility(s3Client);
                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        InputStream = memoryStream,
                        Key = nomeArquivo,
                        //CannedACL = S3CannedACL.PublicRead // Permite que o arquivo seja lido publicamente
                    };

                    fileTransferUtility.Upload(fileTransferUtilityRequest);
                    return BuscarUrlArquivo(nomeArquivo);
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Erro encontrado no servidor. Mensagem:", e.Message);
                return null;
            }
        }

        private string BuscarUrlArquivo(string nomeArquivo)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = nomeArquivo,
                Expires = DateTime.Now.AddMinutes(60) // O link expirará após 60 minutos
            };

            return s3Client.GetPreSignedURL(request);
        }
    }
}
