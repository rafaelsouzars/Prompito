/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */

namespace Prompito.AbstractClasses
{
    public abstract class AbstractActionCommandBase
    {        
        protected bool _DEBUG = false;
        
        public bool DEBUG { get => _DEBUG; set { _DEBUG = value; } }

        protected AbstractActionCommandBase() { }

        public abstract void Run();

        public void Help(string appName, string description)
        {
            Console.WriteLine("[ {0} ]\n\tDescrição: {1}", appName, description);
        }

    }
}
