/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using Prompito.Classes;

namespace Prompito.AbstractClasses
{
	abstract class AbstractAppHelpCommandBase
	{
        protected AbstractAppHelpCommandBase() { }
        abstract public void Run(Dictionary<string, (string, ActionCommand)> receives);
    }
}
