using System.Data.SQLite;
using System.Data;
// See https://aka.ms/new-console-template for more information
namespace MyNamespace
{
    class Program
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;
        static public bool Connect(string fileName)
        {
            try
            {
                connection = new SQLiteConnection("Data Source=" + fileName + ";Version=3; FailIfMissing=False");
                connection.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
                return false;
            }
        }
        static void Main(string[] args)
        {
            if (Connect("firstBase.sqlite"))
            {
                Console.WriteLine("Connected");
                command = new SQLiteCommand(connection)
                {
                    CommandText = "CREATE TABLE IF NOT EXISTS [Persons]([id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, [name] TEXT, [addr] TEXT);" +
                    "CREATE TABLE IF NOT EXISTS [Books]([id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, [title] TEXT, [author] TEXT);" +
                    "CREATE TABLE IF NOT EXISTS [PersonsBooks]([id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, person_id INTEGER NOT NULL, book_id INTEGER NOT NULL, FOREIGN KEY (person_id) REFERENCES Persons(id), FOREIGN KEY (book_id) REFERENCES Books(id));"
                };
                command.ExecuteNonQuery();
                Console.WriteLine("Таблица создана");
                choice();
            }
        }

        static void choice()
        {
            Console.WriteLine("Выберите цифру, соответствующую таблице для взаимодействия: 1 - Читатели, 2 - Книги, 3 - Читательский билет");
            string answer = Console.ReadLine();
            switch (answer)
            {
                case ("1"):
                    {
                        command.CommandText = "SELECT * FROM Persons";
                        DataTable data = new DataTable();
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                        adapter.Fill(data);
                        Console.WriteLine($"Прочитано {data.Rows.Count} записей из таблицы БД");
                        foreach (DataRow row in data.Rows)
                        {
                            Console.WriteLine($"id = {row.Field<long>("id")} name = {row.Field<string>("name")} addr = {row.Field<string>("addr")}}");
                        }
                        Console.WriteLine("Выберите цифру, соответствующую таблице для взаимодействия: ");
                        Console.WriteLine("1 - добавление записи, 2 - удаление записи, 3 - изменение записи, 4 - поиск по талице");
                        string answer2 = Console.ReadLine();
                        break;
                    }
                case ("2"):
                    {
                        break;
                    }
                case ("3"):
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Нет управления");
                        choice();
                        break;
                    }
            }
        }
    }
}