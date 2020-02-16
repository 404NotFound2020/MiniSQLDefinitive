using MiniSQL.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    class VersionReader
    {
        private static VersionReader versionReader;
        private const string VersionFileDirectory = "enviroment";
        private const string VersionFileName = "version.inf";
        private const string ParserVersionDelimitatorWord = "PS_Version";
        private const string UbicationVersionDelimitatorWord = "UB_Version";
        private const string DataFormatVersionDelimitatorWord = "DTF_Version";
        private const string PKActivatedDelimitatorWord = "PK";
        private const string FKActivatedDelimitatorWord = "FK";

        private string compositePath;

        private VersionReader() 
        {
            this.compositePath = VersionFileDirectory + "\\" + VersionFileName;
            if (!Directory.Exists(VersionFileDirectory))
            {
                Directory.CreateDirectory(VersionFileDirectory);
                this.CreateVersionFile(this.compositePath);
            }
            else if (!File.Exists(this.compositePath)) 
            {
                this.CreateVersionFile(this.compositePath);
            }
        }

        public static VersionReader GetVersionReader() 
        { 
            if(versionReader == null) 
            {
                versionReader = new VersionReader();
            }
            return versionReader;
        }

        private void CreateVersionFile(string compositePath) 
        {
            FileStream fileSteam = File.Open(compositePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(fileSteam);
            writer.WriteLine(ParserVersionDelimitatorWord + "=" + LastVersionVariables.ParserVersion);
            writer.WriteLine(UbicationVersionDelimitatorWord + "=" + LastVersionVariables.UbicationVersion);
            writer.WriteLine(DataFormatVersionDelimitatorWord + "=" + LastVersionVariables.SaveDataFormatVersion);
            writer.WriteLine(PKActivatedDelimitatorWord + "=" + LastVersionVariables.PKsActivated);
            writer.WriteLine(FKActivatedDelimitatorWord + "=" + LastVersionVariables.FKsActivated);
            writer.Close();
            fileSteam.Close();                            
        }


        



    }
}
