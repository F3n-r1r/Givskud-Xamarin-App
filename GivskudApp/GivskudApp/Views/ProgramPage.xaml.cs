﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GivskudApp.Controllers;
using GivskudApp.ViewModel;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgramPage : ContentPage
	{

		public ProgramPage ()
		{
            DependencyService.Register<ProgramViewModel>();

            InitializeComponent ();
            ElementsController.RenderScannerIcon(ApplicationLayoutTopLevel, Navigation);

            var vm = DependencyService.Get<ProgramViewModel>();
            ProgramList.ItemsSource = vm.Program;

        }
	}
}