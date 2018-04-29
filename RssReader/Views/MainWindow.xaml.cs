using RssReader.Models;
using System;
using System.Collections.Generic;
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

namespace RssReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListStories.Height = this.ActualHeight - 110;
        }




        private void ListStories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Story story = (sender as ListView).SelectedItem as Story;
            
            try
            {
                if(IsLoadPage.IsChecked == true)
                {
                    BrowserFeed.Source = story.Uri;
                }
                else
                {
                    BrowserFeed.NavigateToString(story.Description);
                }
                
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }
    }
}
