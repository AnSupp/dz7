using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace dz7
{
    //Просмотр записи.  Функция должна содержать параметр ID записи, которую необходимо вывести на экран.
    //Создание записи.   
    //Удаление записи.  
    //Редактирование записи.  
    //Загрузка записей в выбранном диапазоне дат.  
    //Сортировка по возрастанию и убыванию даты.
    struct Repository
    {
        public Employee[] Employees;

        public Repository(params Employee[] Args)
        {
            Employees = Args;
        }

        //public string this[int index]
        //{
        //    get { return this.Employees[index].Print(); }
        //}

        public Repository LoadFileInfo() //первоначальная загрузка из файла
        {
            using (StreamReader sr = new StreamReader(@"Employees.txt"))
            {
                string[] lines = File.ReadAllLines(@"Employees.txt");

                Employee[] employees = new Employee[lines.Length];

                int i = 0;

                foreach (var line in lines)
                {
                    
                    string[] subline = line.Split('#');

                    employees[i] = new Employee(Convert.ToInt32(subline[0]), Convert.ToString(subline[1]), Convert.ToString(subline[2]), 
                        Convert.ToByte(subline[3]), Convert.ToByte(subline[4]), Convert.ToString(subline[5]), Convert.ToString(subline[6]));

                    i++;

                }
                return new Repository(employees);
            }
        }

        public void FullDataOutput() //вывод всех записей
        {
            int i = 0;
            foreach(Employee employee in Employees)
            {
                Employees[i].GetInfo();
                i++;
            }         
        }

        public int SetID() //автоматическое получение ID
        {
            if (Employees.Length == 0)
            {
                return 1;
            }
            else
            {
                int[] ids = new int[Employees.Length];

                int i = 0;

                foreach (var id in ids)
                {
                    ids[i] = Employees[i].ID;
                    i++;
                }

                Array.Sort(ids);
                for (i = 0; i < ids.Length - 1; i++)
                {
                    if (ids[i] != (ids[i + 1] - 1))
                    {
                        return (ids[i] + 1);
                    }
                }

                return (ids.Length + 1);
            }
        }

        public int SearchID(string action) //поиск записи (по ID)
        {
            Console.Write($"Введите ID сотрудника, данные которого необходимо {action}: ");
            int index = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < Employees.Length; i++)
            {
                if (Employees[i].ID == index)
                {
                    return i;
                }
                else if (i == Employees.Length - 1)
                {
                    Console.WriteLine("Сотрудник с данным ID не найден.");      
                }
            }
            return -1;
        }

        public void NoteOutput() //вывод записи на экран (по ID)
        {
            int index = SearchID("показать");

            if (index == -1)
            {
                return;
            }

            Employees[index].GetInfo();
        }
        
        public void NoteCreate() //создание новой записи
        {
            Employee employee = new Employee();

            //1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва

            //ID
            employee.ID = SetID();

            //Дату и время добавления записи
            employee.SetCreationDate();

            //Ф.И.О.
            Console.WriteLine("Введите ФИО: ");
            employee.FullName = Console.ReadLine();

            //Возраст
            Console.WriteLine("Введите возраст: ");
            employee.Age = Convert.ToByte(Console.ReadLine());

            //Рост
            Console.WriteLine("Введите рост: ");
            employee.Height = Convert.ToByte(Console.ReadLine());

            //Дату рождения
            Console.WriteLine("Введите дату рождения: ");
            employee.BirthDate = Console.ReadLine();

            //Место рождения
            Console.WriteLine("Введите место рождения: ");
            employee.BirthPlace = Console.ReadLine();

            Console.WriteLine("Введенная запись:");
            employee.GetInfo();

            //вставка нового значения в конец массива сотрудников
            Employee[] newEmployees = new Employee[Employees.Length + 1];

            newEmployees[newEmployees.Length - 1] = employee;

            for (int i = 0; i < Employees.Length; i++)
            {
                newEmployees[i] = Employees[i];
            }
            
            Employees = newEmployees;
        }
    
        public void NoteChange() //изменение записи (по ID)
        {
            int index = SearchID("изменить");

            if (index == -1)
            {
                return;
            }

            byte k = 0;
            do
            {
                Console.WriteLine("Выбранная запись:");
                Employees[index].GetInfo();

                Console.WriteLine("\nКакой параметр необходимо изменить?");
                Console.WriteLine("1. ФИО.\n2. Возраст.\n3. Рост.\n" +
                    "4. Дата рождения.\n5. Место рождения.\n6. Отмена.");
                k = Convert.ToByte(Console.ReadLine());

                switch (k)
                {
                    case 1:
                        Console.Write("Введите новое ФИО: ");
                        Employees[index].FullName = Console.ReadLine();
                        Employees[index].SetCreationDate();
                        break;
                    case 2:
                        Console.Write("Введите новый возраст: ");
                        Employees[index].Age = Convert.ToByte(Console.ReadLine());
                        Employees[index].SetCreationDate();
                        break;
                    case 3:
                        Console.Write("Введите новый рост: ");
                        Employees[index].Height = Convert.ToByte(Console.ReadLine());
                        Employees[index].SetCreationDate();
                        break;
                    case 4:
                        Console.Write("Введите новую дату рождения: ");
                        Employees[index].BirthDate = Console.ReadLine();
                        Employees[index].SetCreationDate();
                        break;
                    case 5:
                        Console.Write("Введите новое место рождения: ");
                        Employees[index].BirthPlace = Console.ReadLine();
                        Employees[index].SetCreationDate();
                        break;
                    case 6:

                        break;
                    default:
                        Console.WriteLine("Ошибка выбора.");
                        break;
                }

            } while (k != 6);
        }
    
        public void NoteDelete() //удаление записи (по ID)
        {
            int index = SearchID("удалить");

            if (index == -1)
            {
                return;
            }

            Console.WriteLine("Выбранная запись подлежит удалению:");
            Employees[index].GetInfo();
            
            byte k;

            do
            {
                Console.WriteLine("Вы уверены, что хотите удалить запись?\n1. Удалить.\n2. Отмена.");
                k = Convert.ToByte(Console.ReadLine());

                switch (k)
                {
                    case 1:
                        Employee[] newEmployees = new Employee[Employees.Length - 1];

                        for (int i = 0; i < index; i++)
                        {
                            newEmployees[i] = Employees[i];
                        }

                        for (int i = index + 1; i < Employees.Length; i++)
                        {
                            newEmployees[i - 1] = Employees[i];
                        }

                        Employees = newEmployees;

                        return;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора.");
                        break;
                }
            } while (k != 2);
        }   
    
        public void DateCreationSort() //сортрировка по дате
        {
            //массив дат под ключи
            DateTime[] dates = new DateTime[Employees.Length];
            int i = 0;

            foreach (Employee employee in Employees)
            {
                dates[i] = DateTime.Parse(Employees[i].CreationDate, System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
                i++;
            }

            Array.Sort(dates, Employees);

            FullDataOutput();

            Array.Reverse(Employees);

            FullDataOutput();

        }
    
        public void RangeDataOutput() //вывод записей в диапазоне дат
        {
            Console.WriteLine("Введите начальную дату: ");
            DateTime date1 = DateTime.Parse(Console.ReadLine(), System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));

            Console.WriteLine("Введите конечную дату: ");
            DateTime date2 = DateTime.Parse(Console.ReadLine(), System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));

            Console.WriteLine(date1);
            Console.WriteLine(date2);

            bool empty = true;

            foreach(Employee employee in Employees)
            {
                DateTime date = DateTime.Parse(employee.CreationDate, System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));

                if ((date >= date1) && (date <= date2))
                {
                    employee.GetInfo();
                    empty = false;
                }
            }

            if (empty)
            {
                Console.WriteLine("В заданном диапазоне нет записей");
            }
        }
    }

    struct Employee
    {
        private int id;

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        private string creationDate;
        public string CreationDate
        {
            get { return this.creationDate; }
            private set { this.creationDate = value; }
        }

        private string fullName;
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        private byte age;
        public byte Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        private byte height;
        public byte Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        private string birthDate;
        public string BirthDate
        {
            get { return this.birthDate; }
            set { this.birthDate = value; }
        }

        private string birthPlace;
        public string BirthPlace
        {
            get { return this.birthPlace; }
            set { this.birthPlace = value; }
        }

        //базовый конструктор
        public Employee(int id, string creationDate, string fullName, byte age, byte height, string birthDate, string birthPlace)
        {
            this.id = id;
            this.creationDate = creationDate;
            this.fullName = fullName;
            this.age = age;
            this.height = height;
            this.birthDate = birthDate;
            this.birthPlace = birthPlace;
        }

        public void GetInfo() //вывод записи на экран
        {   
            Console.WriteLine($"{id} {creationDate, -16} {fullName, -25} {age} {height} {birthDate, -10} {birthPlace}");
        }

        public void SetCreationDate() //получаем время записи
        {
            string now = DateTime.Now.ToString("g", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU")); 
            creationDate = now;
        }

    }
}
