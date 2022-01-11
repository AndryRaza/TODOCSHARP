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
using Microsoft.Data.Sqlite;

namespace TODOCSHARP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getTasks();
        }

        public List<String> nameTask = new List<string> { };
        public List<String> descriptionTask = new List<string> { };

        public class Task{
            private string name;
            private string description; 
            public Task(string n, string d)
            {
                name = n; 
                description = d;
            }
        }

        public Dictionary<int,Task> tasks = new Dictionary<int,Task>(); 

        private void getTasks()
        {
            string pathDB = @"Data Source=C:\Users\widoo\Desktop\sqlitestudio-3.3.3\SQLiteStudio\todo";
            using (var connection = new SqliteConnection(pathDB))
            {
                connection.Open();
                var query =
                    @"
                        SELECT *
                        FROM task
                    ";

                var command = connection.CreateCommand();
                command.CommandText = query;

 

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name= reader.GetString(1);
                        var description = reader.GetString(2);
                        nameTask.Add(name); 
                        descriptionTask.Add(description);
                        tasks.Add(Int32.Parse(reader.GetString(0)),new Task(reader.GetString(1),reader.GetString(2)));
                    }
       
                }
                showTasks();
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            addtask addtask = new addtask();
            this.Close();
            addtask.ShowDialog();
        }

        private void showTasks()
        {
            var rowGroup = contentTable.RowGroups.FirstOrDefault();

            foreach(var task in tasks)
            {
                Console.WriteLine(task.Value);
            }

            if (rowGroup != null)
            {
                int i = 0;
                while(i < nameTask.Count)
                {
                    TableRow row = new TableRow();

                    TableCell cell = new TableCell();

                    cell.Blocks.Add(new Paragraph(new Run(nameTask[i])));
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Blocks.Add(new Paragraph(new Run(descriptionTask[i])));
                    row.Cells.Add(cell);

                    rowGroup.Rows.Add(row);
                    i++;
                }
            }           
        }

        private void editTask(int i)
        {
            string pathDB = @"Data Source=C:\Users\widoo\Desktop\sqlitestudio-3.3.3\SQLiteStudio\todo";
        }
    }
}
