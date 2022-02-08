using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;

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
            this.PreviewKeyDown += new KeyEventHandler(Delete);
            //this.KeyDown += new KeyEventHandler(Delete);
        }

        public List<String> nameTask = new List<string> { };
        public List<String> descriptionTask = new List<string> { };

        public class task_
        {
            public string id { get; set; }  
            public string name { get; set; }
            public string description { get; set; }
        }

        private void getTasks()
        {
            Connection co = new Connection();
            var tasks = co.getTasks();
            showTasks(tasks);
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            addtask addtask = new addtask();
            this.Close();
            addtask.ShowDialog();
        }

        private void showTasks(Dictionary<int,Connection.Task> tasks)
        {

            List<task_> tasksList = new List<task_>();
            foreach(var task in tasks)
            {
                tasksList.Add(new task_() {id=task.Key.ToString(), name=task.Value.name,description=task.Value.description});
            }
            contentGrid.ItemsSource = tasksList;
        }

        private void resultGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //La ligne où le changement a eu lieu (ne contient pas la modification)
            var data = e.EditingElement.DataContext as task_;
            var id = data.id;
            //La textbox avec le texte modifié
            var editingTextBox = e.EditingElement as TextBox;

            //Nom de la colonne 
            var nameColumn = e.Column.Header;

            var newValue = editingTextBox.Text;

            Connection connection = new Connection();   

            if(nameColumn.ToString() == "Description")
            {
               bool success = connection.editTask(id,new string[]{data.name,newValue });
            }

            if (nameColumn.ToString() == "Nom" && newValue.Length > 1)
            {
                bool success = connection.editTask(id, new string[] { newValue, data.description });
            }

            if (nameColumn.ToString() == "Nom" && newValue.Length == 0)
            {
                MessageBox.Show("Nom de la tâche obligatoire", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
            }

        }


        private void Delete(object sender,KeyEventArgs e)
        {   

            if(e.Key == Key.Delete && contentGrid.CurrentItem != null)
            {
                var item = contentGrid.CurrentItem as task_;
                bool resultDelete = false;
                MessageBoxResult result = MessageBox.Show("Supprimer la tâche ?", "Suppression", MessageBoxButton.YesNo,MessageBoxImage.Warning);

                Connection connection = new Connection();   
  
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        resultDelete = connection.deleteTask(item.id);
                        if(resultDelete)
                        {
                            MessageBox.Show("Tâche supprimée","Succès",MessageBoxButton.OK,MessageBoxImage.Information);
                        }
                        break;
                        case MessageBoxResult.No:
                        e.Handled = true;
                        break;
                }
               
            }
        }

    }
}
