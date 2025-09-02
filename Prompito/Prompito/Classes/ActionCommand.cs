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
    /// <summary>
    /// Está classe pai cria uma instância <b>ActionCommand</b> para execução dos comandos. <br/>    
    /// Ela deve ser herdada para criar novos ActionCommand: <code>class MyActionCommand : ActionCommand</code>       
    /// </summary>           
    /// <remarks>
    /// <example>
    /// <code>Exemplo:<br/>
    /// var app = new Executer();
    /// app.AddCommand("init", "Inicia alguma coisa", new MyActionCommand())
    /// </code>
    /// </example>        
    /// </remarks>
    class ActionCommand : AbstractActionCommandBase
    {
        protected Dictionary<string, (string, string)> _flags = new Dictionary<string, (string, string)>();
        protected ReadOnlyDictionary<string, (string, string)> _flagsReadOnly;
        protected bool _DEBUG = false;
        public bool DEBUG { get => _DEBUG; set { _DEBUG = value; } }

        public ReadOnlyDictionary<string, (string, string)> Flags
        {
            get
            {
                return _flagsReadOnly;
            }
            
        } 
        
        public ActionCommand()
        {
            _flagsReadOnly = new(_flags);
        }

        /// <summary>        
        /// Este método valida o formato de flag: <i>"-r"</i>
        /// </summary>
        /// <param name="flag">String com o formato de flag</param>         
        /// <remarks>
        /// <example>Exemplo: <code>
        ///     string flag = "-r";
        ///     if (FlagVerify(flag)) { //code }
        /// </code></example>
        /// <i>Obs: Método private</i>
        /// </remarks>
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

        /// <summary>        
        /// Este método valida o formato de flag: <i>"--rato-roeu"</i>
        /// </summary>
        /// <param name="flag">String com o formato de flag</param>         
        /// <remarks>
        /// <example>Exemplo: <code>
        ///     string flag = "--rato-roeu";
        ///     if (ExtendFlagVerify(flag)) { //code }
        /// </code></example>
        /// <i>Obs: Método private</i>
        /// </remarks>
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

        /// <summary>        
        /// Este método valida os dois formatos de flag: <i>"-r" e "--rato-roeu"</i>
        /// </summary>
        /// <param name="flag">String com o formato de flag</param>         
        /// <remarks>
        /// <example>Exemplo: <code>
        ///     string flag = "-r";
        ///     if (FlagsVerify(flag)) { //code }
        /// </code></example>
        /// <i>Obs: Método private</i>
        /// </remarks>
        private bool FlagsVerify(string flag)
        {
            var flagsRegex = new Regex(@"^(-[a-zA-Z0-9])|(--([a-zA-Z0-9]{2,})(-[a-zA-Z0-9]+)?)$");

            if (flagsRegex.IsMatch(flag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método EqualsFlags(string <paramref name="flagMapper"/>, string <paramref name="flagAdd"/>).<br/>
        /// Testa se a flag mapeada é igual a uma das flags do comando.
        /// </summary>
        /// <param name="flagMapper">Valor da flag mapeada. <i>ArgsMapper.GetArgs["flag1"]</i></param>
        /// <param name="flagAdd">String com a Key das flags adicionadas.</param>  
        /// <remarks>Exemplos de formatos de flags e sua versões extendidas: "-e", "-X", "-8", "--extend-flags", "--extend".</remarks>
        public override bool EqualsFlags(string flagMapper, string flagAdd)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(flagMapper) && !string.IsNullOrWhiteSpace(flagAdd))
                {
                    if (FlagsVerify(flagMapper) && FlagsVerify(flagAdd))
                    {
                        if (FlagVerify(flagMapper))
                        {
                            if (_flags.ContainsKey(flagMapper) || _flags.ContainsKey(flagAdd)) 
                            {
                                if (string.Equals(Flags[flagMapper].Item1, Flags[flagAdd].Item1))
                                {
                                    return true;
                                }                                
                            }
                            else 
                            {
                                throw new ArgumentNullException("Uma ou mais flags de EqualsFlags() não foram adicionadas ao comando.");
                            }                            
                        }
                        else if (ExtendFlagVerify(flagMapper))
                        {
                            if (_flags.TryGetValue(flagAdd, out (string, string) value) && string.Equals(flagMapper, value.Item1))
                            {                                
                                return true;
                            }
                            else
                            {
                                throw new ArgumentNullException("Uma ou mais flags de EqualsFlags() não foram adicionadas ao comando.");
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Um dos parametros de EqualsFlags() não contém um formato de flag. {flagMapper} {flagAdd}");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Nenhum dos parametros de EqualsFlags() podem ser nulo");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
            return false;            
        }

        /// <summary>
        /// Método ContainsFlags(string <paramref name="flag"/>). Verifica a existencia de uma flag ou versão extendida.
        /// </summary>
        /// <param name="flag">String com a flag</param>        
        /// <remarks>Exemplos de formatos de flags e sua versões extendidas: "-e", "-X", "-8", "--extend-flags", "--extend".</remarks>
        public override bool ContainsFlags (string flag) 
        {            
            try
            {                
                if (!string.IsNullOrWhiteSpace(flag))
                {
                    if (FlagsVerify(flag))
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
                        if (ExtendFlagVerify(flag))
                        {
                            foreach (var flagValue in _flags)
                            {
                                if (flagValue.Value.Item1.Contains(flag))
                                {
                                    return true;
                                }                                
                            }
                        }
                    }                                       
                    else
                    {
                        throw new ArgumentException("Formato de flag não reconhecido no parametro de ConstainsFlags(). ", flag);
                    }
                }
                else
                {                    
                    throw new ArgumentNullException(nameof(flag), "O parametro de ConstainsFlags() não pode ser nulo");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);                
            }
            return false;
        }

        /// <summary>
        /// Este método testa a linha de comando mapeada.
        /// </summary>
        /// <param name="argsMapper">Objeto <b>ArgsMapper</b> com a linha de comando mapeada</param>
        /// <param name="sequence">Formato de string com a sequencia de teste</param>
        /// <remarks><example>
        /// Exemplo:
        /// <code>
        ///     if (MappedLineTester(argsMapper, "flag1 arg1"))
        ///     {
        ///         WriteLine("Mensagem: {0}", argsMapper.GetArgs("arg1"));
        ///     }
        /// </code></example></remarks>
        public bool MappedLineTester(ArgsMapper argsMapper, string sequence) 
        {
            // ^(((arg|flag)([1-9]+))(((\s)(arg|flag)([1-9]+))+)?)$
            try
            {
                if (!string.IsNullOrWhiteSpace(sequence) && argsMapper != null) 
                {
                    var sequenceRegex = new Regex(@"^(((arg|flag)([1-9]+))(((\s)+(arg|flag)([1-9]+))+)?)$");
                    var sequenceSplitRegex = new Regex(@"\s+");

                    if (sequenceRegex.IsMatch(sequence)) 
                    {
                        MatchCollection matchs = sequenceRegex.Matches(sequence);                        
                        
                        var sequenceArgs = sequenceSplitRegex.Split(sequence);
                        //var onlyArgs = from arg in args
                        //where !string.IsNullOrWhiteSpace(arg) //&& !int.TryParse(arg, out number)
                        //select arg;

                        if (argsMapper.GetArgsMapper.Keys.SequenceEqual(sequenceArgs)) 
                        { 
                            return true;
                        }                        
                    }
                    else 
                    {
                        throw new ArgumentException("Erro de declaração no parametro 'sequence'", nameof(sequence));
                    }
                }
                else 
                {
                    throw new ArgumentNullException(nameof(sequence), "O parâmetro 'sequence' não pode ser nulo");
                }
            }
            catch (Exception exception) 
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}",exception.Message);
            }
            return false;
            //throw new NotImplementedException("Not implementation");
        }

        /// <summary>
        /// Este método testa a linha de comando e uma flag mapeada.
        /// </summary>
        /// <param name="argsMapper">Objeto <b>ArgsMapper</b> com a linha de comando mapeada</param>
        /// <param name="sequence">Formato de string com a sequencia de teste</param>
        /// <param name="testerFlag">Formato de string com a flag para teste</param>
        /// <remarks><example>
        /// Exemplo:
        /// <code>
        ///     if (MappedLineTester(argsMapper, "flag1 arg1", "flag1=-m"))
        ///     {
        ///         if (!string.IsNotNullOrEmpty(argsMapper.GetArgs("arg1")))
        ///         {
        ///             WriteLine("Mensagem: {0}", argsMapper.GetArgs("arg1"));
        ///         }         
        ///     }
        /// </code></example></remarks>
        public bool MappedLineTester(ArgsMapper argsMapper, string sequence, string testerFlag)
        {
            // ^(flag([1-9]+)=(-([a-zA-Z0-9])|--([a-zA-Z0-9]{2,})(-([a-zA-Z0-9]+))?))$
            try
            {
                if (!string.IsNullOrWhiteSpace(sequence) && !string.IsNullOrWhiteSpace(testerFlag) && argsMapper != null)
                {
                    var sequenceRegex = new Regex(@"^(((arg|flag)([1-9]+))(((\s)+(arg|flag)([1-9]+))+)?)$");
                    var sequenceSplitRegex = new Regex(@"\s+");
                    var testerFlagRegex = new Regex(@"^(flag([1-9]+)=(-([a-zA-Z0-9])))$");                    

                    if (sequenceRegex.IsMatch(sequence) && testerFlagRegex.IsMatch(testerFlag))
                    {
                        MatchCollection matchs = sequenceRegex.Matches(sequence);                        

                        var sequenceArgs = sequenceSplitRegex.Split(sequence);

                        var flag = testerFlag.Split("=");

                        if (argsMapper.GetArgsMapper.Keys.SequenceEqual(sequenceArgs))
                        {
                            if (EqualsFlags(argsMapper.GetArgs(flag[0]), flag[1])) 
                            {
                                return true;
                            }                            
                        }                        
                    }
                    else
                    {
                        throw new ArgumentException("Erro de declaração no parametro 'sequence' ou no parametro 'testeFlags'");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Nenhum dos parametros podem ser nulos");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
            return false;
            //throw new NotImplementedException("Not implementation");
        }

        /// <summary>
        /// Método AddFlag(string <paramref name="flag"/>). Adiciona uma flag ao ActionCommand.
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
                    _flagsReadOnly = new ReadOnlyDictionary<string, (string, string)>(_flags);                    
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
        /// <para>Adiciona uma flag e sua versão extendida ao <b>ActionCommand</b>.</para>
        /// </summary>
        /// <param name="flag">String com a flag</param>
        /// <param name="extendFlag">String com a flag extendida</param>        
        /// <remarks>
        /// <example>Exemplo:
        /// <code>
        /// AddFlag(
        ///     "-r",
        ///     "--repo-hook",
        ///     "Criar hook a partir de repositório de script"
        ///     );
        /// </code>
        /// </example>
        /// </remarks>
        public void AddFlag(string flag, string extendFlag)
        {
            try
            {
                if (FlagVerify(flag) && ExtendFlagVerify(extendFlag))
                {
                    _flags.Add(flag, (extendFlag, ""));
                    _flagsReadOnly = new ReadOnlyDictionary<string, (string, string)>(_flags);
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
        /// <para>Adiciona uma flag, sua versão extendida e uma descrição ao <b>ActionCommand</b>.</para>
        /// </summary>
        /// <param name="flag">String com a flag</param>
        /// <param name="extendFlag">String com a flag extendida</param>
        /// <param name="descriptionFlag">String com a descrição da flag</param>
        /// <remarks>
        /// <example>Exemplo:
        /// <code>
        /// AddFlag(
        ///     "-r",
        ///     "--repo-hook",
        ///     "Criar hook a partir de repositório de script"
        ///     );
        /// </code>
        /// </example>
        /// </remarks>
        public void AddFlag(string flag, string extendFlag, string descriptionFlag)
        {
            try
            {
                if (FlagVerify(flag) && ExtendFlagVerify(extendFlag))
                {
                    _flags.Add(flag, (extendFlag, descriptionFlag));
                    _flagsReadOnly = new ReadOnlyDictionary<string, (string, string)>(_flags);
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

                if (ContainsFlags(flag))
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

                if (ContainsFlags(flag))
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
        /// Este método chama a tela de ajuda do aplicativo retornando as flags do comando.
        /// </summary>               
        /// <remarks>
        /// <i>Obs: Método protected</i>
        /// </remarks>
        protected void Help() 
        {
            Console.WriteLine(" [ AJUDA ]\n");

            foreach (var flag in Flags)
            {
                Console.WriteLine("\t{0,-2} {1,-12} - {2,10}\n", flag.Key, flag.Value.Item1, flag.Value.Item2);
            }
        }

        /// <summary>
        /// Método Run(ArgsMapper <paramref name="argsMapper"/>). Executa o código implementado no escopo quando o comando for executado.
        /// </summary>
        /// <param name="argsMapper">Parametro com a instância dos argumentos mapeados</param>        
        /// <remarks>Deve ser implementado em cada classe derivada.</remarks>
        public override void Run(ArgsMapper argsMapper)
        {

        }        


    }
}
