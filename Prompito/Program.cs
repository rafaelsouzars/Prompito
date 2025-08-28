/*
 * 
 * Prompito
 * Version: v1.1.0
 * Description: Ferramenta C# para criação de CLI
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
// See https://aka.ms/new-console-template for more information
using Prompito;

var app = new Executer();

app.ScreenAbout(true);

app.InsertAppData(new {
    AppName = "prompito",
    Version = "v1.0.0",
    Description = "Ultilitário de CLI",
    ProfileURL = "https://github.com/rafaelsouzars",
    RepositorieURL = "https://github.com/rafaelsouzars/prompito"
});


app.ExecuteCommands(args);