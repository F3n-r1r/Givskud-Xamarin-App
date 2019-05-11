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
using GivskudApp.Controllers;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnimalDetailsPage : ContentPage
	{

        public AnimalDetailsPage (AnimalModel SelectedAnimal)
		{

			InitializeComponent();
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            BindingContext = SelectedAnimal;

            if(SelectedAnimal.Content.Count > 0)
            {
                foreach(string Paragraph in SelectedAnimal.Content)
                {
                    InfopageContentWrapper.Children.Add(new Label
                    {
                        Text = Paragraph,
                        StyleClass = new List<string> { "ViewParagraph" }
                    });
                }
            }

        }
    }
}