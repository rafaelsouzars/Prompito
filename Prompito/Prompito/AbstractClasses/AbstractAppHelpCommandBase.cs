/*
 * 
 * Prompito
 * Version: v1.0.1
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using prompito.Prompito.Classes;

namespace prompito.Prompito.AbstractClasses
{
	abstract class AbstractAppHelpCommandBase
	{
        protected AbstractAppHelpCommandBase() { }
        abstract public void Run(Dictionary<string, (string, ActionCommand)> receives);
    }
}
