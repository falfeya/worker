using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Worker
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Age { get; set; } 
    }
    class Repository
    {
        public string _fileName;

        public Repository(string fileName)
        {
            _fileName = fileName;
        }

        public void ViewAll()
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            using (var reader = new StreamReader(_fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var worker = new Worker
                    {
                        ID = int.Parse(values[0]),
                        Date = DateTime.Parse(values[1]),
                        Name = values[2],
                        Age = int.Parse(values[3])
                    };

                    Console.WriteLine($"{worker.ID}, {worker.Date}, {worker.Name}, {worker.Age}");
                }
            }
        }

        public void View(int id)
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            using (var reader = new StreamReader(_fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (int.Parse(values[0]) == id)
                    {
                        var worker = new Worker
                        {
                            ID = int.Parse(values[0]),
                            Date = DateTime.Parse(values[1]),
                            Name = values[2],
                            Age = int.Parse(values[3])
                        };

                        Console.WriteLine($"{worker.ID}, {worker.Date}, {worker.Name}, {worker.Age}");
                        return;
                    }
                }

                Console.WriteLine("Запись не найдена");
            }
        }

        public void Create(Worker worker)
        {
            using (var writer = new StreamWriter(_fileName, true))
            {
                writer.WriteLine($"{worker.ID},{worker.Date},{worker.Name},{worker.Age}");
            }
        }

        public void Delete(int id)
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            var tempFileName = Path.GetTempFileName();

            using (var reader = new StreamReader(_fileName))
            using (var writer = new StreamWriter(tempFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (int.Parse(values[0]) != id)
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            File.Delete(_fileName);
            File.Move(tempFileName, _fileName);
        }

        public void LoadByDateRange(DateTime startDate, DateTime endDate)
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            using (var reader = new StreamReader(_fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var date = DateTime.Parse(values[1]);

                    if (date >= startDate && date <= endDate)
                    {
                        var worker = new Worker
                        {
                            ID = int.Parse(values[0]),
                            Date = date,
                            Name = values[2],
                            Age = int.Parse(values[3])
                        };

                        Console.WriteLine($"{worker.ID}, {worker.Date}, {worker.Name}, {worker.Age}");
                    }
                }
            }
        }
    }
}
