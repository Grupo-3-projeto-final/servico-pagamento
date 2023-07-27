# Documentação do Serviço de Pagamento :money_with_wings:

## Introdução :wave:

Este serviço foi desenvolvido para receber solicitações de pagamento, gerar o PDF para realizar o pagamento através de dois métodos disponíveis (Pix ou Boleto) :credit_card:, armazená-lo no Amazon S3 :package: e retornar a URL para o frontend. Além disso, o serviço é responsável por notificar o serviço aluno sobre o status do pagamento para que possam liberar ou não a inscrição do aluno.

## Funcionalidades :gear:

O serviço de pagamento oferece as seguintes funcionalidades:

- **Recebimento de solicitação de pagamento**:
  - O serviço recebe uma solicitação contendo os detalhes do pagamento, como valor, método de pagamento escolhido (Pix ou Boleto) e informações do aluno.

- **Notificação do serviço aluno**:
  - O serviço de pagamento notifica o serviço aluno sobre o status do pagamento (Aprovado ou Negado) para que possam atualizar o status da inscrição do aluno de acordo.

## Tecnologias Utilizadas :computer:

O serviço de pagamento foi desenvolvido utilizando a seguinte stack de tecnologias:

- **ASP.NET Core 6.0** :large_orange_diamond: Plataforma de desenvolvimento para a criação de aplicativos Web e APIs.
- **Amazon S3** :package: Serviço de armazenamento de objetos da Amazon Web Services (AWS).
- **Xunit** :white_check_mark: Framework de testes unitários para testar a lógica de negócio do serviço.

## Configuração e Execução :wrench:

Para compilar e executar o projeto, siga as instruções abaixo:

1. **Requisitos do Sistema**:
   - Certifique-se de ter instalado o SDK do .NET 6.0 em sua máquina.

2. **Clonar o Repositório**:
   - Clone este repositório para o seu ambiente de desenvolvimento.

3. **Configurar as Credenciais da AWS**:
   - Antes de executar o serviço, é necessário configurar as credenciais de acesso à AWS, incluindo a chave de acesso e a chave secreta, que serão usadas para acessar o serviço S3. Certifique-se de que as credenciais tenham permissão para acessar o Amazon S3.

4. **Compilar o Projeto**:
   - Abra um terminal ou prompt de comando na raiz do projeto e execute o seguinte comando:
     ```
     dotnet build
     ```

5. **Executar o Projeto**:
   - Para iniciar o serviço de pagamento, execute o seguinte comando:
     ```
     dotnet watch run
     ```

## Testes Unitários :white_check_mark:

Os testes unitários são fundamentais para garantir a qualidade do serviço. Eles foram implementados utilizando o framework Xunit e estão localizados no diretório de testes do projeto.

Para executar os testes unitários, siga os passos abaixo:

1. Abra um terminal ou prompt de comando na raiz do projeto.

2. Execute o seguinte comando:
 ```
 dotnet test
 ```
