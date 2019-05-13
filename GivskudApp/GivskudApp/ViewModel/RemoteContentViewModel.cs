using System;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Newtonsoft.Json;

using Plugin.Connectivity;

using Xamarin.Forms;

using GivskudApp.Models;
using GivskudApp.Services;
using GivskudApp.Resources;

namespace GivskudApp.ViewModel
{
    class GuideAnimalViewModel : RemoteContentViewModel
    {

        public AnimalModel AnimalData { get; private set; }
        public QuizModel QuizData { get; private set; }

        public bool HasQuiz { get; private set; }

        private int QuizInstanceId { get; set; }

        public GuideAnimalViewModel(string Local, string Virtual, AnimalModel PassedData, bool ShowNotifications = true, int QuizId = -1) : base(Local, Virtual, ShowNotifications)
        {
            AnimalData = PassedData;
            QuizInstanceId = QuizId;
            QuizData = null;
        }
        public override void Get(string Local, string Virtual)
        {
            if(QuizInstanceId != -1)
            {
                string DataResponse = GetFromContentService(Local, Virtual);
                
                try
                {

                    List<QuizModel> QuizBuffer = JsonConvert.DeserializeObject<List<QuizModel>>(DataResponse);

                    if(QuizBuffer.Count > 0)
                    {
                        foreach(QuizModel Quiz in QuizBuffer)
                        {
                            if(Quiz.ID == QuizInstanceId)
                            {
                                QuizData = Quiz;
                                break;
                            }
                        }
                    } else
                    {
                        QuizData = null;
                    }

                } catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    QuizData = null;
                }

            } else
            {
                QuizData = null;
            }

            HasQuiz = QuizData != null;

