using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;


namespace AppDataContainerMethods
{
    public class Paramaters
    {
        public static List<String> ListOfAssets = new List<String> { };
        public static List<String> listOfColor = new List<String> { };
        public static List<String> ListOfAllUniqueIds = new List<String> { };
        public static List<String> ListOfUsedUniqueIds = new List<String> { };
        public List<String> EmptyList = new List<String> { };
        public StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
    }

    public abstract class DataContainerMethods: Paramaters
    {

      
        
        public void SaveSetting(String Contents, String SaveType, String Container)
            {
                try
                {

                    if (String.IsNullOrWhiteSpace(Contents) != true)
                    {
                        String SaveTypeCaseInsensitive = SaveType;

                        if (localSettings.Containers.ContainsKey(Container))
                        {
                            localSettings.Containers[Container].Values[SaveTypeCaseInsensitive] = Contents;
                        }
                        else localSettings.CreateContainer(Container, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    }
                }

                catch { }
            }
     
            public String RetrieveSetting(String SaveType, String Container)
            {

                String SaveTypeCaseInsensitive = SaveType;
                String GetSetting;

                try
                {
                    GetSetting = localSettings.Containers[Container].Values[SaveTypeCaseInsensitive].ToString();
                }

                catch
                {
                    GetSetting = String.Empty;
                }

                return GetSetting;
            }
            
            public String EraseSettingsContainer(String Container)
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                if (localSettings.Containers.ContainsKey(Container))
                    localSettings.DeleteContainer(Container);

                return "Settings Container Erased!";
            }

            public async Task<List<String>> RetrieveItemsForList(int TotalNumberOFNotepads = 8)
            {
                try
                {
                    if (ListOfUsedUniqueIds.Count == 0 || ListOfUsedUniqueIds == null)
                    {
                        for (int i = 1; i <= TotalNumberOFNotepads; i++)
                        {
                            String uNiqueId = "Group-1-Item-" + i.ToString();
                        }
                    }
                }
                catch (KeyNotFoundException)
                {

                }

                return ListOfUsedUniqueIds;
            }
        
    }
}
