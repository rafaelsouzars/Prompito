/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Ferramenta C# para criação de CLI
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prompito.Classes;

namespace Prompito.ActionCommands
{
    class TesteActionCommand : ActionCommand
    {
        public TesteActionCommand() 
        {
            AddFlag(
                "-m",
                "--msg-line",
                "Mensagem de linha."
                );

            AddFlag(
                "-f",
                "--flags",
                "Retorna as flags."
                );

            AddFlag(
                "-h",
                "--help",
                "Ajuda do comando."
                );
        }

        public override void Run(ArgsMapper argsMapper)
        {
            try
            {
                if (argsMapper.Count == 1)
                {
                    Help();
                }
                else if (argsMapper.Count == 2)
                {
                    if (EqualsFlags(argsMapper.GetArg["flag1"], "-m"))
                    {
                        Console.WriteLine("Mensagem do Prompito");
                    }
                    else if (EqualsFlags(argsMapper.GetArg["flag1"], "-f"))
                    {
                        foreach (var f in Flags)
                        {
                            Console.WriteLine("Flag => {0}", f);
                        }
                    }
                    else if (EqualsFlags(argsMapper.GetArg["flag1"], "-h")) 
                    {
                        Help();
                    }                    
                }
                else if (argsMapper.Count > 2)
                {
                    throw new ArgumentException("Argumentos não reconhecidos: ", argsMapper.ToString());
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }
    }
}
