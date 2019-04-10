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
	public partial class TicketsPage : ContentPage
	{
		public TicketsPage ()
		{
			InitializeComponent ();
            ElementsController.InitializeAbsoluteContent(ApplicationLayoutContentLevel, ApplicationLayoutTopLevel, false);
        }
	}
}