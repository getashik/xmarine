using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossplatApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewBook : ContentPage
	{
		public NewBook ()
		{
			InitializeComponent ();
		}

        private void Button_SaveBook(object sender, EventArgs e)
        {
            Book book = new Book()
            {
                Name = bookName.Text,
                Author = authorName.Text

            };
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection((App.DB_PATH)))
            {
                conn.CreateTable<Book>();
                var numberofrows=conn.Insert(book);
                if (numberofrows > 0) { 
                    bookName.Text = "";
                  authorName.Text = "";
                    DisplayAlert("Success", "New Book has been Saved", "OK");
                }
                else
                    DisplayAlert("Error", "Error while Saving", "Oh :(");

            }
            
        }

       
    }
}