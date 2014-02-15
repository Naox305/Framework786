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

                    //Problem====================================================================================================================================

                    StorageFile sampleFile = await SubFolder.CreateFileAsync(FileName305 + ".rtf", CreationCollisionOption.ReplaceExisting);

                    //Problem====================================================================================================================================

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

        //async abstract public Task<String> EraseContainer(String SubFolder305)
        //{
        //    try
        //    {

        //        StorageFile FileToErase = await thisStorageFile(SubFolder305);
        //        await FileToErase.DeleteAsync(StorageDeleteOption.Default);
        //        //EraseSettingsContainer(SubFolder305);

        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //    }
        //    catch (FileNotFoundException)
        //    {
        //    }


        //    return "Container " + SubFolder305 + " has been erased!";
        //}

        private async Task<StorageFile> thisStorageFile(String SubFolder786)
        {
            StorageFile CurrentFile;
            List<StorageFile> everyFile = new List<StorageFile> { };

            StorageFolder SubFolder = await localFolder.GetFolderAsync(SubFolder786);
            everyFile.AddRange(await SubFolder.GetFilesAsync());
            CurrentFile = everyFile.ElementAt(0);

            return CurrentFile;
        }

        //public abstract async Task<List<String>> ContentsForLIst(String SubFolder305)
        //{
        //    Windows.UI.Xaml.Controls.RichEditBox rtfConverterBox = new RichEditBox();
        //    String rtfFormatedText;
        //    String ContentsOfCurrentFile;
        //    List<String> CurrentList = new List<String> { };

        //    try
        //    {

        //        StorageFile CurrentFile = await thisStorageFile(SubFolder305);
        //        rtfFormatedText = await FileIO.ReadTextAsync(CurrentFile);
        //        rtfConverterBox.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, rtfFormatedText);
        //        rtfConverterBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out ContentsOfCurrentFile);

        //    }
        //    catch (FileNotFoundException)
        //    {
        //        CurrentList.Clear();
        //        return CurrentList;
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //        CurrentList.Clear();
        //        return CurrentList;

        //    }

        //    CurrentList.Clear();

        //    if (ContentsOfCurrentFile.Length > 200)
        //        CurrentList.Add(ContentsOfCurrentFile.Remove(200) + "...");
        //    else
        //        CurrentList.Add(ContentsOfCurrentFile);

        //    return CurrentList;
        //}

        //public abstract async Task<String> EraseAll(int TotalNumberOFNotepads = 8)
        //{
        //    for (int i = 0; i <= TotalNumberOFNotepads; i++)
        //    {
        //        String SubFolder305 = "Group-1-Item-" + i.ToString();
        //        try
        //        {
        //            StorageFolder SubFolder = await localFolder.GetFolderAsync(SubFolder305);
        //            await SubFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
        //            await ImageChange(SubFolder305);
        //            EraseSettingsContainer(SubFolder305);
        //        }
        //        catch { }
        //    }
        //    EraseSettingsContainer("UsedUniqueIds");


        //    return "All subfolders and files have been erased!";
        //}
    }
}
