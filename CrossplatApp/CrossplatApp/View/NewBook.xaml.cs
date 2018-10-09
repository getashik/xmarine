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
        string ctxt = "save";
        public NewBook()
        {
            InitializeComponent();
            BookEntryForm.BindingContext = new Book();
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
                // BookEntryForm.BindingContext.GetType().GetProperty("Id").SetValue(BookEntryForm.BindingContext, null);
                var numberofrows = 0;
                if (ctxt == "edit")
                    numberofrows = conn.Update(BookEntryForm.BindingContext);
                else
                    numberofrows = conn.Insert(BookEntryForm.BindingContext);


                if (numberofrows > 0)
                {

                    BookEntryForm.BindingContext = new Book();
                    ctxt = "save";

                    //bookName.Text = "";
                    //authorName.Text = "";
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
            var Button = (Button)sender;
            var bc = Button.BindingContext as Book;
            BookEntryForm.BindingContext = bc;
            ctxt = "edit";

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

        private void Delete_Clicked(object sender, EventArgs e)
        {

        }

        private void Edit_Clicked(object sender, EventArgs e)
        {

        }
    }
}