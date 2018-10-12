using CrossplatApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossplatApp.ViewModel
{
    public class UserRegister
    {

    }

    public class UserLogin : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _Msg;
        public string UserName { get; set; } = "ashik";
        public string UserPassword { get; set; } = "123";
        public string Msg
        {
            get => _Msg;
            set
            {
                if (_Msg != value)
                {
                    _Msg = value;
                    OnPropertyChanged("Msg");
                }
            }
        }
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public ICommand LoginAction
        {
            get
            {

                return new Command(() =>
                {
                    if (UserName == "ashik" && UserPassword == "123")
                        Application.Current.MainPage = new MasterDetail();
                    else
                        Msg = "Login Failed";


                });
            }

        }

    }

}
