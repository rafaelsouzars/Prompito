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
using System.Collections.ObjectModel;

namespace prompito.Prompito.AbstractClasses
{
    public abstract class AbstractActionCommandBase
    {
        protected AbstractActionCommandBase() { }

        public abstract bool EqualsFlags(string flagMapper, string flagAdd);

        public abstract bool ContainsFlags(string flag);
        
        public abstract void Run(ArgsMapper argsMapper);        

    }
}
