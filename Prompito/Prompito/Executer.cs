﻿/*
 * 
 * Prompito
 * Version: v1.1.0
 * Description: Ferramenta C# para criação de CLI
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using Prompito.Interfaces;
using Prompito.Classes;
using Prompito.ConsoleScreen;

namespace Prompito
{
    /// <summary>
    /// Class Executer(). Executa os commandos adicionados.
    /// </summary>   
    class Executer : IExecuter
    {        
        private static object? _appData;
        private static bool _DEBUG_MODE = false;        
        private HelpCommand _appHelperActionCommand = new HelpCommand();
        private Dictionary<string, (string, ActionCommand)> _receivers = new Dictionary<string, (string, ActionCommand)>();               

        /// <summary>
        /// O Método InsertAppData. Recebe um object com as informações da aplicação.
        /// </summary>
        /// <param name="appData">Tipo object</param>
        /// <remarks>Exemplo: new { AppName = "", Version = "", Description = "", ProfileURL = "", RepositorieURL = "" }</remarks>
        public void InsertAppData(object appData)          
        {
            try
            {                
                _appData = appData ?? throw new ArgumentNullException("O ProgramData não pode ser nulo", nameof(appData));

                var appName = _appData?.GetType().GetProperty("AppName")?.GetValue(_appData) ?? "";
                var version = _appData?.GetType().GetProperty("Version")?.GetValue(_appData) ?? "";
                var description = _appData?.GetType().GetProperty("Description")?.GetValue(_appData) ?? "";
                var profileURL = _appData?.GetType().GetProperty("ProfileURL")?.GetValue(_appData) ?? "";
                var repositorieURL = _appData?.GetType().GetProperty("RepositorieURL")?.GetValue(_appData) ?? "";                

                Screen.About(new AppData((string)appName, (string)version, (string)description, (string)profileURL, (string)repositorieURL));               
                
            }
            catch (Exception exception)
            {
                Console.WriteLine("ERROR: {0}", exception);
            }
        }

        /// <summary>
        /// Método ExecuteCommand(). Recebe um array de argumentos, do Console, e repassa para o metodo Run() dos Commands.
        /// </summary>
        /// <param name="args">Array de argumentos repassa pelo console</param>        
        public void ExecuteCommands(string[] args) 
        {
            try
            {
                if (args.Length >= 1)
                {
                    if (_receivers.Keys.Contains<string>(args[0]))
                    {
                        if (_receivers.TryGetValue(args[0], out (string, ActionCommand) receiver))
                        {
                            var command = new Command<ActionCommand>(receiver.Item2, r => r.Run(new ArgsMapper(args)));
                            command.Execute();
                        }
                    }
                    else
                    {
                        throw new ArgumentException($" {args[0],-5} - Commando não reconhecido\n");
                    }

                }
                else
                {
                    if (_appHelperActionCommand != null)
                    {
                        var command = new Command<HelpCommand>(_appHelperActionCommand, r => r.Run(_receivers));
                        command.Execute();
                    }
                    else
                    {
                        Console.WriteLine("\tSem ação para este comando.\n\tUtilize o método AppHelperActionCommand() para adicionar a ajuda do programa.\n");
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        /// <summary>
        /// Método AddCommand(). Adiciona um commando ao Executer
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="newActionCommand"></param>        
        public void AddCommand (string commandName, ActionCommand newActionCommand)
        {
            _receivers.Add(commandName, ("", newActionCommand));
        }

        /// <summary>
        /// Método AddCommand. Adiciona um commando ao Executer
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="newActionCommand"></param> 
        public void AddCommand (string commandName, string description, ActionCommand newActionCommand)
        {
            _receivers.Add(commandName, (description, newActionCommand));
        }
        

        /// <summary>
        /// Método ScreenAbout. Ativa e desativa a tela do App.
        /// </summary>
        /// <param name="activeScreen"></param>        
        public void ScreenAbout (bool activeScreen) 
        {
            Screen.ActiveScreenLetter = activeScreen;
        }

    }
}
    

