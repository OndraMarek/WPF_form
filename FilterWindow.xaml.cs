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
            List<BookModel> filteredBooks;
            string selectedFilter = ((ComboBoxItem)ComboBoxFilter.SelectedItem).Content.ToString();
            string textFilter = textBoxFilter.Text;

            switch (selectedFilter) {
                case "Autor":
                    filteredBooks = books.Where(book => book.Author.Contains(textFilter)).ToList();
                    break;
                case "Vydavatel":
                    filteredBooks = books.Where(book => book.Publisher.Contains(textFilter)).ToList();
                    break;
                case "Název":
                    filteredBooks = books.Where(book => book.Name.Contains(textFilter)).ToList();
                    break;
                default:
                    filteredBooks = books.ToList();
                    break;
            }

            dataGridBooks.ItemsSource = filteredBooks;
            this.Close();
        }
    }
}
