using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialize_People
{
    // Простая программа, котора будет принимать имя, год и месяц
    // создавать person на основе этих данных, 
    // и будет выводить на экран возраст этого person.
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Person p = Deserialize();
                Console.WriteLine(p.ToString());
            }
            else
            {
                try
                {
                    if (args.Length != 4)
                    {
                        throw new ArgumentException("You must provide four arguments.");
                    }

                    DateTime dob = new DateTime(Int32.Parse(args[1]), Int32.Parse(args[2]), Int32.Parse(args[3]));
                    Person p = new Person(args[0], dob);
                    Console.WriteLine(p.ToString());

                    Serialize(p);
                }
                catch (Exception ex)
                {
                    DisplayUsageInformation(ex.Message);
                }
            }
        }

        private static void DisplayUsageInformation(string message)
        {
            Console.WriteLine("\nERROR: Invalid parameters. " + message);
            Console.WriteLine("\nSerialize_People \"Name\" Year Month Date");
            Console.WriteLine("\nFor example:\nSerialize_People \"Tony\" 1922 11 22");
            Console.WriteLine("\nOr, run the command with no arguments to display that previous person.");
        }

        private static void Serialize(Person sp)
        {
            // TODO: Serialize sp object
            // Создаем файл для сохранения данных
            FileStream fs = new FileStream("Person.Dat", FileMode.Create);

            // Создаем BinaryFormatter объект для сериализации
            BinaryFormatter bf = new BinaryFormatter();

            // Используем BinaryFormatter объект для сериализации данных в файл
            bf.Serialize(fs, sp);

            // Закрываем файл
            fs.Close();

        }

        // C#
        private static Person Deserialize()
        {
            Person dsp = new Person();

            // Открываем файл для чтения данных 
            FileStream fs = new FileStream("Person.Dat", FileMode.Open);
            // Создаем объект BinaryFormatter для выполнения десериализации
            BinaryFormatter bf = new BinaryFormatter();
            // Используем объект BinaryFormatter для десериализации данных из файла
            dsp = (Person)bf.Deserialize(fs);
            //Закрываем файл 
            fs.Close();

            return dsp;
        }

    }
}