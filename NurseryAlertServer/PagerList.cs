using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using NurseryAlertServer.Properties;

namespace NurseryAlertServer
{
    class PagerList
    {
        public class PagerEntry : IEquatable<PagerEntry>
        {
            public String pagerText { get; set; }
            public int index { get; set; }
            public String emergency { get; set; }
            public String outstanding { get; set; }
            public String time { get; set; }
            public String displayTime { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public PagerEntry(String text, int i)
            {
                pagerText = text;
                index = i;
            }

            public bool Equals(PagerEntry other)
            {
                return (other.pagerText == this.pagerText);
            }
        }

        private static PagerList _instance;
        private static readonly object singletonPadlock = new object();

        private Mutex _dataLock;
        private List<PagerEntry> _PagerEntries;
        private SoundPlayer _player;
        private Queue<PagerEntry> displayQueue;
        private Queue<PagerEntry> preDisplayQueue;
        private int tallyState;
        private String displayText;

        public delegate void PagerListUpdate();
        public event PagerListUpdate PagerListUpdated;

        /// <summary>
        /// Private constructor
        /// </summary>
        private PagerList()
        {
            _dataLock = new Mutex();
            _PagerEntries = new List<PagerEntry>();
            tallyState = 0;
            displayQueue = new Queue<PagerEntry>();
            preDisplayQueue = new Queue<PagerEntry>();
            displayText = "";

            LoadSettings();

            Tally.TallyManager.Instance.TallyChanged += new Tally.TallyManager.TallyChange(TallyChangedHandler);
        }

        public void LoadSettings()
        {
            _player = null;
            _player = new SoundPlayer(Settings.Default.NotificationSound);
        }

        /// <summary>
        /// Gets the instance of the Pager List
        /// </summary>
        public static PagerList Instance
        {
            get
            {
                lock (singletonPadlock)
                {
                    return _instance ?? (_instance = new PagerList());
                }
            }
        }

        /// <summary>
        /// Callback indicating a tally change has occurred
        ///  This runs in the serial port thread
        /// </summary>
        /// <param name="e">TallyChangedEventArgs containing the tally state</param>
        private void TallyChangedHandler(Tally.TallyManager.TallyChangedEventArgs e)
        {
            Console.WriteLine("Tally {0}", e.state);

            int lastState = tallyState;
            tallyState = e.state;
            if ((tallyState == 1) && (lastState == 0))
            {
                //Screen is being displayed
                MarkDisplayedItems();
            }
            else if ((tallyState == 0) && (lastState == 1))
            {
                //Screen has stopped displaying
                ClearDisplayedItems();
            }
        }

        public void AddEntry(String entryText, bool emergency)
        {
            if (_dataLock.WaitOne(2000))
            {
                int index = _PagerEntries.Count;
                PagerEntry newItem = new PagerEntry(entryText, index);
                //Don't add the entry if it is already displayed
                if (displayQueue.Contains(newItem))
                {
                    _dataLock.ReleaseMutex();
                    return;
                }
                if (preDisplayQueue.Contains(newItem))
                {
                    _dataLock.ReleaseMutex();
                    return;
                }

                newItem.emergency = emergency ? "Yes" : "";
                newItem.outstanding = "Yes";
                DateTime current = DateTime.Now;
                newItem.time = current.ToString("h:mm:ss tt");
                newItem.displayTime = "--Never--";

                if (tallyState == 1)
                {
                    //Currently displaying, queue the item for display later
                    preDisplayQueue.Enqueue(newItem);
                }
                else
                {
                    displayQueue.Enqueue(newItem);
                    UpdateDisplayText(entryText);
                }
                _PagerEntries.Insert(0, newItem);
                _dataLock.ReleaseMutex();

                try
                {
                    _player.Play();
                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine("Failed playing notifcation sound!");
                }
            }
            else
            {
                Console.WriteLine("AddEntry failed getting mutex!");
            }
            PagerListUpdated();
        }

        public void GetState(ref List<PagerEntry> outList, ref String outString)
        {
            if (_dataLock.WaitOne(2000))
            {
                //Do a shallow copy; should be sufficient for read-only access
                for (int i = 0; i < _PagerEntries.Count; i++)
                {
                    outList.Insert(0,_PagerEntries[i]);
                }
                outString = String.Copy(displayText);
                _dataLock.ReleaseMutex();
            }
            else
            {
                Console.WriteLine("GetState failed getting mutex!");
            }
        }

        public void ClearList()
        {
            if (_dataLock.WaitOne(2000))
            {
                _PagerEntries.Clear();
                displayQueue.Clear();
                preDisplayQueue.Clear();
                displayText = "";

                _dataLock.ReleaseMutex();

                PagerListUpdated();
            }
            else
            {
                Console.WriteLine("ClearList failed getting mutex!");
            }
        }

        public void MarkDisplayedItems()
        {
            if (_dataLock.WaitOne(2000))
            {
                foreach (var qitem in displayQueue.ToList())
                {
                    //Use the reverse index to find the entry
                    int rindex = _PagerEntries.Count - qitem.index - 1;
                    PagerEntry entry = _PagerEntries[rindex];
                    entry.outstanding = "No";
                    DateTime current = DateTime.Now;
                    entry.displayTime = current.ToString("h:mm:ss tt");
                }
                _dataLock.ReleaseMutex();

                PagerListUpdated();
            }
            else
            {
                Console.WriteLine("ClearDisplayedItems failed getting mutex!");
            }
        }

        public void ClearDisplayedItems()
        {
            if (_dataLock.WaitOne(2000))
            {
                foreach (var qitem in displayQueue.ToList())
                {
                    //Use the reverse index to find the entry
                    int rindex = _PagerEntries.Count - qitem.index - 1;
                    PagerEntry entry = _PagerEntries[rindex];
                    entry.outstanding = "No";
                    DateTime current = DateTime.Now;
                    entry.displayTime = current.ToString("h:mm:ss tt");
                    //Maybe could just dequeue directly?
                    displayQueue.Dequeue();
                }
                displayText = "";

                //Move items from predisplay to display
                foreach (var qitem in preDisplayQueue.ToList())
                {
                    PagerEntry item = preDisplayQueue.Dequeue();
                    displayQueue.Enqueue(item);
                    UpdateDisplayText(item.pagerText);
                }
                _dataLock.ReleaseMutex();

                PagerListUpdated();
            }
            else
            {
                Console.WriteLine("ClearDisplayedItems failed getting mutex!");
            }
        }

        /// <summary>
        /// Update the value for screen display
        /// </summary>
        /// <param name="entryText">Entry ID text string to display</param>
        private void UpdateDisplayText(string entryText)
        {
            if (String.IsNullOrEmpty(displayText))
            {
                displayText = entryText;
            }
            else
            {
                displayText += ", ";
                displayText += entryText;
            }
        }
    }
}
