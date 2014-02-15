using GroupedItemsManipulation.Common;
using GroupedItemsManipulation.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace GroupedItemsManipulation
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public GroupedItemsPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(GroupDetailPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemDetailPage), itemId);
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }

    public abstract class GroupedItemsMethods
    {
        public async Task<List<SampleDataItem>> CustomGroupedItems(int TotalNumberOFNotepads, List<string> NeedListAsync, List<string> SortedList, GroupedItemsPage ThisGroupedItemsPage, List<SampleDataItem> ManipulateItems, SampleDataSource ThisSampleDataSource)
        {
            ThisGroupedItemsPage.DefaultViewModel["Groups"] = null;
            var sampleDataGroups2 = await SampleDataSource.GetGroupAsync("Group-1");

            //List<String> SortedList = await SortedListAsync();

            foreach (String _UniqueID in SortedList)
            {
                if (ManipulateItems.All(v => v.UniqueId.Contains(_UniqueID) == false) && ManipulateItems.All(z => z.Title != ""))
                {
                    if (/*SortedList.Count > 0 && */ManipulateItems.Count <= TotalNumberOFNotepads)
                    {
                        try
                        {
                            String GroupItemToAdd = SortedList.ElementAt(1);
                            //List<String> NeedListAsync = await ImageManager.ContentsForLIst(GroupItemToAdd);
                            ManipulateItems.Add(sampleDataGroups2.Items.Where(v => v.UniqueId == GroupItemToAdd).Single());
                            try
                            {
                                sampleDataGroups2.Items.Where(v => v.UniqueId == GroupItemToAdd).Single().Title = NeedListAsync.FirstOrDefault();
                            }
                            catch
                            {

                            }
                                SortedList.RemoveAt(1);
                            //await ImageManager.ImageChange(GroupItemToAdd);
                            //  NotePadMessageMain = "New NotePad +";
                            break;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            break;
                        }
                    }
                    else
                    {
                        //  NotePadMessageMain = "No more NotePads";
                        break;
                    }
                }
                else
                {
                    //NotePadMessageMain = "Use NotePad(s) before + new";
                }
            }
            ThisGroupedItemsPage.DefaultViewModel["Groups"] = ManipulateItems;
            return ManipulateItems;

        }

        public List<String> SortedList(List<string>AllUniqueIds, List<string> _UsedUniqueIds)
        {
           
            int j = 0;
            try
            {
                foreach (String _uniqueId in _UsedUniqueIds)
                {
                    j++;
                    if (AllUniqueIds.Contains(_uniqueId))
                    {
                        AllUniqueIds.Remove(_uniqueId);
                        if (j > _UsedUniqueIds.Count())
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (j > _UsedUniqueIds.Count())
                        {
                            break;
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
            }

            return AllUniqueIds;
        }

        public Boolean ContainerTestForUniqueID(String SubFolder305, StorageFile sampleFile)
        {
            Boolean ContainerBool;
            try
            {

                if (sampleFile != null)
                    ContainerBool = true;
                else
                    ContainerBool = false;
            }
            catch (FileNotFoundException)
            {
                ContainerBool = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                ContainerBool = false;
            }

            return ContainerBool;
        }

        public Boolean GroupItemChange(String GroupItemForIamge, StorageFile ThisStorageFile, List<SampleDataItem> ManipulateItems, List<string> NeedListAsync, List<string> ListOfUsedUniqueIds)
        {
            //GroupedItemsPage GetManipulateItems = new GroupedItemsPage();
           // var ImageSwitch = SampleDataSource.GetGroupAsync("Group-1");

            //var ImageSwitch = SampleDataSource.GetGroupAsync("Group-1");
            Boolean ReturnValue = false;

            if (ManipulateItems.Exists(v => v.UniqueId == GroupItemForIamge))
            {
              //  List<String> NeedListAsync = await ContentsForLIst(GroupItemForIamge);
                if (ContainerTestForUniqueID(GroupItemForIamge, ThisStorageFile) == true)
                {

                    //ImageSwitch.Items.Where(z => z.UniqueId == GroupItemForIamge).ElementAt(0).SetImage(displayColor(GroupItemForIamge));
                    ManipulateItems.Where(v => v.UniqueId == GroupItemForIamge).Single().Title = NeedListAsync.FirstOrDefault();
                    ReturnValue = true;

                }
                else
                {
                    //ManipulateItems.Where(z => z.UniqueId == GroupItemForIamge).ElementAt(0).SetImage(ListOfAssets.ElementAt(RanDomNumber(7)));
                    ManipulateItems.Where(v => v.UniqueId == GroupItemForIamge).Single().Title = "";

                    try
                    {
                        ListOfUsedUniqueIds.RemoveAll(v => v.Equals(GroupItemForIamge));
                        //EraseSettingsContainer("UsedUniqueIds");
                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
            }

            return ReturnValue;

        }

    }
}