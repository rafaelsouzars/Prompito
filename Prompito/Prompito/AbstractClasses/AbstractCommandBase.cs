/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using Prompito.Interfaces;

namespace Prompito.AbstractClasses
{
    public abstract class AbstractCommandBase : ICommand
    {
        protected AbstractCommandBase() { }

        public abstract void Execute();

    }
}
