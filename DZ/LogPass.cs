using System;
using System.IO;

namespace DZ
{
    internal class LogPass
    {
        internal string Login { get; set; }                                     // Поле Логина  
        internal string Password { get; set; }                                  // Поле Пароля
        internal string Date { get; set; }                                      // Поле Даты рождения

        internal string[] LoginLines;                                           // Массив строк, записывается из файла

        internal bool VerificationCheck = false;                                // Проверка на логирование пользователя

        internal string LoginPath = @"Files\Logins.txt";                        // Часть пути хранения файла с Лог/Пасс
        public LogPass() { }                                                    // Конструктор по умолчанию
        public LogPass(string log, string pass)                                 // Конструктор с параметрами
        {
            Login = log;
            Password = pass;
        }
        /// <summary>
        /// Основное меню, запрашивает варианты выбора функционала
        /// </summary>
        internal void LoginForm()                                               // Метод "Меню" с формой логина и регистрацией пользователей
        {
            Console.Clear();
            Console.WriteLine(" \t\tВведите логин и пароль для входа\n" +
               " \t\tЕсли нет учетной записи необходимо зарегистрироваться\n" +
               " 1. Ввести логин и пароль.\n 2. Зарегистрироваться.\n" +
               " 3. Редактирование профиля.\n 4. Выход.");
            int choice = Int32.Parse(Console.ReadLine());
            while (choice != 4)
            {
                switch (choice)
                {
                    case 1:                                                     // Авторизация пользователя
                        Console.Clear();
                        Console.WriteLine(" Введите логин");
                        Login = Console.ReadLine();
                        Console.WriteLine(" Введите пароль");
                        Password = Console.ReadLine();
                        if (Verifiacation(Login, Password))
                        {
                            Console.WriteLine("Вы залогинены");
                            VerificationCheck = true;
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (!Verifiacation(Login, Password))
                        {
                            Console.WriteLine("Не верный ввод");
                            VerificationCheck = false;
                            Console.ReadKey();
                            LoginForm();
                        }
                        break;
                    case 2:                                                     // Доавление нового пользователя
                        AddMember();
                        break;
                    case 3:                                                     // Рдактирование профиля с запросм логина и 
                        Console.Clear();
                        Console.WriteLine(" Введите логин");
                        Login = Console.ReadLine();
                        Console.WriteLine(" Введите пароль");
                        Password = Console.ReadLine();
                        if (Verifiacation(Login, Password))
                        {
                            Console.WriteLine("Вы залогинены");
                            VerificationCheck = true;
                            Console.ReadKey();
                            Console.Clear();
                            EditProfile();
                            Console.WriteLine(" Профиль обновлен!");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        else if (!Verifiacation(Login, Password))
                        {
                            Console.WriteLine("Не верный ввод");
                            VerificationCheck = false;
                            Console.ReadKey();
                            LoginForm();
                        }
                        break;
                }
                if (choice != 4)
                    break;
            }
        }
        /// <summary>
        /// Добаялет нового пользователя, запрашивает логин и пароль, записывает в файл
        /// </summary>
        internal void AddMember()                                               // Метод добавления нового пользователя
        {
            try
            {                                                                   // Запись даных в файл, если файла не существует он будет создан
                using (StreamWriter sw = new StreamWriter(FullPathToProject(LoginPath), true))
                {
                    Console.Clear();
                    Console.WriteLine("\t\tРегистрация нового пользователя!\n Введите логин");
                    Login = Console.ReadLine();
                    Console.WriteLine(" Введите пароль");
                    Password = Console.ReadLine();
                    Console.WriteLine(" Введите дату рождения в формате 01.01.2022");
                    Date = Console.ReadLine();
                    sw.WriteLine(Login + " " + Password + " " + Date);          // Запись логина, пароля и даты рождения через пробел " "
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Проверка на коректность ввода логина и пароля
        /// </summary>
        /// <param name="login">Принимает строку для проверки логина</param>
        /// <param name="password">Принимает строку для проверки пароля</param>
        /// <return></return>
        internal bool Verifiacation(string login, string password)
        {
            bool checklog = false;                                              // Проверка на совпадение логина
            bool checkpass = false;                                             // Проверка на совпадения пароля

            LoginLines = File.ReadAllLines(FullPathToProject(LoginPath));       // Запись строк файла в массив
            foreach (string line in LoginLines)
            {
                if (line.StartsWith($"{login} "))                               // Проверка на вхожддения логина в строку
                {
                    checklog = true;                                            //логин коректен
                }
                if (line.IndexOf($" {password}") > -1 && checklog == true)      // Проверка на вхожддения пароля в строку с учетом что логин коректн
                {
                    checkpass = true;                                           // пароль коректен
                    break;
                }
            }
            if (checklog == true && checkpass == true)                          // Возвращает true если логин ипароль введены правильно
                return true;
            else
                return false;
        }
        /// <summary>Полный путь каталога программы, принимает строку как параметр</summary>
        /// <param name="path"> Принимает строку </param>
        /// <returns>Возвращает полный путь к файлу в папке проекта</returns>
        internal string FullPathToProject(string path)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            return Path.Combine(projectDirectory, path);
        }
        /// <summary>
        /// Редактирует профиль пользователя, принимает новые даные и записывает в файл
        /// </summary>
        internal void EditProfile()
        {
            Console.WriteLine("\tРедактирование профиля\n" +
               $" Ваш логин {Login}\n Ваш пароль {Password}\n Ваша дата рождения {Date}");
            Console.WriteLine(" Введиет нове данные");
            Console.Write(" Логин - ");
            string log = Console.ReadLine();
            Console.Write(" Пароль - ");
            string pass = Console.ReadLine();

            string CurrentLine = "0";
            LoginLines = File.ReadAllLines(FullPathToProject(LoginPath));               // Запись строк из файла в массив
            foreach (string line in LoginLines)
            {
                if (line.StartsWith($"{Login} ") && line.IndexOf($" {Password}") > -1)  // Проверка но вхождение логина и пароля в строку
                {
                    string tmp1 = Login.Replace(Login, log);                            // Перезапись логина
                    string tmp2 = Password.Replace(Password, pass);                     // перезапись пароля
                    CurrentLine = tmp1 + " " + tmp2;                                    // Обьединяет логин и парол в строку
                    int index = Array.IndexOf(LoginLines, line);                        // Получает индекс строки массива 
                    LoginLines[index] = line.Replace(line, CurrentLine);                // Заменяет старую строку на новую по индексу
                    break;
                }
            }
            using (StreamWriter sw = new StreamWriter(FullPathToProject(LoginPath)))    // Перезапись в существующий файл маасива строк
            {
                foreach (string line in LoginLines)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
