using Prompito.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompito.ActionCommands
{
    class TesteRootActionCommand : ActionCommand
    {
        public TesteRootActionCommand()
        {
            AddFlag(
                "-m",
                "--msg-line",
                "Mensagem de linha."
                );

            AddFlag(
                "-h",
                "--help",
                "Ajuda do commando TestRootActionCommand"
                );
        }

        public override void Run(ArgsMapper argsMapper)
        {
            try
            {
                if (argsMapper.TotalArgs > 0)
                {
                    if (MappedLineTester(argsMapper, "flag1 arg1", "flag1=-m"))
                    {
                        if (!string.IsNullOrEmpty(argsMapper.GetArgs("arg1")))
                        {
                            Console.WriteLine("Mensagem: {0}", argsMapper.GetArgs("arg1"));
                        }
                        else
                        {
                            Console.WriteLine("Sem mensagem!!!");
                        }
                    }
                    else if (MappedLineTester(argsMapper, "flag1", "flag1=-h"))
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
