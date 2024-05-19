using HtmlFetcherLib; 
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace HtmlViewer
{
    public partial class MainWindow : Window
    {
        private HtmlFetcher _htmlFetcher = new HtmlFetcher();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void FetchHtml_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;
            try
            {
                string html = await _htmlFetcher.GetHtmlAsync(url);
                string title = Regex.Match(html, @"<title>\s*(.+?)\s*</title>", RegexOptions.IgnoreCase).Groups[1].Value;
                string textContent = Regex.Replace(html, "<.*?>", string.Empty);

                HtmlContentWindow contentWindow = new HtmlContentWindow
                {
                    Title = title,
                    HtmlTextBlock = { Text = textContent }
                };

                contentWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
