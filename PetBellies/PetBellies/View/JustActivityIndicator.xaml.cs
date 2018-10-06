﻿using PetBellies.BLL.Helper;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JustActivityIndicator : ContentPage
	{
        private string isEmpty = String.Empty;

        public JustActivityIndicator()
        {
            InitializeComponent();
        }

        public JustActivityIndicator(string facebookOrLogin)
        {
            InitializeComponent();

            isEmpty = facebookOrLogin;
        }

        public JustActivityIndicator(bool connection)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!String.IsNullOrEmpty(isEmpty))
            {
                GlobalVariables.InitializeUsersEmailVariable();

                GlobalVariables.InitializeUser();

                GlobalVariables.Mypetlist = new List<GlobalVariables.MyPetsList>();

                GlobalVariables.GetMyPets();

                GlobalVariables.SetMyPetListString();

                Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                GlobalVariables.GetMyPets();

                GlobalVariables.SetMyPetListString();

                Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
        }
    }
}