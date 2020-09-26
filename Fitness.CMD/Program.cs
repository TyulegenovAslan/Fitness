using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var culture = CultureInfo.CreateSpecificCulture("ru-ru");
            var resourceManager = new ResourceManager("Fitness.CMD.Languages.Messages", typeof(Program).Assembly);

            Console.WriteLine(resourceManager.GetString("Hello", culture));
            Console.WriteLine(resourceManager.GetString("EnterName", culture));
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);
            var exerciseController = new ExerciseController(userController.CurrentUser);

            if (userController.IsNewUser)
            {
                Console.Write("Введите пол: ");
                var gender = Console.ReadLine();
                var birthDate = ParseDateTime("дата рождкния");
                var weight = ParseDouble("Вес");
                var height = ParseDouble("Рост");

                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            Console.WriteLine(userController.CurrentUser);

            while (true)
            {
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("E - ввести прием пищи");
                Console.WriteLine("A - ввести упражднение");
                Console.WriteLine("Q - выход");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.E:
                        var food = EnterEating();
                        eatingController.Add(food.Food, food.Weight);

                        foreach (var item in eatingController.Eating.Foods)
                        {
                            Console.WriteLine($"\t{item.Key} - {item.Value}");
                        }
                        break;
                    case ConsoleKey.A:
                        var exe = EnterExercise();
                        exerciseController.Add(exe.activity, exe.begin, exe.end);
                        
                        foreach(var item in exerciseController.Exercises)
                        {
                            Console.WriteLine($"\t {item.Activity} c {item.Start.ToShortTimeString()} до {item.Finish.ToShortTimeString()}");
                        }
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }

                Console.ReadLine();
            }
        }

        private static (DateTime begin, DateTime end, Activity activity) EnterExercise()
        {
            Console.WriteLine("Введите название упражднения: ");
            var name = Console.ReadLine();

            var energy = ParseDouble("расход энергии в минуту");

            var begin = ParseDateTime("начало упражднения");
            var end = ParseDateTime("оконяание упражднения");

            var activity = new Activity(name, energy);
            return (begin, end, activity);
        }

        private static (Food Food, double Weight) EnterEating()
        {
            Console.WriteLine("\nВведите имя продукта: ");
            var food = Console.ReadLine();

            var calories = ParseDouble("калорийность");
            var prot = ParseDouble("белки");
            var fats = ParseDouble("жиры");
            var carbs = ParseDouble("углеводы");
            var weight = ParseDouble("вес порции");
            var product = new Food(food);

            return (Food: product, Weight: weight);
        }

        private static DateTime ParseDateTime(string value)
        {
            DateTime birthDate;
            while (true)
            {
                Console.WriteLine($"Введите {value} (dd.mm.yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат поля: {value}");
                }
            }

            return birthDate;
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Неверный формат поля {name}");
                }
            }
        }
    }
}
