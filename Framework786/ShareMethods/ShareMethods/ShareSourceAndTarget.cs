using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;


namespace ShareMethods
{
    public class Paramaters
    {
        
        //public List<String> EmptyList = new List<String> { };
       // public StorageFolder localFolder = ApplicationData.Current.LocalFolder;
       // public Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public Windows.ApplicationModel.DataTransfer.ShareTarget.ShareOperation _shareOperation;
    }
    public abstract class ShareSourceAndTarget:Paramaters
    {
        private async Task<String> FormatOfSharedData(DataPackageView shardedData)
        {
            String newString = "";


            if (shardedData.Contains(StandardDataFormats.Rtf))
            { newString = this._shareOperation.Data.GetRtfAsync().ToString(); }
            else

                if (shardedData.Contains(StandardDataFormats.Text))
                { newString = await this._shareOperation.Data.GetTextAsync(); }
                else

                    if (shardedData.Contains(StandardDataFormats.Html))
                    { newString = this._shareOperation.Data.GetHtmlFormatAsync().ToString(); }
                    else
                        newString = "Could not share content.";

            return newString;
        }


        public void shareToDocument(RichEditBox NewRTF, TextBox NewText, TextBox ShareBox, int TotalNumberOFNotepads=8)
        {
            String thisContent;
            String GroupItemForTitle = "Group-1-Item-" + TotalNumberOFNotepads;
            //var fileName = "thisFileName.skyDriveFileName(NewText.Text)";

            try
            {
                NewRTF.Document.SetText(Windows.UI.Text.TextSetOptions.None, ShareBox.Text);
                NewRTF.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out thisContent);

            }
            catch (Exception)
            {
                thisContent = "Wrong Format!";
            }

            //await this.shareTO.WriteTo(thisContent, fileName, GroupItemForTitle);
            //titleChange.Items.Where(v => v.UniqueId == GroupItemForTitle).Single().Title = fileName;
        }
    }
}
