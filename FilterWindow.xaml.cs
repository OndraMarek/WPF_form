using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System;


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
            if (((ComboBoxItem)ComboBoxFilter.SelectedItem).Content.ToString() == "Autor")
            {
                string filterAuthor = textBoxFilter.Text;
                List<BookModel> filteredBooks = books.Where(book => book.Author.Contains(filterAuthor)).ToList();
                dataGridBooks.ItemsSource = filteredBooks;
            }
            if (((ComboBoxItem)ComboBoxFilter.SelectedItem).Content.ToString() == "Vydavatel")
            {
                string filterPublisher = textBoxFilter.Text;
                List<BookModel> filteredBooks = books.Where(book => book.Publisher.Contains(filterPublisher)).ToList();
                dataGridBooks.ItemsSource = filteredBooks;
            }
            if (((ComboBoxItem)ComboBoxFilter.SelectedItem).Content.ToString() == "Název")
            {
                string filterName = textBoxFilter.Text;
                List<BookModel> filteredBooks = books.Where(book => book.Name.Contains(filterName)).ToList();
                dataGridBooks.ItemsSource = filteredBooks;
            }

            this.Close();
        }
    }
}
