using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prompto.Prompito.Tools
{
    class WebClientProxy
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientProxy;
        private readonly WebProxy _proxy;
        private readonly HttpClientHandler _httpClientHandler;
        private Regex _urlRegex;
        public WebClientProxy() 
        {
            _httpClient = new HttpClient();
            _urlRegex = new Regex("^(http|https)://(([a-zA-Z0-9]+).)?([a-zA-Z0-9]+).(com|org|net|tk|cc|co|io)(((/[a-zA-Z0-9]+)+))?$");

            _proxy = new WebProxy
            {
                Address = new Uri($"http://191.252.204.220:8080"),
                BypassProxyOnLocal = true,
                UseDefaultCredentials = false,
            };

            _httpClientHandler = new HttpClientHandler 
            {
                Proxy = _proxy
            };

            _httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            _httpClientProxy = new HttpClient(handler: _httpClientHandler, disposeHandler: true);
        }

        private async Task VerifyNetworkConnect(string host)
        {            

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                //string host = "8.8.8.8"; // Script repositorie URL                

                using (Ping pingSend = new Ping())
                {
                    PingReply pingReply = pingSend.Send(host);

                    // Adiciona um manipulador de eventos para lidar com a conclusão
                    pingSend.PingCompleted += (sender, e) =>
                    {
                        if (e.Reply != null && e.Reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine($"Ping para {host} bem-sucedido! Tempo: {e.Reply.RoundtripTime}ms");
                        }
                        else
                        {
                            Console.WriteLine($"Ping para {host} falhou. Status: {e.Reply?.Status}");
                        }
                    };

                    Console.WriteLine($"A iniciar ping para {host}...");
                    // Inicia o ping assíncrono. O controle volta para o chamador.
                    await pingSend.SendPingAsync(host, 1000); // 1000 é o tempo limite em milissegundos
                }

            }
            else
            {
                // Case false, not network available return
                Console.WriteLine("Computador Sem conexão");
                
            }
        }

        public void SimpleUrlRequest(string url) 
        {
            try
            {
                if (_urlRegex.IsMatch(url))
                {
                    var result = _httpClient.GetStringAsync(url).Result;
                    Console.WriteLine(result);
                }
                else 
                {
                    throw new ArgumentException("Formato de URL inválida", nameof(url));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }

        public void ProxyUrlRequest(string url)
        {
            try
            {
                if (_urlRegex.IsMatch(url))
                {
                    var result = _httpClientProxy.GetStringAsync(url).Result;
                    Console.WriteLine(result);
                }
                else
                {
                    throw new ArgumentException("Formato de URL inválida", nameof(url));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }
        
        public void SearchProxies () 
        {
            try
            {
                var url = new Uri($"https://api.proxyscrape.com/v4/free-proxy-list/get?request=display_proxies&proxy_format=protocolipport&format=text");

                //Console.WriteLine("Url: {0}",url.AbsoluteUri);

                var result = _httpClient.GetStringAsync(url).Result;
                Console.WriteLine(result);
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }

        public void SearchProxies(string protocol)
        {
            try
            {
                var protocolRegex = new Regex("^(http|socks4|socks5)$");

                if (protocolRegex.IsMatch(protocol))
                {
                    var url = new Uri($"https://api.proxyscrape.com/v4/free-proxy-list/get?request=display_proxies&protocol={protocol}&proxy_format=protocolipport&format=text&timeout=20000");
                    
                    var result = _httpClient.GetStringAsync(url).Result;
                    //result = Regex.Replace(result, "([a-z]+)://", "");
                    //result = Regex.Replace(result, ":([0-9]+)", "");
                    Console.WriteLine(result);
                    
                    
                }
                else 
                {
                    throw new ArgumentException("Digite um protocolo válido: http, socks4 e socks5", nameof(protocol));
                }               

            }
            catch (Exception exception)
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception.Message);
            }
        }
       
    }
}
