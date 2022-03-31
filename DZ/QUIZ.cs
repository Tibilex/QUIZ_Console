using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DZ
{
    internal class QUIZ : LogPass
    {
        internal int CorectAnswers;                             // Счетчик правильный ответов
        internal int IncorectAnswers;                           // Счетчик не правильных ответов
        internal string[] AnsData;                              // Массив хранения вариантов ответа
        internal List<string> Categories = new List<string>     // Список хранения названий категорий
        { "История", "Биология", "География", "Космос" };                      
        /// <summary>
        /// Главное меню, с варинатами выбора и подменю.
        /// </summary>
        internal void QuizMainMenu()
        {
            int MenuKey;

            LoginForm();

            if (VerificationCheck == true)                      // Проверка на правильность авторизации пройдена
            {
                while (true)
                {
                    Console.WriteLine("\t\t*ВИКТОРИНА*\n 1. Начать новую викторину.\n 2. Просмотреть результаты прошлых игр.\n" +
                        " 3. Выход.");

                    MenuKey = Convert.ToInt32(Console.ReadLine());
                    switch (MenuKey)
                    {
                        case 1:                                 // Начать новую викторину
                        restart:
                            Console.Clear();
                            Console.WriteLine("\t\tВЫБЕРИТЕ КАТЕГОРИЮ!");
                            Console.WriteLine(" 1. История        ", Console.BackgroundColor = ConsoleColor.Red, Console.ForegroundColor = ConsoleColor.Black);
                            Console.WriteLine(" 2. Биология       ", Console.BackgroundColor = ConsoleColor.DarkGreen, Console.ForegroundColor = ConsoleColor.Black);
                            Console.WriteLine(" 3. География      ", Console.BackgroundColor = ConsoleColor.DarkYellow, Console.ForegroundColor = ConsoleColor.Black);
                            Console.WriteLine(" 4. Космос         ", Console.BackgroundColor = ConsoleColor.Blue, Console.ForegroundColor = ConsoleColor.Black);
                            Console.WriteLine(" 5. Случайная тема ", Console.BackgroundColor = ConsoleColor.Gray, Console.ForegroundColor = ConsoleColor.Black);
                            Console.ResetColor();

                            MenuKey = Convert.ToInt32(Console.ReadLine());
                            Quiz(MenuKey);

                            Console.WriteLine(" Сыграть Викторину еще раз?\n 1. Да\n 2. Нет");
                            MenuKey = Convert.ToInt32(Console.ReadLine());
                            if (MenuKey == 1)
                                goto restart;
                            if (MenuKey == 2)
                            {
                                Console.Clear();
                                break;
                            }
                            break;
                        case 2:                                 // Статистика викторин
                            Console.Clear();
                            Console.WriteLine("\tРЕЗУЛЬТАТЫ ПРОШЛЫХ ВИКТОРИН!");
                            Console.WriteLine("Логин\t  Верно\t    Неверно   Тема");
                            Statistic(0, false);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 3:

                            break;
                    }
                    if (MenuKey == 3)
                        break;
                }
            }
        }
        /// <summary>
        /// Обрабатывает и отображает вопросы и ответы
        /// </summary>
        /// <param name="choise"></param>
        internal void Quiz(int choise)
        {
            Console.Clear();
            switch (choise)
            {
                case 1:
                    for (int i = 0; i < 10; i++)
                    {
                        ReadingData(choise, true, i);           // Вывод вопроса
                        Console.WriteLine("\nВаринаты ответа! ");
                        ReadingData(choise, false, i);          // Вывод ответа
                        AnswersReading();                       // Проверка результатов ответа
                    }
                    Console.WriteLine(" Викторина завершена!");
                    Console.ReadKey();
                    Statistic(choise, true);                    // Собирает статистику
                    break;
                case 2:
                    for (int i = 0; i < 10; i++)
                    {
                        ReadingData(choise, true, i);
                        Console.WriteLine("\nВаринаты ответа! ");
                        ReadingData(choise, false, i);
                        AnswersReading();
                    }
                    Console.WriteLine(" Викторина завершена!");
                    Console.ReadKey();
                    Statistic(choise, true);
                    break;
                case 3:
                    for (int i = 0; i < 10; i++)
                    {
                        ReadingData(choise, true, i);
                        Console.WriteLine("\nВаринаты ответа! ");
                        ReadingData(choise, false, i);
                        AnswersReading();
                    }
                    Console.WriteLine(" Викторина завершена!");
                    Console.ReadKey();
                    Statistic(choise, true);
                    break;
                case 4:
                    for (int i = 0; i < 10; i++)
                    {
                        ReadingData(choise, true, i);
                        Console.WriteLine("\nВаринаты ответа! ");
                        ReadingData(choise, false, i);
                        AnswersReading();                      
                    }
                    Console.WriteLine(" Викторина завершена!");
                    Console.ReadKey();
                    Statistic(choise, true);
                    break;
                case 5:
                    for (int i = 0; i < 10; i++)
                    {
                        ReadingData(choise, true, i);
                        Console.WriteLine("\nВаринаты ответа! ");
                        ReadingData(choise, false, i);
                        AnswersReading();
                    }
                    Console.WriteLine(" Викторина завершена!");
                    Console.ReadKey();
                    Statistic(choise, true);
                    break;
            }
        }
        /// <summary>
        /// Читает из файла построчно, выводит в консоль строку 
        /// </summary>
        /// <param name="Qdir">id категории</param>
        /// <param name="ANSdir">свитч между вопрос/ответ</param>
        /// <param name="index">id строки</param>
        internal void ReadingData(int Qdir, bool ANSdir, int index)
        {
            try
            {
                if (ANSdir == true)
                {
                    using (StreamReader sr = new StreamReader(Pathes(Qdir, ANSdir), Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {

                            line = File.ReadLines(Pathes(Qdir, ANSdir)).ElementAt(index);
                            Console.WriteLine(line);
                            break;
                        }
                    }

                }
                else if (ANSdir == false)
                {
                    using (StreamReader sr = new StreamReader(Pathes(Qdir, ANSdir), Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {

                            line = File.ReadLines(Pathes(Qdir, ANSdir)).ElementAt(index);
                            AnsData = line.Split(' ');
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Хранит пути хранения файлов
        /// </summary>
        /// <param name="Qdir">id выбора категории</param>
        /// <param name="ANSdir">Свитч категории вопросов и ответов</param>
        /// <returns>Возвращает полный путь хранения файла</returns>
        internal string Pathes(int Qdir, bool ANSdir)
        {
            string workingDirectory = Environment.CurrentDirectory;                         // Путь к файлу в папке проекта
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            var RndCategory = new string[]                                                  // Массив строк хранения второй половины пути к файлам вопросов
            { 
                @"Files\History.txt",
                @"Files\Biology.txt",
                @"Files\Geography.txt",
                @"Files\Space.txt"
            };
            var RndCategoryAns = new string[]                                               // Массив строк хранения второй половины пути к файлам ответов
            {
                @"Files\HistoryAnswers.txt",
                @"Files\BiologyAnswers.txt",
                @"Files\GeographyAnswers.txt",
                @"Files\SpaceAnswers.txt"
            };
            Random RandomCategory = new Random();

            if (Qdir == 1 && ANSdir == true) { return Path.Combine(projectDirectory, @"Files\History.txt"); }
            if (Qdir == 2 && ANSdir == true) { return Path.Combine(projectDirectory, @"Files\Biology.txt"); }
            if (Qdir == 3 && ANSdir == true) { return Path.Combine(projectDirectory, @"Files\Geography.txt"); }
            if (Qdir == 4 && ANSdir == true) { return Path.Combine(projectDirectory, @"Files\Space.txt"); }
            if (Qdir == 5 && ANSdir == true) { return Path.Combine(projectDirectory, RndCategory[RandomCategory.Next(0, 4)]); }

            if (Qdir == 1 && ANSdir == false) { return Path.Combine(projectDirectory, @"Files\HistoryAnswers.txt"); }
            if (Qdir == 2 && ANSdir == false) { return Path.Combine(projectDirectory, @"Files\BiologyAnswers.txt"); }
            if (Qdir == 3 && ANSdir == false) { return Path.Combine(projectDirectory, @"Files\GeographyAnswers.txt"); }
            if (Qdir == 4 && ANSdir == false) { return Path.Combine(projectDirectory, @"Files\SpaceAnswers.txt"); }
            if (Qdir == 5 && ANSdir == false) { return Path.Combine(projectDirectory, RndCategoryAns[RandomCategory.Next(0, 4)]); }
            if (Qdir == 6 && ANSdir == false) { return Path.Combine(projectDirectory, @"Files\Rate.txt"); }
            return @"НЕ ВЕРНЫЙ ВВОД";
        }
        /// <summary>
        /// Принимает варинат ответа и анализирует его, записывает результат в переменную
        /// </summary>
        internal void AnswersReading()
        {
            int tmp = 1;
            int AnsCheck = int.Parse(AnsData[4]);
            foreach (var i in AnsData)                                                      // Показывает варианты овета на экран
            {
                Console.WriteLine($"{tmp}. {i}");
                tmp++;
                if (tmp == 5) break;
            }
            Console.WriteLine("\nВаш ответ? ");
            int ans = int.Parse(Console.ReadLine());                                        // Принимает номер варината ответа
            if (ans == AnsCheck)                                                            // Ответ правильный
            {
                Console.WriteLine("Правильный ответ! ", Console.ForegroundColor = ConsoleColor.Green);
                Console.ResetColor();
                CorectAnswers++;                                                            // Инкрементирует счетчик правильный ответов
                Console.ReadKey();
                Console.Clear();
            }
            else
            {                                                                               // Ответ не верный
                Console.WriteLine("Не правильный ответ! ", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                IncorectAnswers++;                                                          // Инкрементирует счетчик неверных ответов
                Console.ReadKey();
                Console.Clear();
            }
        }
        /// <summary>
        /// Собирает статистику и записывает в файл
        /// </summary>
        /// <param name="choise">Принмает номер категории викторины</param>
        /// <param name="WR">Если true записывает в файл, если false считывает из файла</param>
        internal void Statistic(int choise, bool WR)
        {
            string[] read;
            try
            {
                if (WR == true)
                {
                    using (StreamWriter sw = new StreamWriter(Pathes(6, false), true))           // Запись даных в файл, если файла не существует он будет создан
                    {
                        sw.WriteLine($"{Login,-10}{CorectAnswers,-10}{IncorectAnswers,-10}{Categories[choise - 1], -10}"); // Запись строки с отступов 10 пробелов вправо
                    }
                }
                if (WR == false)
                {
                    read = File.ReadAllLines(Pathes(6, false));
                    foreach (string line in read)
                    {
                        Console.WriteLine(line);
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
