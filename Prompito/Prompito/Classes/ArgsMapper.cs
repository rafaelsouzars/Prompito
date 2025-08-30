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
        private readonly int _countArgs = 0;
        private readonly int _countFlags = 0;

        public ReadOnlyDictionary<string, string> GetArgsMapper
        {
            get
            {
                return _readOnlyArgs;
            }
        }        

        public int TotalArgs
        {
            get 
            {
                return _readOnlyArgs.Count;
            }
        }

        public int CountArgs
        {
            get
            {
                return _countArgs;
            }
        }

        public int CountFlags 
        {
            get 
            {
                return _countFlags;
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
                            _countArgs = indexArg;
                            indexArg++;                            
                        }
                        else
                        {
                            _args.Add($"flag{indexFlag}", arg);
                            _countFlags = indexFlag;
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

        public string GetArgs(string keyArgMapper)
        {
            try
            {
                var keyArgMapperRegex = new Regex("^((arg|flag)([1-9]+))$");

                if (!string.IsNullOrWhiteSpace(keyArgMapper))
                {
                    if (keyArgMapperRegex.IsMatch(keyArgMapper))
                    {
                        if (_readOnlyArgs.ContainsKey(keyArgMapper))
                        {
                            return _readOnlyArgs[keyArgMapper];
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Formato do parametro keyArgMapper, de GetArgs(), não reconhecido!", nameof(keyArgMapper));
                    }
                }
                else
                {
                    throw new ArgumentNullException(nameof(keyArgMapper),"O parametro de GetArgs() não pode ser nulo ou conter espaços!");
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
            return string.Empty;
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
