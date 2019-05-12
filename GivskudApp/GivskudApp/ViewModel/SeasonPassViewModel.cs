using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Plugin.Connectivity;

using Newtonsoft.Json;

using GivskudApp.Models;
using GivskudApp.Services;

namespace GivskudApp.ViewModel
{
    class SeasonPassViewModel : INotifyPropertyChanged
    {

        public SeasonPassModel Data { get; set; }
        private PassService Service = new PassService();

        public string UserInputValue { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand VMCloseOverlayCommand { get; set; }
        public ICommand RemovePassCommand { get; private set; }
        public ICommand FetchPassCommand { get; private set; }

        public bool _VMIsPassOutdated { get; private set; }
        public bool _VMIsPassOutdatedNotification { get; private set; }

        public bool _VMIsDeviceOffline { get; private set; }
        public bool _VMIsDeviceOfflineNotification { get; private set; }

        public event EventHandler InitializeServiceEvent;
        public event EventHandler IsDeviceOfflineEvent;

        public bool IsBusy { get; private set; }
        public bool IsContentLoaded { get {
                return IsBusy == false && Data != null;
            }
        }
        public bool NoContentAvailable {
            get {
                return IsBusy == false && Data == null;
            }
        }

        public SeasonPassViewModel()
        {

            UserInputValue = string.Empty;
            OnPropertyChanged(nameof(UserInputValue));

            _VMIsDeviceOffline = false;
            _VMIsPassOutdated = false;

            OnPropertyChanged(nameof(_VMIsDeviceOffline));
            OnPropertyChanged(nameof(_VMIsPassOutdated));

            RemovePassCommand = new Command(() =>
            {
                Service.Remove();

                Data = null;
                IsBusy = false;

                Refresh();
            });
            FetchPassCommand = new Command(() =>
            {
                if(!string.IsNullOrWhiteSpace(UserInputValue))
                {
                    IsBusy = true;
                    OnPropertyChanged(nameof(IsBusy));

                    Get(UserInputValue);
                }
            });

            VMCloseOverlayCommand = new Command<string>((string OverlayType) => {
                switch (OverlayType)
                {
                    case "lost-connection-notification":
                        _VMIsDeviceOfflineNotification = false;
                        OnPropertyChanged(nameof(_VMIsDeviceOfflineNotification));
                        break;
                    case "invalid-pass-notification":
                        _VMIsPassOutdatedNotification = false;
                        OnPropertyChanged(nameof(_VMIsPassOutdatedNotification));
                        break;
                }
            });

            InitializeServiceEvent += ((object sender, EventArgs e) =>
            {
                Get(null);
            });
            IsDeviceOfflineEvent += ((object sender, EventArgs e) =>
            {
                _VMIsDeviceOffline = true;
                _VMIsDeviceOfflineNotification = true;
                OnPropertyChanged(nameof(_VMIsDeviceOfflineNotification));
                OnPropertyChanged(nameof(_VMIsDeviceOffline));
            });
            Service.IsPassInvalidEvent += (s, e) =>
            {
                _VMIsPassOutdated = true;
                _VMIsPassOutdatedNotification = true;
                OnPropertyChanged(nameof(_VMIsPassOutdatedNotification));
                OnPropertyChanged(nameof(_VMIsPassOutdated));
            };
        }
        public void InitializeService()
        {
            InitializeServiceEvent?.Invoke(this, EventArgs.Empty);
        }
        public void Get(string idinputvalue)
        {

            VMCloseOverlayCommand.Execute("lost-connection-notification");
            VMCloseOverlayCommand.Execute("invalid-pass-notification");

            if(String.IsNullOrWhiteSpace(idinputvalue))
            {
                Service.Get();
                Data = Service.Data;
            } else
            {
                if(IsDeviceOnline())
                {
                    Service.Get(idinputvalue);
                    Data = Service.Data;
                } else
                {
                    IsDeviceOfflineEvent?.Invoke(this, EventArgs.Empty);
                }
            }
            

            IsBusy = false;

            Refresh();

        }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool IsDeviceOnline()
        {

            return CrossConnectivity.Current.IsConnected;

        }
        private void Refresh()
        {
            OnPropertyChanged(nameof(Data));
            OnPropertyChanged(nameof(IsBusy));
            OnPropertyChanged(nameof(IsContentLoaded));
            OnPropertyChanged(nameof(NoContentAvailable));
        }
    }
}
