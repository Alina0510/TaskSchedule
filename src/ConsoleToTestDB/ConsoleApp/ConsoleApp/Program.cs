using System;
using System.Data.SqlClient;

namespace ConsoleApp_DBViewer
{
    class Program
    {
        private const string ConnectionString = @"Data Source=PC\SQLEXPRESS;Initial Catalog=University;Integrated Security=True;Connect Timeout=30;";
        

        static void Main(string[] args)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    GenerateData(connection);

                    PrintTableData(connection, "Users");
                    PrintTableData(connection, "Boards");
                    PrintTableData(connection, "UserBoardRoles");
                    PrintTableData(connection, "Tasks");

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void GenerateData(SqlConnection connection)
        {
            Random random = new Random();

            for (int i = 0; i < 40; i++)
            {
                string username = $"User{random.Next(1000, 9999)}";
                string email = $"{username.ToLower()}@example.com";
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Users (Username, Email) VALUES (@Username, @Email)", connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }

            for (int i = 0; i < 40; i++)
            {
                string boardName = $"Board{random.Next(1000, 9999)}";
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Boards (Name) VALUES (@Name)", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", boardName);
                    cmd.ExecuteNonQuery();
                }
            }

            for (int i = 1; i <= 40; i++)
            {
                int roleId = random.Next(1, 4);
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO UserBoardRoles (UserId, BoardId, RoleId) VALUES (@UserId, @BoardId, @RoleId)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", i);
                    cmd.Parameters.AddWithValue("@BoardId", i);
                    cmd.Parameters.AddWithValue("@RoleId", roleId);
                    cmd.ExecuteNonQuery();
                }
            }

            for (int i = 0; i < 40; i++)
            {
                string taskTitle = $"Task{random.Next(1000, 9999)}";
                string taskDescription = $"Description for {taskTitle}";
                DateTime deadline = DateTime.Now.AddDays(random.Next(1, 365));
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Tasks (Title, Description, Status, Deadline, BoardId) VALUES (@Title, @Description, @Status, @Deadline, @BoardId)", connection))
                {
                    cmd.Parameters.AddWithValue("@Title", taskTitle);
                    cmd.Parameters.AddWithValue("@Description", taskDescription);
                    cmd.Parameters.AddWithValue("@Status", "To Do");
                    cmd.Parameters.AddWithValue("@Deadline", deadline);
                    cmd.Parameters.AddWithValue("@BoardId", random.Next(1, 41));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void PrintTableData(SqlConnection connection, string tableName)
        {
            try
            {
                using (SqlCommand command = new SqlCommand($"SELECT * FROM {tableName}", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($"=== Data from table: {tableName} ===");

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetName(i)}: {reader.GetValue(i)}; ");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read data from {tableName}. Error: {ex.Message}");
            }
        }
    }
}