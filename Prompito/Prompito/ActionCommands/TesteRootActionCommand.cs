/*
 * 
 * Prompito
 * Version: v1.0.1
 * Description: Ferramenta C# para criação de CLI
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using prompito.Prompito.Classes;

namespace prompito.Prompito.ActionCommands
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
                "-p",
                "--program-name",
                "Exibe o nome do programa."
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

                MappedLineTester(argsMapper, "flag1", "flag1=-p", () => {

                    Console.WriteLine("Nome do programa: {0}", AppDomain.CurrentDomain.FriendlyName);

                });

                MappedLineTester(argsMapper, "flag1 arg1", "flag1=-m", () => {

                    if (!string.IsNullOrEmpty(argsMapper.GetArgs("arg1")))
                    {
                        Console.WriteLine("Mensagem: {0}", argsMapper.GetArgs("arg1"));
                    }
                    else
                    {
                        Console.WriteLine("Sem mensagem!!!");
                    }

                });

                MappedLineTester(argsMapper, "flag1", "flag1=-h", () => {

                    Help();

                });

            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\tRun(ArgsMapper argsMapper) - {0}", exception.Message);
            }
        }
    }
}
