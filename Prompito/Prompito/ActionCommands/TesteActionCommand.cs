/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Ferramenta C# para criação de CLI
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
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
                "-p",
                "--program-name",
                "Nome do programa."
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
                
                MappedLineTester(argsMapper, "arg1", () => {

                    Help();

                });
                

                MappedLineTester(argsMapper, "arg1 flag1 arg2", "flag1=-m", () => {

                    Console.WriteLine(argsMapper.GetArgs("arg2"));

                });

                MappedLineTester(argsMapper, "arg1 flag1", "flag1=-a", () => {

                    argsMapper.ShowArgsMapper();

                });

                MappedLineTester(argsMapper, "arg1 flag1", "flag1=-f", () => {

                    foreach (var f in Flags)
                    {
                        Console.WriteLine("Flag => {0}", f);
                    }

                });

                MappedLineTester(argsMapper, "arg1 flag1", "flag1=-p", () => {

                    Console.WriteLine("{0}", AppDomain.CurrentDomain.FriendlyName);

                });

                MappedLineTester(argsMapper, "arg1 flag1", "flag1=-s", () => {

                    Console.WriteLine("Teste de sequencia ok");

                });

                MappedLineTester(argsMapper, "arg1 flag1", "flag1=-h", () => {

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
