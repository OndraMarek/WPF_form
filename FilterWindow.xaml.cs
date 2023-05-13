using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.ComponentModel;


namespace BookWpf
{

    public partial class FilterWindow : Window
    {

        private BindingList<BookModel> books;
        private DataGrid dataGridBooks;

        public FilterWindow(BindingList<BookModel> books, DataGrid dataGridBooks)
        {
            InitializeComponent();

            this.books = books;
            this.dataGridBooks = dataGridBooks;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            string filterAuthor = textBoxFilterAuthor.Text;

            List<BookModel> filteredBooks = books.Where(book => book.Author.Contains(filterAuthor)).ToList();
            dataGridBooks.ItemsSource = filteredBooks;

            this.Close();
        }
    }
}
