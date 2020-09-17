using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует приложение Fitness");
            Console.WriteLine("Введите имя пользлвателя");
            var name = Console.ReadLine();

            Console.WriteLine("Введите  пол");
            var gender = Console.ReadLine();

            Console.WriteLine("Введите  дату рождения");
            var birhDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введите  вес");
            var weight = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите  рост");
            var height = double.Parse(Console.ReadLine());

            var userController = new UserController(name, gender, birhDate, weight, height);
            userController.Save();
        }
    }
}
