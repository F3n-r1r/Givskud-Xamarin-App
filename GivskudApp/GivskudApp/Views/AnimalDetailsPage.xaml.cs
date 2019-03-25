using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Models;
using GivskudApp.Services;
using GivskudApp.ViewModel;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalDetailsPage : ContentPage
	{
		public AnimalDetailsPage ()
		{
			InitializeComponent ();

            AnimalModel Data = new AnimalModel {
                ID = 13541,
                Icon = "icon.png",
                Image = "lionTest.jpg",
                Name = "Løve (Panthera Leo)",
                Content = new List<string> {
                    "First paragraph",
                    "Second paragraph"
                },
                Information = new AnimalInfo {
                    Height = new List<int> {110,125},
                    Length = new List<int> {140,250},
                    Weight = new List<int> {120,250},
                    PDays = 110,
                    Descendants = new List<int> {3,4},
                    Lifetime = new List<int> {12,20},
                    Continent = new List<string> {"Afrika"},
                    Species = "Pattedyr",
                    Status = "Sårbar",
                    Eats = "Kød"
                }
            };
            BindingContext = new AnimalInfopageViewModel(Data);

            // Render paragraphs
            int GridOffset = 3;
            for(int i = 0; i < Data.Content.Count; i++) {
                MainGrid.RowDefinitions.Add(new RowDefinition {
                    Height = GridLength.Auto
                });
                MainGrid.Children.Add(new Label {
                    Text = Data.Content[i],
                    StyleClass = new string[] { "ViewParagraph" }
                }, 0, GridOffset + i);
            }

		}
    }
}