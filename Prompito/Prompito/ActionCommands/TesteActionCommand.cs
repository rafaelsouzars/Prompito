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
                "-a",
                "--args",
                "Retorna os argumentos."
                );

            AddFlag(
                "-f",
                "--flags",
                "Retorna as flags."
                );

            AddFlag(
                "-s",
                "--sequence",
                "Testa uma sequência."
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
                if (argsMapper.TotalArgs == 1)
                {
                    Help();
                }
                else if (argsMapper.TotalArgs > 1)
                {
                    if (EqualsFlags(argsMapper.GetArgs("flag1"), "-m"))
                    {
                        Console.WriteLine(argsMapper.GetArgs("arg2"));
                    }

                    if (EqualsFlags(argsMapper.GetArgs("flag1"), "-a"))
                    {
                        argsMapper.ShowArgsMapper();
                    }

                    if (EqualsFlags(argsMapper.GetArgs("flag1"), "-f"))
                    {
                        foreach (var f in Flags)
                        {
                            Console.WriteLine("Flag => {0}", f);
                        }
                    }

                    /*if (EqualsFlags(argsMapper.GetArgs("flag1"), "-s"))
                    {
                        if (MappedLineTester(argsMapper, "arg1 flag1 flag2")) 
                        {
                            Console.WriteLine("Teste de sequência ok");
                        }
                        else 
                        {
                            Console.WriteLine("Sem correspondencia de sequência");
                        }
                    }*/

                    if (MappedLineTester(argsMapper,"arg1 flag1","flag1=-s")) 
                    {
                        Console.WriteLine("Teste de sequencia ok");
                    }

                    if (EqualsFlags(argsMapper.GetArgs("flag1"), "-h")) 
                    {
                        Help();
                    }                    
                }                


            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\tRun(ArgsMapper argsMapper) - {0}", exception.Message);
            }
        }
    }
}
