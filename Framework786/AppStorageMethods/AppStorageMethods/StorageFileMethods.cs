using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;


namespace AppStorageMethods
{
    public class Paramaters
    {
        public static List<String> ListOfAssets = new List<String> { };
        public static List<String> listOfColor = new List<String> { };
        public static List<String> ListOfAllUniqueIds = new List<String> { };
        private static List<String> ListOfUsedUniqueIds = new List<String> { };
        private List<String> EmptyList = new List<String> { };
        public StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
    }
    
    
    public abstract class StorageFileMethods:Paramaters
    {

        private StorageFile InternalFile;
        private delegate Task<StorageFile> DelegateSave(String Text, String FileName305, String Save);

        private async Task<StorageFile> ThisSaveMethod(String Text, String FileName305, String Save)
        {
            InternalFile = await WriteToThis(Text, FileName305, Save);

            return InternalFile;
        }

        public async Task<StorageFile> WriteToThis(string Content, string FileName, string FolderName)
        {

            DelegateSave ThisSave = new DelegateSave(ThisSaveMethod);
            return await ThisSave(Content, FileName, FolderName);
        }



        async public Task<StorageFile> WriteTo(String Contents, String FileName305, String SubFolder305)
        {
            //await EraseContainer(SubFolder305);
            try
            {
                if (String.IsNullOrWhiteSpace(Contents) == false)
                {
                    await localFolder.CreateFolderAsync(SubFolder305, CreationCollisionOption.ReplaceExisting);
                    StorageFolder SubFolder = await localFolder.GetFolderAsync(SubFolder305);

                    StorageFile sampleFile = await SubFolder.CreateFileAsync(FileName305 + ".rtf", CreationCollisionOption.ReplaceExisting);

                    await FileIO.WriteTextAsync(sampleFile, Contents);

                    return sampleFile;

                }
                else
                    return null;
            }
            catch (FileNotFoundException)
            {
                return null;
            }


        }

        public async Task<String> DisplayOutput(String FileName305, String SubFolder305)
        {
            var DisplayTo = "";

            StorageFile sampleFile = await thisStorageFile(SubFolder305);

            try
            {
                DisplayTo = await FileIO.ReadTextAsync(sampleFile);
            }

            catch (FileNotFoundException)
            {
                DisplayTo = String.Empty;
            }

            return DisplayTo;
        }

        private async Task<StorageFile> thisStorageFile(String SubFolder786)
        {
            StorageFile CurrentFile;
            List<StorageFile> everyFile = new List<StorageFile> { };

            StorageFolder SubFolder = await localFolder.GetFolderAsync(SubFolder786);
            everyFile.AddRange(await SubFolder.GetFilesAsync());
            CurrentFile = everyFile.ElementAt(0);

            return CurrentFile;
        }

    }
}
