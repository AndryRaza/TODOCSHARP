using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace TODOCSHARP
{
    internal class Connection
    {
        private String pathDB = @"Data Source=C:\Users\widoo\Desktop\sqlitestudio-3.3.3\SQLiteStudio\todo";

        public List<String> nameTask = new List<string> { };
        public List<String> descriptionTask = new List<string> { };

        //public SqliteConnection connection = new SqliteConnection();

        public class Task
        {
            public string name;
            public string description;
            public Task(string n, string d)
            {
                name = n;
                description = d;
            }
        }

        public Dictionary<int, Task> tasks = new Dictionary<int, Task>();
        public Dictionary<int, Task> getTasks()
        {
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
                        var name = reader.GetString(1);
                        var description = reader.GetString(2);
                        nameTask.Add(name);
                        descriptionTask.Add(description);
                        tasks.Add(Int32.Parse(reader.GetString(0)), new Task(reader.GetString(1), reader.GetString(2)));
                    }

                }

            }

            return tasks;
        }

        public string[] getTask(string id)
        {
            string[] infos = { };

            using (var connection = new SqliteConnection(pathDB))
            {
                connection.Open();
                var query =
                    @"
                        SELECT *
                        FROM task
                        WHERE id = $id 
                    ";

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(1);
                        var description = reader.GetString(2);
                        infos.Append(name);
                        infos.Append(description);
                    }

                }

                return infos;
            }
        }

        public Boolean addTask(string[] infos)
        {
            Boolean result = false;
            var task = infos[0];
            var description = infos[1];

            if(task != null && description != null)
            {
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
                    command.Parameters.AddWithValue("$name", task);
                    command.Parameters.AddWithValue("$description", description);
                    command.ExecuteNonQuery();

                    result = true;
                }
            }

         
            return result;
        }

        public Boolean editTask(string id, string[] infos)
        {
            Boolean result = false;
            var task = infos[0];
            var description = infos[1];

            if (task != null && description != null && id != null)
            {
                using (var connection = new SqliteConnection(pathDB))
                {
                    connection.Open();
                    var query =
                        @"
                        UPDATE task
                        SET 
                        name = $task,
                        description = $description
                        WHERE 
                        id=$id
                    ";

                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    command.Parameters.AddWithValue("$task", task);
                    command.Parameters.AddWithValue("$description", description);
                    command.Parameters.AddWithValue("$id", id);
                    command.ExecuteNonQuery();

                    result = true;
                }
            }


            return result;
        }

        public Boolean deleteTask(string id)
        {
            Boolean result = false;

            if(id != null)
            {
                using (var connection = new SqliteConnection(pathDB))
                {
                    connection.Open();
                    var query = @"
                            DELETE FROM task
                            WHERE id = $id
                    ";
                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    command.Parameters.AddWithValue("$id", id);
                    command.ExecuteNonQuery();

                    result = true;
                }
            }

            return result;
        }

    }
}