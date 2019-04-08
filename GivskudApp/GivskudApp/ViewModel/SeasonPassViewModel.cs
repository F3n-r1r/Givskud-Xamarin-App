using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using GivskudApp.Models;
using GivskudApp.Services;

namespace GivskudApp.ViewModel
{
    class SeasonPassViewModel : INotifyPropertyChanged {

        SeasonPassService Service = new SeasonPassService();

        public string _UserInputValue = "";
        public string UserInputValue { get { return _UserInputValue; } set { _UserInputValue = value; } }

        public bool _IsBusy = true;
        public bool IsBusy { get { return _IsBusy; } set { _IsBusy = value; } }

        public SeasonPassModel Pass { get { return Service.SeasonPass; } }
        public bool ShowPassDetails { get { return Pass != null && IsBusy == false; } }
        public bool ShowPassInput { get { return Pass == null && IsBusy == false; } }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RemovePassCommand { get; private set; }
        public ICommand FetchPassCommand { get; private set; }

        public SeasonPassViewModel() {

            Task.Run(() => {
                Service.Get();
                IsBusy = false;
                Refresh();
            });

            RemovePassCommand = new Command(() => {
                Task.Run(() => {
                    Service.Remove();
                    Refresh();    
                });
            });
            FetchPassCommand = new Command(() => {
                Task.Run(() => {

                    IsBusy = true;
                    Refresh();

                    Service.Get(UserInputValue);

                    IsBusy = false;
                    Refresh();

                });
            });
        }
        void OnPropertyChanged([CallerMemberName] string name = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void Refresh() {
            OnPropertyChanged("Pass");
            OnPropertyChanged("IsBusy");
            OnPropertyChanged("ShowPassDetails");
            OnPropertyChanged("ShowPassInput");
        }
    }
}
