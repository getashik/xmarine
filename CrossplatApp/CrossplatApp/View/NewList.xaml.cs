using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CrossplatApp.DataSources;

namespace CrossplatApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewList : ContentPage
	{
		public NewList ()
		{
			InitializeComponent ();
            var dataCls = new NewListData();
            CarList.ItemsSource = dataCls.Cars;
           
		}
	}
}