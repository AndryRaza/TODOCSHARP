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
    /// Logique d'interaction pour editTask.xaml
    /// </summary>
    public partial class editTask : Window
    {
        public string[] infos
        {
            get;
            set;
        }

        public string id
        { get; set; }   
        public editTask()
        {
            InitializeComponent();
        }

        public editTask(string id_) : this()
        {
            Connection co = new Connection();
            infos = co.getTask(id_);
            id = id_;
            task.Text = infos[0];
            description.Text = infos[1];    
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            Connection co = new Connection();
            Boolean result = false;
            result = co.editTask(id, infos);

            if (result)
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }
    }
}