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

    //1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва
    //2#15.12.2021 03:12#Алексеев Алексей Иванович#24#176#05.11.1980#город Томск
    class Program
    {
        static void Main(string[] args)
        {
            DoubleLine();

            Console.WriteLine($"{"СПРАВОЧНИК СОТРУДНИКОВ", 34}");

            Repository repository = new Repository();

            //пробуем загрузить файл
            try
            {
                repository = repository.LoadFileInfo();
            }
            catch
            {
                DoubleLine();

                FileCreate();   //при отсутствии файла автоматически создаем
                repository = repository.LoadFileInfo();

                Console.WriteLine("Для начала работы создайте хотя бы одну запись.\n");

                DataCreate(ref repository); //и просим пользователя создать запись

            }

            byte i;
            do
            {
                DoubleLine();
                Console.WriteLine("1. Вывести данные.\n2. Редактировать данные.\n3. Выход.");
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:                                          
                        //всяческий вывод данных
                        //проверка на пустоту для избежания ошибок
                        if (repository.NullCheck())
                        {
                            byte k;
                            do
                            {
                                Console.WriteLine("Файл пуст. Создать запись?");
                                Console.WriteLine("1. Да.\n2. Нет.");
                                k = Convert.ToByte(Console.ReadLine());
                        
                                switch (k)
                                {
                                    case 1:
                                        DataCreate(ref repository);
                                        k = 2;
                                        break;
                                    case 2:
                                        break;
                                    default:
                                        Console.WriteLine("Ошибка выбора");
                                        break;
                                }                        
                            } while (k != 2);
                        }
                        else 
                        {
                          DataOutput(repository);
                        }
                        break;
                    case 2:
                        DataManage(ref repository);
                        break;
                    case 3:
                        repository.SaveFileInfo();
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            } while (i != 3);
        }

        static void DataOutput(Repository repository)
        {
            byte i;
            do
            {
                DoubleLine();
                Console.WriteLine("1. Вывести список.\n2. Найти сотрудника." +
                    "\n3. Сортировать по дате создания.\n4. Найти в диапазоне дат.\n5. Назад.");
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        repository.FullDataOutput(repository.Employees);
                        break;
                    case 2:
                        repository.NoteOutput();
                        break;
                    case 3:
                        byte k;
                        do
                        {
                            Console.WriteLine("\nВывести данные в порядке возрастания или убывания ?");
                            Console.WriteLine("1. В порядке возрастания.\n2. В порядке убывания.\n3. Назад.");
                            k = Convert.ToByte(Console.ReadLine());

                            switch (k) {
                                case 1:
                                    repository.DateCreationSort(true);
                                    break;
                                case 2:
                                    repository.DateCreationSort(false);
                                    break;
                                case 3:
                                    break;
                                default:
                                    Console.WriteLine("Ошибка выбора");
                                    break;       
                            }
                        } while (k != 3);                       
                        break;
                    case 4:
                        repository.RangeDataOutput();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            } while (i != 5);
        }

        static void DataManage(ref Repository repository)
        {
            byte i;
            do
            {
                DoubleLine();
                Console.WriteLine("1. Создать запись.\n2. Редактировать запись." +
                    "\n3. Удалить запись.\n4. Назад.");
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        DataCreate(ref repository);
                        break;
                    case 2:
                        repository.NoteChange();
                        break;
                    case 3:
                        repository.NoteDelete();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            } while (i != 4);
        }

        static void DataCreate(ref Repository repository) //создание записей
        {
            byte i = 1;
            do
            {
                switch (i)
                {
                    case 1:
                        repository.NoteCreate();

                        Console.WriteLine("\nВнести следующего сотрудника?\n1. Да.\n2. Нет");
                        i = Convert.ToByte(Console.ReadLine());                       
                        break;
                    case 2:                   
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            } while (i != 2);
        }

        static void FileCreate()
        {
            using (FileStream fs = File.Create(@"Employees.txt"))
            {
                Console.WriteLine("Файл сотрудников создан.");
            }
        }

        static void DoubleLine()
        {
            for (byte j = 0; j < 46; j++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }
    }
}
