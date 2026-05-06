using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prompito.Prompito.Tools
{
    /// <summary>
    /// Class ProgressBar
    /// </summary>
    /// <remarks>
    /// Cria um objeto de barra de progresso no console.
    /// </remarks>
    public class ProgressBar
    {        
        private int _max = 100;
        private int _progress = 0;
        private int _barLength = 50;
        private float _percent = 0.0f;

        /// <summary>
        /// Esta propriedade retorna a porcentagem de progresso da barra.         
        /// </summary>        
        public float Percent 
        {
            get { return _percent; }
        }

        /// <summary>
        /// Esta propriedade retorna e atribui um valor máximo a barra de progresso.
        /// </summary>
        public int Max
        {
            get { return _max; }
            set
            {
                try
                {
                    _max = value;
                }
                catch (NullReferenceException nullException)
                {
                    Console.WriteLine("Exceção de referência nula: ", nullException.Message);
                    Console.WriteLine("Objeto: ", nullException.Source);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Erro na propriedade Max: ", exception.Message);
                }
            }
        }

        /// <summary>
        /// Esta propriedade retorna e atribui um valor de progresso.
        /// </summary>
        public int Progress
        {  
            get { return _progress; }
            set 
            {
                try
                {
                    if (value <= _max) 
                    {
                        _progress = value;
                    }
                    else 
                    {
                        throw new Exception("Valor fora do intervalo de minimo e máximo");
                    }
                }
                catch (NullReferenceException nullException) 
                {
                    Console.WriteLine("Exceção de referência nula: ", nullException.Message);
                    Console.WriteLine("Objeto: ", nullException.Source);
                }
                catch (Exception exception) 
                {
                    Console.WriteLine("Erro na propriedade Value: ", exception.Message);
                }
            } 
        }

        /// <summary>
        /// Construtor do objeto ProgressBar
        /// </summary>
        /// <param name="max">Parametro opcional. Mantem o valor padrão caso não seja atribuido.</param>
        public ProgressBar(int max = 100) 
        {                        
            _max = max;
        }

        /// <summary>
        /// Desenha a barra de progresso no console.
        /// </summary>
        /// <param name="progress">Valor do progresso atual</param>
        /// <param name="viewPercent">Desabilita a visualização da porcentagem. Habilitado por padrão</param>
        public void DrawProgressBar(int progress, bool viewPercent = true) 
        {
            try 
            {                
                float percent = this.ProgressToPercent(progress);
                int filledLength = (int)(_barLength * percent);
                _progress = progress;

                if (filledLength < _barLength)
                {
                    Console.CursorVisible = false;
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                }
                else 
                {
                    Console.CursorVisible = true;                    
                }

                Console.OutputEncoding = System.Text.Encoding.UTF8;

                string bar = new string('\u2588', filledLength) + new string('\u2591', _barLength - filledLength);

                Console.SetCursorPosition(0, Console.GetCursorPosition().Top);

                if (viewPercent)
                {                   
                    Console.Write($"[ {bar} ] {percent:P0}");                    
                }
                else 
                {                    
                    Console.Write($"[ {bar} ]");                    
                }                    
            }
            catch (Exception exception) 
            {
                Console.WriteLine("DrawProgressBar Error: ", exception.Message);
                Console.CursorVisible = true;
            }
            
        }

        /// <summary>
        /// Retorna em porcentagem o valor do progresso.
        /// </summary>
        /// <param name="progress">Valor do progresso atual.</param>
        /// <returns></returns>
        private float ProgressToPercent (int progress) 
        {
            try 
            {
                float percent = (float)progress / _max;
                _percent = percent;                

                return percent;
            }
            catch (Exception exception)
            {
                Console.WriteLine("ProgressToPercent Erro: ", exception.Message);
                return 0;                
            }
        }
    }
}
