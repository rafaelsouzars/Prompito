/*
 * 
 * Ganchito
 * Version: v1.0.0
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */

using Prompito.Classes;

namespace Prompito.Interfaces
{
    interface IExecuter
    {        
        public void Init() 
        {
            
        }

        public void InsertAppData(AppData appData) 
        {
        
        }

        public void ExecuteCommand(string[] args) 
        {
            // Implementation
        }

        public void AddRootCommand(ActionCommand rootActionCommand) 
        {
            
        }

        public void AddCommand(string commandName, ActionCommand newCommand)
        {

        }

        public void AddCommand(string commandName, string description, ActionCommand newCommand) 
        {
            
        }

        public void ScreenAbout(bool activeScreen) 
        {
            
        }
    }
}
