<div align="center"><img width="200" height="200" alt="ganchito" src="" /></div>

# Prompito v1.0.0

![Version](https://img.shields.io/badge/version-1.0.0-green) ![Status](https://img.shields.io/badge/status-development-yellow) ![Github Release](https://img.shields.io/github/v/release/rafaelsouzars/prompito)

Utilitario para desenvolvimento de aplicações CLI.

### Notas da versão
- Primeira versão

## Introdução
O _Prompito_ é uma biblioteca de desenvolvimento de aplicações CLI.

## Instalação
Baixe o binário no repositorio clicando [aqui](https://github.com/rafaelsouzars/prompito/releases)
1. Baixe o binário
```
https://github.com/rafaelsouzars/prompito/releases
```

## Tutorial
Mapa de argumentos:
```powershell
./prompito init -r "repo"
```
| Key | Value |
|:---:|:-----:|
|arg1 | init|
|flag1| -r  |
|arg2 | repo|

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

class <MyActionCommand> : ActionCommand
{
	public override void Run(ArgsMapper argsMapper) 
	{
		// Implementation
	}
}
```
Adicionando seu _ActionCommand_:
```C#
using Prompito;
using <myActionCommand-namespace>;

var app = new Executer();

app.AddCommand("command", "description", new MyActionCommand());

app.ExecuteCommands(args);
```

## Exemplo
```C#
// MyAction
using Prompito.Classes;

namespace MyActionCommands
{
	class MyActionCommand : ActionCommand 
	{	
		public MyActionCommand()
        {            
            AddFlag(
                "-r",
                "--repo-hook",
                "Criar hook a partir de repositório de script"                
                );

            AddFlag(
                "-h",
                "--help",
                "Ajuda do comando"
                );
        }

		public override void Run (ArgsMapper argsMapper) 
		{
			try
            {
                if (argsMapper.GetArg.Count == 1) 
                {
                    if (hookFiles.GitHookDirectoryExist())
                    {
                        hookFiles.CreateHookFile();
                    } 
                }
                else if (argsMapper.GetArg.Count == 2) 
                {
                    if (string.Equals(argsMapper.GetArg["flag1"],"-r")) 
                    {
                        hookFiles.CreateHookFile(hookFiles.CreateFileRepositorieStream()); 
                    }
                    else 
                    {
                        throw new ArgumentException("Argumento não reconhecido: ", argsMapper.GetArg["flag1"]);
                    }
                }
                else if (argsMapper.GetArg.Count > 2) 
                {
                    throw new ArgumentException("Argumentos não reconhecidos: ", argsMapper.ToString());
                }
                                
               
            }
            catch (Exception exception) 
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}",exception.Message);
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

app.AddCommand(new MyActionCommand());

app.ExecuteCommands(args);
```

----------------------------------
<div align="center">

#### [Github: rafaelsouzars](https://rafaelsouzars.github.io)

</div>
