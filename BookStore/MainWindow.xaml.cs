using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    public partial class MainWindow : Window
    {
        private BookStoreContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new BookStoreContext();
            Load();
        }

        private void Load()
        {
            BooksGrid.ItemsSource = _context.Books.ToList();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            InsertBook(TbName.Text, TbPrice.Text, TbAuthor.Text, TbCategory.Text);
        }

        public void InsertBook(string name, string priceStr, string author, string category)
        {
            if(!decimal.TryParse(priceStr, out decimal price))
            {
                return;
            }

            Book book = new()
            {
                Name = name,
                Price = price,
                Author = author,
                Category = category
            };

            _context.Books.Add(book);
            _context.SaveChanges();
            Load();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateBook(TbName.Text, TbPrice.Text, TbAuthor.Text, TbCategory.Text);
        }

        public void UpdateBook(string name, string priceStr, string author, string category)
        {
            if (BooksGrid.SelectedItem is Book selectedBook)
            {
                if (!decimal.TryParse(priceStr, out decimal price))
                {
                    return;
                }

                selectedBook.Name = name;
                selectedBook.Price = price;
                selectedBook.Category = category;
                selectedBook.Author = author;
                _context.SaveChanges();
                Load();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteBook();
        }

        public void DeleteBook()
        {
            if (BooksGrid.SelectedItem is Book selectedBook)
            {
                _context.Remove(selectedBook);
                _context.SaveChanges();
                Load();
            }
        }

        private void BooksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectBook();
        }

        public void SelectBook()
        {
            if (BooksGrid.SelectedItem is Book selectedBook)
            {
                TbName.Text = selectedBook.Name;
                TbPrice.Text = selectedBook.Price.ToString();
                TbAuthor.Text = selectedBook.Author;
                TbCategory.Text = selectedBook.Category;
            }
        }
    }

    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }

    public class BookStoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BookStoreContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
        }
    }
}
