using GivskudApp.Models;
using GivskudApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GivskudApp.ViewModel
{
    public class AnimalViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public bool _IsBusy = true;
        public bool IsBusy { get { return _IsBusy; } set { _IsBusy = value; } }
        public bool IsLoaded { get { return IsBusy == false; } }

        AnimalService Service = new AnimalService();
        public List<AnimalModel> Animals { get { return Service.Animal; } private set { Service.Animal = value; } }
        int currentAnimal;
        
        public AnimalViewModel(string areaid = null) {

            Task.Run(() => {

                Service.Fetch(areaid);
                IsBusy = false;
                Refresh();

            });

        }

        // The specific Animal item that is clicked (Used for -> AnimalDetailsPage)
        public AnimalModel SelectedAnimal
        {
            get
            {
                return Animals[currentAnimal];
            }
            set
            {
                int index = Animals.IndexOf(value);
                if (index >= 0)
                {
                    currentAnimal = index;
                }
            }
        }
        void OnPropertyChanged([CallerMemberName] string name = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void Refresh() {
            OnPropertyChanged("Animals");
            OnPropertyChanged("IsBusy");
            OnPropertyChanged("IsLoaded");
        }
    }
}