            OnPropertyChanged(nameof(HasQuiz));
            OnPropertyChanged(nameof(QuizData));

        }
    }
    class AnimalViewModel : RemoteContentViewModel
    {
        
        public List<AnimalModel> Data { get; private set; }

        private string SortParameter { get; set; }

        public AnimalViewModel(string Local, string Virtual, bool ShowNotifications = true, string _SortParameter = null) : base(Local, Virtual, ShowNotifications)
        {
            SortParameter = _SortParameter;
        }
        public override void Get(string Local, string Virtual)
        {
            
            string DataResponse = GetFromContentService(Local, Virtual);

            try
            {

                List<AnimalModel> AnimalBuffer = JsonConvert.DeserializeObject<List<AnimalModel>>(DataResponse);

                if(AnimalBuffer.Count > 0)
                {
                    if(SortParameter != null)
                    {

                        Data = new List<AnimalModel>();
                        foreach(AnimalModel Animal in AnimalBuffer)
                        {
                            if(Int32.TryParse(SortParameter, out int SortParameterParsed))
                            {
                                if(SortParameterParsed == Animal.AreaID)
                                {
                                    Data.Add(Animal);
                                } else
                                {
                                    continue;
                                }
                            } else
                            {
                                continue;
                            }
                        }

                    } else
                    {
                        Data = AnimalBuffer;
                    }
                } else
                {
                    Data = null;
                }

                /*
                if (!IsDeviceOnline() && Data.Count > 0 && SortParameter != null)
                {

                    if (Int32.TryParse(SortParameter, out int SortParameterConverted)) {

                        List<AnimalModel> Temp = new List<AnimalModel>();

                        foreach (AnimalModel Animal in Data)
                        {
                            if (Animal.AreaID == SortParameterConverted)
                            {
                                Temp.Add(Animal);
                            }
                        }

                        Data = Temp;

                    };

                }
                */

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Data = new List<AnimalModel>();
            }

            OnPropertyChanged(nameof(Data));

        }

    }
    class NewsViewModel : RemoteContentViewModel
    {

        public List<NewsModel> Data { get; private set; }

        public NewsViewModel(string Local, string Virtual, bool ShowNotifications = true) : base(Local, Virtual, ShowNotifications)
        {
        }
        public override void Get(string Local, string Virtual)
        {

            string DataResponse = GetFromContentService(Local, Virtual);

            try
            {
                Data = JsonConvert.DeserializeObject<List<NewsModel>>(DataResponse);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Data = new List<NewsModel>();
            }

            OnPropertyChanged(nameof(Data));

        }

    }
    class QuizViewModel : RemoteContentViewModel
    {

        public List<QuizModel> Data { get; private set; }
        private string SortParameter { get; set; }

        public QuizViewModel(string Local, string Virtual, bool ShowNotifications = true, string _SortParameter = null) : base(Local, Virtual, ShowNotifications)
        {
            SortParameter = _SortParameter;
        }
        public override void Get(string Local, string Virtual)
        {

            string DataResponse = GetFromContentService(Local, Virtual);

            try
            {
                List<QuizModel> QuizBuffer = JsonConvert.DeserializeObject<List<QuizModel>>(DataResponse);

                if(QuizBuffer.Count > 0)
                {
                    if(SortParameter != null)
                    {
                        if(Int32.TryParse(SortParameter, out int SortParameterConverted))
                        {
                            Data = new List<QuizModel>();
                            foreach(QuizModel Quiz in QuizBuffer)
                            {
                                if(Quiz.ID == SortParameterConverted)
                                {
                                    Data.Add(Quiz);
                                } else
                                {
                                    continue;
                                }
                            }
                        } else
                        {
                            Data = QuizBuffer;
                        }
                    } else
                    {
                        Data = QuizBuffer;
                    }
                } else
                {
                    Data = null;
                }
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Data = new List<QuizModel>();
            }

            OnPropertyChanged(nameof(Data));

        }

    }
    class ProgramViewModel : RemoteContentViewModel
    {

        public List<ProgramModel> Data { get; private set; }

        public PViewModel(string Local, string Virtual, bool ShowNotifications = true) : base(Local, Virtual, ShowNotifications)
        {
        }
        public override void Get(string Local, string Virtual)
        {

            string DataResponse = GetFromContentService(Local, Virtual);

            try
            {
                Data = JsonConvert.DeserializeObject<List<ProgramModel>>(DataResponse);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Data = new List<ProgramModel>();
            }

            OnPropertyChanged(nameof(Data));

        }

    }
    public abstract class RemoteContentViewModel : INotifyPropertyChanged
    {

        public bool IsBusy { get; set; }
        public bool IsContentLoaded { get; set; }

        public ICommand VMCloseOverlayCommand { get; set; }

        public bool _VMIsContentOutdated { get; private set; }
        public bool _VMIsDeviceOffline { get; private set; }

        public bool _VMIsContentOutdatedNotification { get; private set; }
        public bool _VMIsDeviceOfflineNotification { get; private set; }

        public event EventHandler IsDeviceOfflineEvent;
        public event EventHandler IsContentOutdatedEvent;

        public event EventHandler InitializeServiceEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public RemoteContentViewModel(string Local, string Virtual, bool AllowNotifications)
        {
            InitializeServiceEvent += ((object sender, EventArgs e) =>
            {

                _VMIsDeviceOffline = false;
                _VMIsContentOutdated = false;

                OnPropertyChanged(nameof(_VMIsDeviceOffline));
                OnPropertyChanged(nameof(_VMIsContentOutdated));

                Task.Run(() =>
                {

                    IsBusy = true;
                    IsContentLoaded = false;

                    OnPropertyChanged(nameof(IsBusy));
                    OnPropertyChanged(nameof(IsContentLoaded));

                    Get(Local, Virtual);

                });

            });

            IsDeviceOfflineEvent += ((object sender, EventArgs e) =>
            {
                _VMIsDeviceOffline = true;
                _VMIsDeviceOfflineNotification = true;
                OnPropertyChanged(nameof(_VMIsDeviceOfflineNotification));
                OnPropertyChanged(nameof(_VMIsDeviceOffline));
            });
            IsContentOutdatedEvent += ((object sender, EventArgs e) =>
            {
                if (AllowNotifications)
                {
                    _VMIsContentOutdated = true;
                    _VMIsContentOutdatedNotification = true;
                    OnPropertyChanged(nameof(_VMIsContentOutdatedNotification));
                    OnPropertyChanged(nameof(_VMIsContentOutdated));
                }
            });

            VMCloseOverlayCommand = new Command<string>((string OverlayType) => {
                switch(OverlayType)
                {
                    case "lost-connection-notification":
                        _VMIsDeviceOfflineNotification = false;
                        OnPropertyChanged(nameof(_VMIsDeviceOfflineNotification));
                        break;
                    case "cached-content-notification":
                        _VMIsContentOutdatedNotification = false;
                        OnPropertyChanged(nameof(_VMIsContentOutdatedNotification));
                        break;
                }
            });

        }

        public bool VerifyResponse(bool IsOnline, bool UsesCache)
        {
            if(IsOnline == false)
            {
                if(UsesCache == true)
                {

                    IsContentOutdatedEvent?.Invoke(this, EventArgs.Empty);

                    IsBusy = false;
                    IsContentLoaded = true;

                    OnPropertyChanged(nameof(IsBusy));
                    OnPropertyChanged(nameof(IsContentLoaded));

                    return true;
                } else
                {
                    IsDeviceOfflineEvent?.Invoke(this, EventArgs.Empty);
                    return false;
                }
            } else
            {
                IsBusy = false;
                IsContentLoaded = true;

                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(IsContentLoaded));

                return true;
            }
        }

        public abstract void Get(string Local, string Virtual);

        public string GetFromContentService(string Local, string Virtual, string ApiAttribute = null, Dictionary<string,string> ApiHeaders = null)
        {

            ContentService Service = new ContentService(Local, Virtual);

            if (ApiAttribute != null)
            {
                Service.SetApiResourceAttribute(ApiAttribute);
            }
            if(ApiHeaders != null)
            {
                Service.SetApiResourceHeaders(ApiHeaders);
            }

            ServiceResponse Response = Service.Get();

            if (VerifyResponse(Service.IsDeviceOnline(), Response.UsesCache))
            {
                System.Diagnostics.Debug.WriteLine("Some content will be used");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No content will be used");
            }

            System.Diagnostics.Debug.WriteLine(Response.Data);

            return Response.Data;

        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void InitializeService()
        {
            InitializeServiceEvent?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDeviceOnline()
        {

            return CrossConnectivity.Current.IsConnected;

        }

    }
}
