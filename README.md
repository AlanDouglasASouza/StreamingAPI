# API de Streaming

A aplicação tem como objetivo ser uma API com funções de CRUD, que cadastra usuários recebendo seu nome e seu e-mail e senha e é capaz de cadastrar Playlists. Permite exibir uma lista dos usuários já cadastrados e de suas playlists e tem opções de editar ou apagar as playlists.

<div align="center" >
  ![image](https://github.com/user-attachments/assets/d08ff70a-4c95-4860-b986-82ca9a3ec58f)  
</div>

## 🚀 Começando

Faça o clone desse repositório:

```
git clone git@github.com:AlanDouglasASouza/StreamingAPI.git
```

### 📋 Pré-requisitos

```
ASP.NET Core
Visual Studio 2022
```

### 🔧 Instalação

Para iniciar a aplicação, estando dentro do ambiante Visual Studio, é necessário verificar se possui o SQL Server Express LocalDB configurado. Estando tudo correto podemos dar o comando para rodar as migrations. O projeto usa o Entity Framework então podemos criar as tableas e rodar as migrations com os comando: 

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Se caso não tiver o ef instalado vc pode adicionar ele globalmente com o comando:

```
dotnet ef database update
```

Tendo as configurações feitas corretamente é só fazer o build e rodar o projeto. Ele usa o Swagger para documentar a API.


## 🛠️ Construído com

-   [ASP.NET Core](https://dotnet.microsoft.com/pt-br/apps/aspnet) - Framework web
-   [Visual Basic](https://visualstudio.microsoft.com/pt-br/) - Gerenciador de Dependências

## ✒️ Autores

-   **Equipe Unip** - _Desenvolvimento e construção do projeto_ - [Alan Douglas](https://github.com/AlanDouglasASouza)

## 🎁 Expressões de gratidão
