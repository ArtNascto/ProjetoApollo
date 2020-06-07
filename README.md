# ProjetoApolo

- Pré requisitos
	- Backend
		- [Dotnet core](https://dotnet.microsoft.com/download)
		- [Postgres](https://www.postgresql.org/download/)
	
	- Gerenciador de banco de dados
		- [DBeaver](https://dbeaver.io/download/)
	
	- FrontEnd
		- [Node](https://yarnpkg.com/getting-started/install)
		- [Yarn](https://yarnpkg.com/getting-started/install)
	
- Preparar ambiente
	- Pasta `Projeto Apollo\angular`
		- Executar o comando `yarn`
	- Pasta `Projeto Apollo\aspnet-core`
		- Executar o comando `dotnet restore`
	- Pasta `Projeto Apollo\aspnet-core\src\ProjetoApollo.Web.Host`
		- Alterar o arquivo `appsettings.json`, colocando sua conectionString
	- Pasta `Projeto Apollo\aspnet-core\src\ProjetoApollo.EntityFrameworkCore`
		- Executar o comando `dotnet ef update database`
- Executando
	- FrontEnd
		- Pasta `Projeto Apollo\angular`
			- Executar o comando `yarn start`
	- Backend
		- Pasta `Projeto Apollo\aspnet-core\src\ProjetoApollo.Web.Host`
			- Executar o comando `dotnet run`
