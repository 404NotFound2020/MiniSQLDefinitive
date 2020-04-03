using MiniSQL.ConfigurationClasses;
using MiniSQL.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public class ConfigurationParser
    {
        private static ConfigurationParser versionReader;
        private const string VersionFileDirectory = "enviroment";
        private const string VersionFileName = "configuration.inf";
        private const string delimitator = "=";
        private Dictionary<string, ConfigDelimitator> delimitatorsWords;
        private string compositePath;

        private ConfigurationParser() 
        {
            this.delimitatorsWords = new Dictionary<string, ConfigDelimitator>();
            this.SetDelimitators();
            this.compositePath = VersionFileDirectory + "\\" + VersionFileName;
            if (!Directory.Exists(VersionFileDirectory))
            {
                Directory.CreateDirectory(VersionFileDirectory);
                this.CreateConfigurationFile(this.compositePath);
            }
            else if (!File.Exists(this.compositePath)) 
            {
                this.CreateConfigurationFile(this.compositePath);
            }
        }

        public static ConfigurationParser GetConfigurationParser() 
        { 
            if(versionReader == null) 
            {
                versionReader = new ConfigurationParser();
            }
            return versionReader;
        }

        private void CreateConfigurationFile(string compositePath) 
        {
            FileStream fileSteam = File.Open(compositePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(fileSteam);
            IEnumerator<ConfigDelimitator> enumerator = this.delimitatorsWords.Values.GetEnumerator();
            while (enumerator.MoveNext()) 
            {
                writer.WriteLine(enumerator.Current.delimitatorName + delimitator + enumerator.Current.defaultValue);
            }
            writer.Close();
            fileSteam.Close();                            
        }

        public SystemConfiguration GetSystemConfiguration() 
        {
            FileStream fileSteam = File.Open(compositePath, FileMode.Open, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fileSteam);
            SystemConfiguration systemConfiguration = new SystemConfiguration();
            string line;
            string[] lineSplit;
            while (!reader.EndOfStream) 
            {
                line = reader.ReadLine();
                lineSplit = line.Split(delimitator.ToCharArray());
                if (this.delimitatorsWords.ContainsKey(lineSplit[0])) this.delimitatorsWords[lineSplit[0]].setValueToConfig.Invoke(new Tuple<SystemConfiguration, string>(systemConfiguration, lineSplit[1]));                
            }
            reader.Close();
            fileSteam.Close();
            return systemConfiguration;        
        }

        public void SaveSystemConfiguration(SystemConfiguration configuration) 
        { 
        
        }

        private void SetDelimitators() 
        {
            this.delimitatorsWords.Add("PS_Version", new ConfigDelimitator("PS_Version", LastVersionVariables.ParserVersion, (tuple) => tuple.Item1.parserVersion = tuple.Item2));
            this.delimitatorsWords.Add("UB_Version", new ConfigDelimitator("UB_Version", LastVersionVariables.UbicationVersion, (tuple) => tuple.Item1.ubicationVersion = tuple.Item2));
            this.delimitatorsWords.Add("DTF_Version", new ConfigDelimitator("DTF_Version", LastVersionVariables.SaveDataFormatVersion, (tuple) => tuple.Item1.saveDataVersion = tuple.Item2));
            this.delimitatorsWords.Add("INX_Version", new ConfigDelimitator("INX_Version", LastVersionVariables.ActualIndexationVersion, (tuple) => tuple.Item1.indexationVersion = tuple.Item2));


            /**
             * 
             * 

            private const string IsSetActiveDatabaseTimeIntervalDelimitatorWord = "IsSetActiveDBTimeInterval";
            private const string ActiveDatabaseTimeIntervalDelimitatorWord = "ActiveDBTimeInterval";
             * 
             * 
             * */
        }




    }
}
