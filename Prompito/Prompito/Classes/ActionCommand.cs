/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using Prompito.AbstractClasses;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Prompito.Classes
{
    class ActionCommand : AbstractActionCommandBase
    {               
        private Dictionary<string, (string, string)> _flags = new Dictionary<string, (string, string)>();
       
        public Dictionary<string, (string, string)> Flags
        {
            get
            {
                return _flags;
            }

            private set
            {
                _flags = value;
            }
        }

        private bool FlagVerify(string flag)
        {
            var flagRegex = new Regex(@"^(-[a-zA-Z0-9])$");

            if (flagRegex.IsMatch(flag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ExtendFlagVerify(string flag)
        {
            var extendFlagRegex = new Regex(@"^(--([a-zA-Z0-9]{2,})(-[a-zA-Z0-9]+)?)$");

            if (extendFlagRegex.IsMatch(flag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ContainsFlag(string flag)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(flag))
                {
                    if (FlagVerify(flag))
                    {
                        if (_flags.ContainsKey(flag))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Formato de flag não reconhecido. ", flag);
                    }
                }
                else
                {
                    throw new ArgumentNullException(nameof(flag), "O arqumento não pode ser nulo");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
                return false;
            }

        }

        /// <summary>
        /// Método AddFlag(). Adiciona uma flag ao ActionCommand.
        /// </summary>
        /// <param name="flag">String com a flag</param>        
        /// <remarks>Exemplos de formatos de flags: "-e", "-X" e "-8".</remarks>
        public void AddFlag(string flag)
        {
            try
            {
                if (FlagVerify(flag))
                {
                    _flags.Add(flag, ("", ""));
                }
                else
                {
                    throw new ArgumentException("Formato de flag não reconhecido. ", flag);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }


        }

        /// <summary>
        /// Método AddFlag(). Adiciona uma flag ao ActionCommand e sua versão extendida.
        /// </summary>
        /// <param name="flag">String com a flag</param>
        /// <param name="extendFlag">String com a flag extendida</param>
        /// <remarks>Exemplo de formato de flag extendida: "--add-flags".</remarks>
        public void AddFlag(string flag, string extendFlag)
        {
            try
            {
                if (FlagVerify(flag) && ExtendFlagVerify(extendFlag))
                {
                    _flags.Add(flag, (extendFlag, ""));
                }
                if (!FlagVerify(flag))
                {
                    throw new ArgumentException("Formato de flag não reconhecido. ", flag);
                }
                else if (!ExtendFlagVerify(extendFlag))
                {
                    throw new ArgumentException("Formato de flag extendida não reconhecido. ", extendFlag);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }

        /// <summary>
        /// Método AddFlag(). Adiciona uma flag ao ActionCommand, sua versão extendida e uma descrição.
        /// </summary>
        /// <param name="flag">String com a flag</param>
        /// <param name="extendFlag">String com a flag extendida</param>
        /// <param name="descriptionFlag">String com a descrição da flag</param>
        /// <remarks>Exemplo de formato de flag extendida: "--add-flags".</remarks>
        public void AddFlag(string flag, string extendFlag, string descriptionFlag)
        {
            try
            {
                if (FlagVerify(flag) && ExtendFlagVerify(extendFlag))
                {
                    _flags.Add(flag, (extendFlag, descriptionFlag));
                }
                if (!FlagVerify(flag))
                {
                    throw new ArgumentException("Formato de flag não reconhecido. ", flag);
                }
                else if (!ExtendFlagVerify(extendFlag))
                {
                    throw new ArgumentException("Formato de flag extendida não reconhecido. ", extendFlag);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }


        /// <summary>
        /// Método AddExtendFlag(). Adiciona uma flag extendida a uma flag já existente.
        /// </summary>
        /// <param name="flag">String com a flag para busca</param>
        /// <param name="extendFlag">String com a flag extendida</param>        
        /// <remarks>Exemplo de formato de flag extendida: "--add-flags".</remarks>
        public void AddExtendFlag(string flag, string extendFlag)
        {
            try
            {

                if (ContainsFlag(flag))
                {
                    var tuple = _flags[flag];

                    tuple = (extendFlag, tuple.Item2);

                    _flags[flag] = tuple;
                }
                else
                {
                    throw new ArgumentException("Item não encontrado", flag);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }

        /// <summary>
        /// Método AddExtendFlag(). Adiciona uma descrição a uma flag já existente.
        /// </summary>
        /// <param name="flag">String com a flag para busca</param>
        /// <param name="descriptionFlag">String com a descrição</param>        
        /// <remarks>Exemplo de formato de flag extendida: "--add-flags".</remarks>
        public void AddDescriptionFlag(string flag, string descriptionFlag)
        {
            try
            {

                if (ContainsFlag(flag))
                {
                    var tuple = _flags[flag];

                    tuple = (tuple.Item1, descriptionFlag);

                    _flags[flag] = tuple;
                }
                else
                {
                    throw new ArgumentException("Item não encontrado", flag);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }

        /// <summary>
        /// Método Run(). Executa o código implementado no escopo quando o comando for executado.
        /// </summary>              
        /// <remarks>Deve ser implementado em cada classe derivada.</remarks>
        public override void Run()
        {

        }

        /// <summary>
        /// Método Run(). Executa o código implementado no escopo quando o comando for executado.
        /// </summary>
        /// <param name="args">Recebe os argumentos do console</param>        
        /// <remarks>Deve ser implementado em cada classe derivada.</remarks>
        public virtual void Run(ArgsMapper argsMapper)
        {

        }        


    }
}
