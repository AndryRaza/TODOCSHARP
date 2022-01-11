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
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            string pathDB = @"Data Source=C:\Users\widoo\Desktop\sqlitestudio-3.3.3\SQLiteStudio\todo";
            using (var connection = new SqliteConnection(pathDB))
            {
                connection.Open();
                var query =
                    @"
                        INSERT INTO task
                        (name,description,status)
                        VALUES ($name,$description,true)
                    ";

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("$name", task.Text);
                command.Parameters.AddWithValue("$description", description.Text);
                command.ExecuteNonQuery();

                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }
    }
}
