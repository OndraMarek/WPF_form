using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.IO;


namespace BookWpf
{
    public partial class MainWindow : Window
    {
        public class Book
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
            public string Publisher { get; set; }
            public int Pages { get; set; }
        }

        private BindingList<Book> books = new BindingList<Book>();
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
            Book newBook = new Book()
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
        }
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)dataGridBooks.SelectedItem;

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

                    Book newBook = new Book()
                    {
                        Id = fields[0],
                        Name = fields[1],
                        Author = fields[2],
                        Publisher = fields[3],
                        Pages = int.Parse(fields[4])
                    };
                    books.Add(newBook);
                }
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
                        var book = row as Book;
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
            }
        }

        private void TextBoxPages_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        } 
    }
}
