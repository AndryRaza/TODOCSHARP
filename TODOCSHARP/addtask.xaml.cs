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
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace TODOCSHARP
{
    /// <summary>
    /// Logique d'interaction pour addtask.xaml
    /// </summary>
    public partial class addtask : Window
    {
        public addtask()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(keyEnter);
        }

        private void keyEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btn_add_Click(sender, e);
            }
            
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Connection co = new Connection();
            string[] infos = {task.Text, description.Text };

            Boolean result = false;

            if(task.Text.Length > 1)
            {
                result = co.addTask(infos);
            }
            else
            {
                MessageBox.Show("Le nom de la tâche est obligatoire","Attention",MessageBoxButton.OK,MessageBoxImage.Error);
            }
    
            if (result)
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }
    }
}
