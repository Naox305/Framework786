using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStorageMethods;
using OneDriveMethods;
using AppDataContainerMethods;
using ShareMethods;
using FontsAndStyles;
//using GroupedItemsManipulation;
using LiveTileMethods;
using ColorSettings;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace Framework305
{
    public sealed class CloudMethods305 : CloudMethods
    {
        private static volatile CloudMethods305 Instance;
        private static object SyncRoot = new Object();

        public static CloudMethods305 _Instance
        {
            get
            {
                if (Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (Instance == null)
                            Instance = new CloudMethods305();
                    }
                }

                return Instance;
            }
        }
    }

    public sealed class StorageFileMethods305 : StorageFileMethods
    {
        private static volatile StorageFileMethods305 Instance;
        private static object SyncRoot = new Object();

        public static StorageFileMethods305 _Instance
        {
            get
            {
                if (Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (Instance == null)
                            Instance = new StorageFileMethods305();
                    }
                }

                return Instance;
            }
        }
    }

    public sealed class DataContainerMethods305 : DataContainerMethods
    {
        private static volatile DataContainerMethods305 Instance;
        private static object SyncRoot = new Object();

        public static DataContainerMethods305 _Instance
        {
            get
            {
                if (Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (Instance == null)
                            Instance = new DataContainerMethods305();
                    }
                }

                return Instance;
            }
        }
    }

    public sealed class ShareMethods305 : ShareSourceAndTarget
    {
        private static volatile ShareMethods305 Instance;
        private static object SyncRoot = new Object();

        public static ShareMethods305 _Instance
        {
            get
            {
                if (Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (Instance == null)
                            Instance = new ShareMethods305();
                    }
                }

                return Instance;
            }
        }
    }

    public sealed class FontsOrStyles305 : FontsOrStyles
    {
        private static volatile FontsOrStyles305 Instance;
        private static object SyncRoot = new Object();

        public static FontsOrStyles305 _Instance
        {
            get
            {
                if (Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (Instance == null)
                            Instance = new FontsOrStyles305();
                    }
                }

                return Instance;
            }
        }
    }

    public sealed class Color305 : Color
    {
        private static volatile Color305 Instance;
        private static object SyncRoot = new Object();

        public static Color305 _Instance
        {
            get
            {
                if (Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (Instance == null)
                            Instance = new Color305();
                    }
                }

                return Instance;
            }
        }
    }

    public sealed class PinToStart305 : PinToStartMethods
    {
        private static volatile PinToStart305 Instance;
        private static object SyncRoot = new Object();

        public static PinToStart305 _Instance
        {
            get
            {
                if (Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (Instance == null)
                            Instance = new PinToStart305();
                    }
                }

                return Instance;
            }
        }
    }

}
