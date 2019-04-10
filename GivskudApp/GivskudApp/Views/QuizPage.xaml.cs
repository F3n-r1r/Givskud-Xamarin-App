﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GivskudApp.Controllers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GivskudApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuizPage : ContentPage
	{
		public QuizPage ()
		{
			InitializeComponent ();
            ElementsController.InitializeAbsoluteContent(ApplicationLayoutContentLevel, ApplicationLayoutTopLevel, false);

        }
	}
}