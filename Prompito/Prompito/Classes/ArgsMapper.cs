/*
 * 
 * Prompito
 * Version: v1.1.0
 * Description: Ferramenta C# para criação de CLI
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Prompito.Classes
{
    /// <summary>
    /// class ArgsMapper. Mapeia o array de argumentos identificando como argumentos e flags.
    /// </summary>            
    /// <remarks>Exemplo. A entrada da linha "init -r 'olá'" é mapeada como "arg1","flag1" e "arg2"</remarks>
    public class ArgsMapper
    {
        private readonly Dictionary<string, string> _args = new();
        private ReadOnlyDictionary<string, string> _readOnlyArgs;

        public ReadOnlyDictionary<string, string> GetArg
        {
            get
            {
                return _readOnlyArgs;
            }
        }

        public int Count 
        {
            get 
            {
                return _readOnlyArgs.Count;
            }
        }

        public ArgsMapper(string[] args)
        {
            try
            {    
                var flagsRegex = new Regex(@"^(-[a-zA-Z0-9])|(--([a-zA-Z0-9]{2,})(-[a-zA-Z0-9]+)?)$");

                if (args.Length > 0)
                {
                    var indexArg = 1;
                    var indexFlag = 1;
                    foreach (var arg in args)
                    {
                        if (!flagsRegex.IsMatch(arg))
                        {
                            _args.Add($"arg{indexArg}", arg);
                            indexArg++;
                        }
                        else
                        {
                            _args.Add($"flag{indexFlag}", arg);
                            indexFlag++;
                        }
                    }
                    
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}\n", exception.Message);
            }

            _readOnlyArgs = new ReadOnlyDictionary<string, string>(_args);
        }

        public string[] ToArray ()
        {
            string[] args = new string[] { };
            if (_args.Count > 0)
            {
                foreach (var arg in _args)
                {
                    //Console.WriteLine(" {0} => {1}", arg.Key, arg.Value);
                    args = args.Append<string>($"{arg.Key} => {arg.Value}\n").ToArray();
                }
            }

            return args;
        }

        public void ShowArgsMapper () 
        {
            foreach(var elements in ToArray()) 
            {
                Console.WriteLine(elements);
            }
        }

        public override string ToString()
        {
            string args = "";
            if (_args.Count > 0)
            {
                foreach (var arg in _args)
                {
                    //Console.WriteLine(" {0} => {1}", arg.Key, arg.Value);
                    args += $"{arg.Key} => {arg.Value}\n";                    
                }
            }            

            return args;
        }
    }
}
