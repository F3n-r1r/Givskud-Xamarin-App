using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

using System.Diagnostics;
using System.Threading.Tasks;

namespace GivskudApp.ViewModel
{
    class MapViewModel : INotifyPropertyChanged
    {

        public ICommand MapControlCommand { get; private set; }

        public List<bool> MapOverlayState { get; private set; }

        private Dictionary<string, MapOverlay> StatePresets { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MapViewModel()
        {
            SetOverlays();

            MapOverlayState = new List<bool> { false, false, false, false, false, false, false, false, false };

            MapControlCommand = new Command<string>((id) => {
                Task.Run(() =>
                {
                    if (StatePresets.ContainsKey(id))
                    {

                        StatePresets[id].IsActive = !StatePresets[id].IsActive;

                        for (int i = 0; i < MapOverlayState.Count; i++)
                        {
                            if (StatePresets[id].IsActive)
                            {
                                if (StatePresets[id].Values[i] == true)
                                {
                                    MapOverlayState[i] = true;
                                }
                            }
                            else
                            {
                                if(MapOverlayState[i] == true)
                                {
                                    MapOverlayState[i] = false;
                                    foreach(KeyValuePair<string, MapOverlay> State in StatePresets)
                                    {
                                        if (State.Key == id || State.Value.IsActive == false)
                                        {
                                            continue;
                                        } else
                                        {
                                            if(State.Value.Values[i] == true)
                                            {
                                                MapOverlayState[i] = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        OnPropertyChanged(nameof(MapOverlayState));
                    };
                });
            });
        }
        private void SetOverlays()
        {
            StatePresets = new Dictionary<string, MapOverlay>();
            Dictionary<string, List<bool>> Presets = new Dictionary<string, List<bool>> {
                { "Animals", new List<bool>          { false,    false,  false,  false,  false,  false,  false,  false,  true   } },
                { "Defibrilator", new List<bool>    { false,    false,  false,  false,  false,  false,  false,  false,  false   } },
                { "Information", new List<bool>     { false,    false,  false,  false,  true,   true,   false,  false,  false   } },
                { "Parking", new List<bool>         { true,     false,  false,  false,  true,   false,  false,  false,  false   } },
                { "Playground", new List<bool>      { false,    true,   false,  false,  false,  false,  true,   false,  false   } },
                { "Restaurant", new List<bool>      { true,     false,  true,   false,  false,  true,   true,   true,   false   } },
                { "Toilet", new List<bool>          { true,     true,   false,  false,  true,   true,   false,  true,   false   } },
                { "Tour", new List<bool>            { false,    false,  false,  true,   false,  false,  false,  false,  false   } }
            };
            foreach(KeyValuePair<string, List<bool>> Preset in Presets)
            {
                StatePresets.Add(Preset.Key, new MapOverlay
                {
                    Key = Preset.Key,
                    IsActive = false,
                    Values = Preset.Value
                });
            }
        }

    }
    class MapOverlay
    {
        public string Key { get; set; }
        public bool IsActive { get; set; }
        public List<bool> Values { get; set; }
    }
}
