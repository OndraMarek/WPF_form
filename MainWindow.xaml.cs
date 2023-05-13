using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;


namespace BookWpf
{
    public partial class MainWindow : Window
    {
        
        private BindingList<BookModel> books = new BindingList<BookModel>();

        public MainWindow()
        {
            InitializeComponent();
            dataGridBooks.ItemsSource = books;
            dataGridBooks.IsReadOnly = true;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxId.Text) ||
                string.IsNullOrEmpty(textBoxName.Text) ||
                string.IsNullOrEmpty(textBoxAuthor.Text) ||
                string.IsNullOrEmpty(textBoxPublisher.Text) ||
                string.IsNullOrEmpty(textBoxPages.Text))
            {
                MessageBox.Show("Prosím vyplňte všechny pole", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (books.Any(book => book.Id == textBoxId.Text))
            {
                MessageBox.Show("Kniha s tímto ID již existuje", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BookModel newBook = new BookModel()
            {
                Id = textBoxId.Text,
                Name = textBoxName.Text,
                Author = textBoxAuthor.Text,
                Publisher = textBoxPublisher.Text,
                Pages = int.Parse(textBoxPages.Text),
            };

            books.Add(newBook);
            textBoxId.Clear();
            textBoxName.Clear();
            textBoxAuthor.Clear();
            textBoxPublisher.Clear();
            textBoxPages.Clear();

            return;
        }
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            BookModel selectedBook = (BookModel)dataGridBooks.SelectedItem;

            if (selectedBook != null)
            {
                books.Remove(selectedBook);

                dataGridBooks.ItemsSource = null;
                dataGridBooks.ItemsSource = books;
                return;
            }
            MessageBox.Show("Vyberte záznam, který chcete smazat", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        private void ButtonImport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);

                foreach (string line in lines)
                {
                    string[] fields = line.Split(';');

                    BookModel newBook = new BookModel()
                    {
                        Id = fields[0],
                        Name = fields[1],
                        Author = fields[2],
                        Publisher = fields[3],
                        Pages = int.Parse(fields[4])
                    };
                    books.Add(newBook);
                }

                return;
            }
        }

        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.Title = "Export data";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var row in dataGridBooks.Items)
                    {
                        var book = row as BookModel;
                        if (book != null)
                        {
                            writer.Write(book.Id + ";");
                            writer.Write(book.Name + ";");
                            writer.Write(book.Author + ";");
                            writer.Write(book.Publisher + ";");
                            writer.Write(book.Pages);
                            writer.WriteLine();
                        }
                    }
                }
                return;
            }
        }

        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {

            FilterWindow filterWindow = new FilterWindow(books, dataGridBooks);
            filterWindow.Owner = this;
            filterWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            filterWindow.ShowDialog();
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            dataGridBooks.ItemsSource = books;
        }

        private void TextBoxPages_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
                return;
            }
        } 
    }
}
