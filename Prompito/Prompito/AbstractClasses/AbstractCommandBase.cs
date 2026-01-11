/*
 * 
 * Prompito
 * Version: v1.0.1
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using prompito.Prompito.Interfaces;

namespace prompito.Prompito.AbstractClasses
{
    public abstract class AbstractCommandBase : ICommand
    {
        protected AbstractCommandBase() { }

        public abstract void Execute();

    }
}
