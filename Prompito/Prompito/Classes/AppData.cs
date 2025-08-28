/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using System.Reflection;


namespace Prompito.Classes
{
    class AppData
    {
        private string _appName = "";
        private string _versionNumber = "";
        private string _description = "";
        private string _profileURL = "";
        private string _repositorieURL = "";
        public string GetAppName { get => _appName; }
        public string GetVersionNumber { get => _versionNumber; }
        public string GetDescription { get => _description; }
        public string GetProfileURL { get => _profileURL; }
        public string GetRepositorieURL { get => _repositorieURL; }

        public AppData(string appName = "Prompito", string versionNumber = "v1.0.0", string description = "", string profileURL = "", string repositorieURL = "")
        {
            try 
            {                

                if (string.IsNullOrWhiteSpace(appName))
                {
                                       
                    _appName = new Program().ToString().Replace(".Program", "") ?? "Prompito";                    
                }
                else
                {
                    _appName = appName;
                }

                _versionNumber = versionNumber ?? throw new ArgumentNullException("O AppData não pode ter valores nulos", nameof(versionNumber));
                _description = description ?? throw new ArgumentNullException("O AppData não pode ter valores nulos", nameof(description));
                _profileURL = profileURL ?? throw new ArgumentNullException("O AppData não pode ter valores nulos", nameof(profileURL));
                _repositorieURL = repositorieURL ?? throw new ArgumentNullException("O AppData não pode ter  valores nulos", nameof(repositorieURL));
                
            }
            catch (Exception exception) 
            {
                Console.WriteLine(" [ ERROR ]\n\t{0}", exception);
            }
            
        }
    }
}
