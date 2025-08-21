<div align="center"><img width="200" height="200" alt="ganchito" src="" /></div>

# Prompito v1.0.0

![Version](https://img.shields.io/badge/version-1.0.0-green) ![Status](https://img.shields.io/badge/status-development-yellow) ![Github Release](https://img.shields.io/github/v/release/rafaelsouzars/prompito)

Utilitario para criação de aplicações CLI.

### Notas da versão
- Primeira versão

## Introdução
O _Prompito_ é uma biblioteca que facilia o desenvolvimento de aplicações CLI.

## Instalação
Baixe o binário no repositorio clicando [aqui](https://github.com/rafaelsouzars/prompito/releases)
1. Baixe o binário
```
https://github.com/rafaelsouzars/prompito/releases
```

## Tutorial
Para iniciar o projeto:
```C#
using Promito;

// Cria um Executer
var app = new Executer();

// Recebe o array de argumentos do app
app.ExecuteCommands(args);
```
Para iniciar o _letreiro_ com as informações do _app_:
```C#
using Prompito;

// Cria um Executer
var app = new Executer();

// Ativa o letreito
app.ScreenAbout(true);

// Insere as informações do app
app.InsertData(new {
	AppName = "my-app",
	Version = "v1.0.0",
	Description = "My first app",
	ProfileURL = "https://github.com/<profile>"
	RepositorieURL = "https://github.com/<profile>/<my-app>"
});

// Recebe o array de argumentos do app
app.ExecuteCommands(args);
```
Criando um _ActionCommand_:
```C#
using Prompito.Classes;

class MyClassAction : ActionCommand
{
	public override void Run(string[] args) 
	{
		// Implementation
	}
}
```
Adicionando seu _ActionCommand_:
```C#
using Prompito;
using <my-namespace>;

var app = new Executer();

app.AddCommand(new MyClassAction());

app.ExecuteCommands(args);
```

## Exemplo
```C#
// MyAction
using Prompito.Classes;

namespace MyActionCommands
{
	class MyAction : ActionCommand 
	{		
		public override void Run (string[] args) 
		{
			if (args.Length != 0 and args[0] == "init") 
			{
			
			}
			if (args.Length == 0) 
			{
				Console.Write("Help!");
			}
		}
	}
}

```
```C#
// Program.cs
using Prompito;
using MyActionCommands;

var app = new Executer();

app.AddCommand(new MyAction());

app.ExecuteCommands(args);
```

----------------------------------
<div align="center">

#### [Github: rafaelsouzars](https://rafaelsouzars.github.io)

</div>
