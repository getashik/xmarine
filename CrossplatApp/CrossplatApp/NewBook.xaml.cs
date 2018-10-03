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
        public NewBook()
        {
            InitializeComponent();
            GetBooksData();
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
                var numberofrows = conn.Insert(book);
                if (numberofrows > 0)
                {
                    bookName.Text = "";
                    authorName.Text = "";
                    GetBooksData();
                    //DisplayAlert("Success", "New Book has been Saved", "OK");
                }
                else
                    DisplayAlert("Error", "Error while Saving", "Oh :(");

            }

        }

        public void GetBooksData()
        {

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {

                conn.CreateTable<Book>();
                var books = conn.Table<Book>().ToList();
                booksListView.ItemsSource = books;

            }

        }

        private void Btn_ItemEdit(object sender, EventArgs e)
        {
            DisplayAlert("Success", "Edit Pressed ", "ok");
        }

        private void Btn_ListDelete(object sender, EventArgs e)
        {
            var button = (Image)sender;
            var bc = button.BindingContext as Book;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {

                conn.Delete<Book>(bc.Id);
                DisplayAlert("Success", "Deleted " + bc.Name, "ok");
                GetBooksData();


            }


        }



    }
}