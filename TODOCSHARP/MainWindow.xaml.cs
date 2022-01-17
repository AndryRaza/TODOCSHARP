using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;

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
            //DataRow dr = DataTable.NewRow();

            foreach(var task in tasks)
            {

            }
            /*
           var rowGroup = contentTable.RowGroups.FirstOrDefault();

           foreach(var task in tasks)
           {
               var t = task.Value;

               TableRow row = new TableRow();

               TableCell cell = new TableCell();

               cell.Blocks.Add(new Paragraph(new Run(t.name)));
               row.Cells.Add(cell);

               cell = new TableCell();
               cell.Blocks.Add(new Paragraph(new Run(t.description)));
               row.Cells.Add(cell);

               rowGroup.Rows.Add(row);

           }
           */
        }
    }
}
