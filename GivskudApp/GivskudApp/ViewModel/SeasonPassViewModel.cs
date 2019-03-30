using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using System.Diagnostics;

using Newtonsoft.Json;

using Plugin.Settings;
using Xamarin.Forms;
using GivskudApp.Models;
using GivskudApp.Controllers;
using GivskudApp.ResourceControllers;
using GivskudApp.CryptographyService;

namespace GivskudApp.ViewModel
{
    class SeasonPassViewModel : INotifyPropertyChanged {

        private string _SeasonPassDataKey = "applicationSeasonPassObject";
        public bool _IsBusy = false;
        public bool _HasValidPass = false;
        public bool _IsWorking = false;
        public string _SeasonPassInput = "";
        public SeasonPassModel _ValidPassData = null;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand RemovePassCommand { get; private set; }
        public ICommand FetchPassCommand { get; private set; }

        public SeasonPassViewModel() {

            LoadSavedPass();

            RemovePassCommand = new Command(RemovePassAction);
            FetchPassCommand = new Command(FetchPassAction);
        }

        public string SeasonPassInput {
            get { return _SeasonPassInput; }
            set {
                _SeasonPassInput = value;
                OnPropertyChanged();
            }
        }
        public bool IsBusy {
            get { return _IsBusy; }
            set { _IsBusy = value; }
        }
        public bool HasValidPass {
            get { return _HasValidPass; }
            set { _HasValidPass = value; }
        }
        public bool IsWorking {
            get { return _IsWorking; }
            set { _IsWorking = value; }
        }
        public SeasonPassModel ValidPassData {
            get { return _ValidPassData; }
            set { _ValidPassData = value;  }
        }
        private void FetchPassAction() {

            bool Valid = false;
            
            if(SeasonPassInput == "") {
                return;
            }

            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));

            /* FETCH PASS AND VALIDATE */
            List<RequestHeader> RequestHeaders = new List<RequestHeader>();
            RequestHeaders.Add(new RequestHeader() {
                Key = "PassID",
                Value = EncDecService.Hash(SeasonPassInput)
            });

            ApiResourceController ApiResource = new ApiResourceController("/seasonpass/get", RequestHeaders);
            string ApiResourceResult = ApiResource.Get().Result;
            List<SeasonPassModel> ResultParsed = JsonConvert.DeserializeObject<List<SeasonPassModel>>(ApiResourceResult);

            if(ResultParsed.Count > 0) {
                SeasonPassModel LoadedPass = ResultParsed[0];
                if(LoadedPass.IsValid()) {
                    CrossSettings.Current.AddOrUpdateValue(_SeasonPassDataKey, JsonConvert.SerializeObject(LoadedPass));
                    LoadedPass.Holder = EncDecService.Decrypt(LoadedPass.Holder);
                    ValidPassData = LoadedPass;
                    OnPropertyChanged(nameof(ValidPassData));
                    Valid = true;
                } else {
                    Valid = false;
                }
            } else {
                Valid = false;
            }

            /* FETCH PASS AND VALIDATE */

            HasValidPass = Valid;
            OnPropertyChanged(nameof(HasValidPass));

            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));

            return;

        }
        private void LoadSavedPass() {

            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));

            bool Valid = false;

            string MemoryData = CrossSettings.Current.GetValueOrDefault(_SeasonPassDataKey, null);

            if(MemoryData == null) {
                Valid = false;
            } else {
                
                SeasonPassModel Deserialized = JsonConvert.DeserializeObject<SeasonPassModel>(MemoryData);
                Debug.WriteLine(Deserialized);

                if(Deserialized.IsValid()) {
                    Deserialized.Holder = EncDecService.Decrypt(Deserialized.Holder);
                    ValidPassData = Deserialized;
                    OnPropertyChanged(nameof(ValidPassData));
                    Valid = true;
                } else {
                    Valid = false;
                }
            }

            HasValidPass = Valid;
            OnPropertyChanged(nameof(HasValidPass));

            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));

        }
        private void RemovePassAction() {

            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));

            CrossSettings.Current.Remove(_SeasonPassDataKey);

            HasValidPass = false;
            OnPropertyChanged(nameof(HasValidPass));

            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));

        }
    }
}
