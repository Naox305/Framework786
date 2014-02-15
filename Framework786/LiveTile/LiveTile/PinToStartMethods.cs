using System;
using Windows.UI.Xaml.Controls;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace LiveTileMethods
{
    public abstract class PinToStartMethods
    {
          public Boolean PinToStartNow(RichEditBox Business)
            {
                
            try
            {
                var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
                var plannedUpdated = tileUpdater.GetScheduledTileNotifications();
                XmlDocument documentNow = new XmlDocument();

                String ContentsForPinToSart;
                Business.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out ContentsForPinToSart);
                if (ContentsForPinToSart.Length > 100)
                    ContentsForPinToSart = ContentsForPinToSart.Remove(101);

                const string xml = @"<tile><visual>
                                        <binding template=""TileSquareText02""><text id=""1"">{0}</text><text id=""2"">{1}</text></binding>
                                        <binding template=""TileWideText01""><text id=""1"">{0}</text><text id=""2"">{1}</text><text id=""3""></text><text id=""4""></text><text id=""5""></text></binding>
                                   </visual></tile>";

                var xmlNow = String.Format(xml, "", ContentsForPinToSart);
                var newUpdate = new TileNotification(documentNow);
                documentNow.LoadXml(xmlNow);
                //documentNow = <title> String Test </title>;
                if (String.IsNullOrWhiteSpace(ContentsForPinToSart))
                {
                    tileUpdater.Clear();
                    
                }
                else
                {
                    tileUpdater.Update(newUpdate);
                   
                }
                
            }
            catch
            {
                return false;
            }

            return true;
            }
        }
    }

