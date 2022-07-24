using System;
using System.IO;

namespace dz7
{
    /*Создайте справочник «Сотрудники».

    Создайте структуру «Сотрудник» со следующими полями:
    ID
    Дату и время добавления записи
    Ф.И.О.
    Возраст
    Рост
    Дату рождения
    Место рождения

    Для записей реализуйте следующие функции:

    Просмотр записи. Функция должна содержать параметр ID записи, которую необходимо вывести на экран. 
    Создание записи.
    Удаление записи.
    Редактирование записи.
    Загрузка записей в выбранном диапазоне дат.
    Сортировка по возрастанию и убыванию даты.

     */

    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            repository = repository.LoadFileInfo();

            Console.WriteLine("СПРАВОЧНИК СОТРУДНИКОВ");

            byte i;

            do
            {
                for (byte j = 0; j < 35; j++)
                {
                    Console.Write("=");
                }
                Console.WriteLine("\n1. Вывести данные.\n2. Внести данные.\n3. Выход.");
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        DataOutput();
                        break;
                    case 2:
                        DataInput();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            } while (i != 3);
        }

        static void DataOutput()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"Employees.txt"))
                {
                    string s = string.Empty;
                    if ((s = sr.ReadLine()) == null)
                    {
                        Console.WriteLine("Файл пуст. Добавить записи?");
                        Console.WriteLine("1. Да.\n2. Нет.");
                        byte i;
                        do
                        {
                            i = Convert.ToByte(Console.ReadLine());

                            switch (i)
                            {
                                case 1:
                                    sr.Close();
                                    DataInput();
                                    return;
                                case 2:
                                    break;
                                default:
                                    Console.WriteLine("Ошибка выбора");
                                    break;
                            }
                        } while (i != 2);
                    }
                    else
                    {
                        Console.WriteLine("СПИСОК СОТРУДНИКОВ");
                        string[] lines = File.ReadAllLines(@"Employees.txt");

                        foreach (var line in lines)
                        {
                            Console.WriteLine(line.Replace('#', ' '));
                        }
                    }
                }
            }
            catch
            {
                FileCreate();
            }
        }

        static void DataInput()
        {
            byte i = 1;
            do
            {
                switch (i)
                {
                    case 1:
                        string id = SetID();

                        using (StreamWriter sw = new StreamWriter(@"Employees.txt", true))
                        {
                            string note = string.Empty;
                            //1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва

                            //ID
                            note += id + '#';

                            //Дату и время добавления записи
                            string now = DateTime.Now.ToShortDateString();
                            now += ' ' + DateTime.Now.ToShortTimeString();
                            note += $"{now}#";

                            //Ф.И.О.
                            Console.WriteLine("Введите ФИО: ");
                            note += $"{Console.ReadLine()}#";

                            //Возраст
                            Console.WriteLine("Введите возраст: ");
                            note += $"{Console.ReadLine()}#";

                            //Рост
                            Console.WriteLine("Введите рост: ");
                            note += $"{Console.ReadLine()}#";

                            //Дату рождения
                            Console.WriteLine("Введите дату рождения: ");
                            note += $"{Console.ReadLine()}#";

                            //Место рождения
                            Console.WriteLine("Введите место рождения: ");
                            note += $"{Console.ReadLine()}";

                            for (byte j = 0; j < 35; j++)
                            {
                                Console.Write("=");
                            }

                            Console.WriteLine($"Введенная запись:\n{note}");
                            sw.WriteLine(note);

                            Console.WriteLine("Внести следующего сотрудника?\n1. Да.\n2. Нет");
                            i = Convert.ToByte(Console.ReadLine());
                        }
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            } while (i == 1);
        }

        static void FileCreate()
        {
            Console.WriteLine("Файла не существует. Создать новый файл?");
            Console.WriteLine("1. Да.\n2. Нет.");
            byte i;
            do
            {
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        using (FileStream fs = File.Create(@"Employees.txt"))
                        {
                            Console.WriteLine("Файл создан.");
                        }
                        return;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            } while (i != 2);
        }

        static string SetID()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"Employees.txt"))
                {
                    string s = string.Empty;
                    if ((s = sr.ReadLine()) == null)
                    {
                        return "1";
                    }

                    string[] lines = File.ReadAllLines(@"Employees.txt");
                    int[] ids = new int[lines.Length];
                    int i = 0;

                    foreach (var line in lines)
                    {
                        int ind = line.IndexOf('#');
                        string id = line.Substring(0, ind);
                        ids[i] = Convert.ToInt32(id);
                        i++;
                    }

                    Array.Sort(ids);
                    for (i = 0; i < ids.Length - 1; i++)
                    {
                        if (ids[i] != (ids[i + 1] - 1))
                        {
                            return Convert.ToString(ids[i] + 1);
                        }
                    }

                    return Convert.ToString(ids.Length + 1);
                }
            }
            catch
            {
                using (FileStream fs = File.Create(@"Employees.txt"))
                {
                    Console.WriteLine("Файл создан.");
                    fs.Close();
                    return SetID();
                }
            }
        }
    }
}
