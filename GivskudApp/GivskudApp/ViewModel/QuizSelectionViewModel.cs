using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using GivskudApp.Services;
using GivskudApp.Models;

namespace GivskudApp.ViewModel
{
    class QuizSelectionViewModel : INotifyPropertyChanged
    {
        public bool _IsBusy = true;
        public bool IsBusy { get { return _IsBusy; } set { _IsBusy = value; } }
        public bool IsLoaded { get { return !IsBusy; } set { _IsBusy = !value; } }

        private QuizService Service = new QuizService();
        public List<QuizModel> Data { get { return Service.Data; } }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public QuizSelectionViewModel()
        {
            Task.Run(() =>
            {

                Service.Get();
                IsBusy = false;
                Refresh();

            });
        }

        public void Refresh()
        {
            OnPropertyChanged("Data");
            OnPropertyChanged("IsBusy");
            OnPropertyChanged("IsLoaded");
        }
    }
}
