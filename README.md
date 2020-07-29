Para executar o projeto:

Clique com o botão direito em cima do Projeto de Infra => UserServiceAccount.Data e escolha a opção Manage User Secrets
Configure exatamente o que tem de exemplo no appsettings na parte de ConnectionString, ou seja, cole isso no secrets.json e informe ali seus dados do banco.
{
	"ConnectionStrings": {
    "DefaultConnection": "Server=Server;Database=Database;Integrated Security=True"
  }
}

O primeiro endpoint exibido ali para se pegar o token é apenas como forma de mostrar o retorno do token, caso queiram testar de fato a autenticação,
configure o postman da seguinte maneira.

Em Authorization escolha OAuth2 e em seguida no botão Get New Access Token:

Token Name: qualquer um
Granty Type: Client Credentials
Access Token Url: https://localhost:6001/connect/token
ClientID: client
Client Secret: secret
Scope: api1
Client Authentication: Send as Basic Header


Não coloquei rotas autenticadas pois queria deixa o CRUD funcionando pelo Swagger.
Não criei um endpoint especifico para troca de senha pois como tem o update no CRUD e o CRUD é bem simples achei que seria redundante.


Por último, ao iniciar o projeto verificar se tanto o UserServiceAccount.Application e o UserServiceAccount.Identity estão para ser iniciados juntos.