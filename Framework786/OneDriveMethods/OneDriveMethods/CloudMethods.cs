using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Microsoft.Live;

namespace OneDriveMethods
{
    
    
    public abstract class CloudMethods
    {
       private StorageFolder localFolder = ApplicationData.Current.LocalFolder;
       private static LiveConnectClient meClient;
       public const String GenericName = "GenericOneDriveFolder";
       public static String GetFilePath; 
       public String FormatName(String rawName)
       {
           if (String.IsNullOrWhiteSpace(rawName))
               rawName = "NotePad 305 file";
           else
           {
               rawName = rawName.Replace("\\", "_");
               rawName = rawName.Replace("/", "_");
               rawName = rawName.Replace("?", "_");
               rawName = rawName.Replace(":", "_");
               rawName = rawName.Replace("*", "_");
               rawName = rawName.Replace("\"", "_");
               rawName = rawName.Replace(">", "_");
               rawName = rawName.Replace("<", "_");
               rawName = rawName.Replace("|", "_");
               rawName = rawName.Replace(";", "_");
           }

           return rawName;
       }

            public async void LoadProfile(String SkyDriveFolderName = GenericName)
            {
                LiveOperationResult meResult;
                dynamic result;
                bool createLiveFolder = false;

               try
               {
                    LiveAuthClient authClient = new LiveAuthClient();
                    LiveLoginResult authResult = await authClient.LoginAsync(new List<String>() { "wl.skydrive_update" });

                    if (authResult.Status == LiveConnectSessionStatus.Connected)
                    {
                        try
                        {
                            meClient = new LiveConnectClient(authResult.Session);
                            meResult = await meClient.GetAsync("me/skydrive/" + SkyDriveFolderName);
                            result = meResult.Result;
                        }
                        catch (LiveConnectException)
                        {
                            createLiveFolder = true;
                        }
                        if (createLiveFolder == true)
                        {
                            try
                            {
                                var skyDriveFolderData = new Dictionary<String, Object>();
                                skyDriveFolderData.Add("name", SkyDriveFolderName);
                                LiveConnectClient LiveClient = new LiveConnectClient(meClient.Session);
                                LiveOperationResult operationResult = await LiveClient.PostAsync("me/skydrive/", skyDriveFolderData);
                                meResult = await meClient.GetAsync("me/skydrive/");

                            }
                            catch (LiveConnectException)
                            {
                            }
                        }

                    }

               }

               catch (LiveAuthException)
               {
               }

            }


            public async Task<Boolean> UpLoadToLive(String thisPath, StorageFile thisFile)
            {

                try
                {
                    LiveConnectClient uploadClient = new LiveConnectClient(meClient.Session);
                    LiveOperationResult result = await uploadClient.GetAsync("/me/skydrive/files");
                    dynamic files = result.Result;
                    List<object> data = (List<object>)files.data;
                    foreach (dynamic item in data)
                    {
                        
                        if (item.name == thisPath)
                        {

                            try
                            {

                                LiveOperationResult uploadResult = await uploadClient.BackgroundUploadAsync(item.id, thisFile.Name, thisFile, OverwriteOption.Overwrite);

                                return true;
                            }
                            catch (NullReferenceException)
                            {
                                return false;
                            }
                            catch (FileNotFoundException)
                            {
                                return false;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                return false;
                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (LiveConnectException)
                {
                }

                return false;
            }

            public async Task<StorageFile> DownLoadFromLive(string FileFormat = ".rtf")
            {
      
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
                openPicker.FileTypeFilter.Add(FileFormat);
                var ThisFile = await openPicker.PickSingleFileAsync();
                
                GetFilePath = GetParentFolderFromPath(Path.GetDirectoryName(ThisFile.Path));
                
                StorageFile Downloadedfile = ThisFile;

                return Downloadedfile;
            }

            public async Task<Boolean> RenameLiveFolder(String OldName, String NewName)
            {

                try
                {
                    LiveConnectClient uploadClient = new LiveConnectClient(meClient.Session);
                    LiveOperationResult result = await uploadClient.GetAsync("/me/skydrive/files");
        
                    dynamic files = result.Result;
                    List<object> data = (List<object>)files.data;
                    foreach (dynamic item in data)
                    {
                     
                        if (item.name == OldName)
                        {

                            try
                            {

                                LoadProfile(NewName);
                                
                                await uploadClient.CopyAsync(OldName, NewName);
                                await uploadClient.DeleteAsync(OldName);
                                
                                return true;
                            }
                            catch (NullReferenceException)
                            {
                                return false;
                            }
                            catch (FileNotFoundException)
                            {
                                return false;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                return false;
                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (LiveConnectException)
                {
                }

                return false;
            }

        private String GetParentFolderFromPath(String Path)
            {
            String Result =  String.Empty;
            List<Char> TempList = new List<Char>();
           
            foreach (Char Letter in Path)
            {
                TempList.Add(Letter);
            }

            TempList.RemoveRange(0, TempList.LastIndexOf('\\'));
            try
            {
                TempList.Remove('\\');
            }
            catch
            {

            }

            foreach (Char Letter in TempList)
            {
                Result = Result + Letter;
            }


            return Result;

            }
                    
        } 


    }

