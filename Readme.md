# Sistema de Cadastro de Protocólo - Teste Desenvolvedor DBM

Este projeto é um sistema básico de cadastro e gerenciamento de protocolos, conforme solicitado no teste de desenvolvedor DBM. O sistema foi desenvolvido utilizando .NET Core 8, SQL Server, e implementando o padrão MVC (Model-View-Controller).

## Tecnologias Utilizadas

- **Backend**: ASP.NET Core 8
- **Banco de Dados**: SQL Server
- **ORM**: Entity Framework Core
- **Frontend**: HTML, CSS e Razor Views

## Instruções para Execução Local

Para rodar este projeto localmente, siga os passos abaixo:

### 1. Clone o repositório

Primeiro, clone este repositório em seu computador:

```bash
git clone https://github.com/SeuUsuario/NomeDoProjeto.git

### 2. Instale o .NET Core 8

Este projeto utiliza o .NET Core 8. Caso ainda não tenha o .NET Core 8 instalado, faça o download da versão mais recente em dotnet.microsoft.com/download/dotnet.

### 3. Instale o SQL Server

Este projeto utiliza o SQL Server para o banco de dados. Caso não tenha o SQL Server instalado, você pode utilizar o SQL Server Express ou qualquer outro banco SQL Server. Se preferir, pode também usar uma instância remota do SQL Server.

### 4. Configuração da String de Conexão

No arquivo appsettings.Development.json, configure a string de conexão para o seu banco de dados local:

{
  "ConnectionStrings": {
    "ConexaoPadrao": "Server=(localdb); Initial Catalog=ProtocoloDB; Integrated Security=True; TrustServerCertificate=True"
  }
}

Substitua "localdb" pelo nome do servidor.

### 5. Execução das Migrations

Execute o comando abaixo para aplicar as migrations no banco de dados e criar as tabelas automaticamente:

dotnet ef database update

Isso criará o banco de dados e todas as tabelas necessárias de acordo com a estrutura definida no modelo.

### 6. Execute o Projeto

Após configurar o banco de dados, execute o projeto localmente com o comando:

dotnet run

:sparkles: prontinho! a aplicação está funcionando! :sparkles:

