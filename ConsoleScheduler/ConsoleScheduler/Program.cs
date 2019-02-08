using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerConsole
{
    class Program
    {

        static bool exit = false;
        static int menu = 0;
        static public List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {

            Database db = new Database();
            db.readTask();
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1.Открыть план на сегодня");
                Console.WriteLine("2.Открыть план на завтра");
                Console.WriteLine("3.Выбрать день");
                Console.WriteLine("4.Удалить все выполненые задачи");
                Console.WriteLine("0.Выход");
                Console.Write(" Выберите действие: ");
                try
                {
                    menu = Convert.ToInt16(Console.ReadLine());

                    switch (menu)
                    {

                        case 1:
                            ShowDate(DateTime.Today);
                            break;
                        case 2:
                            ShowDate(DateTime.Today.AddDays(1));
                            break;
                        case 3:

                            Console.Write("Год/месяц/день: ");
                            string[] strArr = Console.ReadLine().Split('/');

                            DateTime time = new DateTime(Convert.ToInt32(strArr[0]), Convert.ToInt32(strArr[1]), Convert.ToInt32(strArr[2]));
                            ShowDate(time);
                            break;
                        case 4:
                            DeleteDoneTasks();
                            break;
                        case 0:
                            exit = true;
                            break;
                    }
                }
                catch { }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.Write("1.Назначить задачу на определенное время  ");
            Console.Write("2.Добавить свободную задачу  ");
            Console.WriteLine("3.Добавить безсрочную задачу");
            Console.Write("4.Удалить задачу  ");
            Console.Write("5.Редактировать задачу  ");
            Console.Write("6.Отметить выполнение  ");
            Console.WriteLine("0.Назад");
        }

        static void ShowDate(DateTime time)
        {
            bool dayexit = false;
            int daymenu = 0;

            while (!dayexit)
            {

                tasks.Sort((x, y) => x.time.CompareTo(y.time));
                ShowMenu();
                Console.WriteLine("Дата: " + time.ToShortDateString());
                Console.WriteLine("\n Задачи с определенным времененем:");
                ShowTasks(time, 3);
                Console.WriteLine("\n Свободные задачи:");
                ShowTasks(time, 2);
                Console.WriteLine("\n Безсрочные задачи:");
                ShowTasks(1);
                Console.Write("\n Выберите действие: ");
                try
                {
                    daymenu = Convert.ToInt16(Console.ReadLine());

                    string[] strArr1 = time.ToShortDateString().Split('.');

                    switch (daymenu)
                    {
                        case 1:
                            Console.Write("Время (в формате: чч/мм): ");

                            string[] strArr = Console.ReadLine().Split('/');
                            DateTime time1 = new DateTime(Convert.ToInt32(strArr1[2]), Convert.ToInt32(strArr1[1]), Convert.ToInt32(strArr1[0]), Convert.ToInt32(strArr[0]), Convert.ToInt32(strArr[1]), 0);

                            Console.Write("Задача: ");
                            tasks.Add(new Task(time1, Console.ReadLine(), 3));
                            Console.Clear();
                            break;
                        case 2:
                            DateTime time2 = new DateTime(Convert.ToInt32(strArr1[2]), Convert.ToInt32(strArr1[1]), Convert.ToInt32(strArr1[0]));
                            Console.Write("Задача: ");
                            tasks.Add(new Task(time2, Console.ReadLine(), 2));

                            Console.Clear();
                            break;
                        case 3:
                            Console.Write("Задача: ");
                            tasks.Add(new Task(Console.ReadLine(), 1));
                            Console.Clear();
                            break;
                        case 4:
                            Console.Write("Введите id задачи (значение в ()): ");
                            DeleteTasks(Convert.ToInt32(Console.ReadLine()));
                            break;
                        case 5:
                            Console.Write("Введите id задачи (значение в ()): ");
                            int a = Convert.ToInt32(Console.ReadLine());
                            if (searcTask(a))
                            {
                                Console.Write("Ввдите 1 чтобы изменить время, 2 чтобы изменить задачу: ");
                                int red = Convert.ToInt32(Console.ReadLine());
                                switch (red)
                                {
                                    case 1:
                                        Database db = new Database();
                                        for (int i = tasks.Count() - 1; i >= 0; --i)
                                        {
                                            if (tasks[i].getID() == a)
                                            {
                                                Console.Write("Введите новое время (в формате: чч/мм): ");
                                                strArr = Console.ReadLine().Split('/');
                                                db.updtTask(a, strArr[0] + ":" + strArr[1]);
                                                tasks[i].editTime(new DateTime(Convert.ToInt32(strArr1[2]), Convert.ToInt32(strArr1[1]), Convert.ToInt32(strArr1[0]), Convert.ToInt32(strArr[0]), Convert.ToInt32(strArr[1]), 0));
                                            }
                                        }
                                        break;
                                    case 2:
                                        updateTask(a);

                                        break;

                                }
                            }
                            else
                            {
                                updateTask(a);
                            }
                            break;
                        case 6:
                            Console.Write("Введите id задачи (значение в ()): ");
                            AddSuccess(Convert.ToInt32(Console.ReadLine()));
                            break;
                        case 0:
                            dayexit = true;
                            break;

                    }
                }
                catch { }

                Console.Clear();
            }
        }

        static void ShowTasks(DateTime time, int a)
        {
            foreach (Task i in tasks)
            {
                if (i.getSuccess())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    if (a == 3) i.Write(time, a);
                    if (a == 2) i.Write(time);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {

                    if (a == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        i.Write(time, a);
                    }
                    if (a == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        i.Write(time);
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

        }

        static void ShowTasks(int a)
        {
            foreach (Task i in tasks)
            {
                if (i.getSuccess())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    if (a == 1) i.Write(a);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    if (a == 1) i.Write(a);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

        }

        static void DeleteTasks(int a)
        {
            Database db = new Database();
            for (int i = tasks.Count() - 1; i >= 0; --i)
            {
                if (tasks[i].getID() == a)
                {
                    tasks.RemoveAt(i);
                    db.delTask(a);
                }
            }

        }

        static void DeleteDoneTasks()
        {
            Database db = new Database();
            for (int i = tasks.Count() - 1; i >= 0; --i)
            {
                if (tasks[i].getSuccess())
                {
                    db.delTask(tasks[i].getID());
                    tasks.RemoveAt(i);
                }
            }

        }

        static void AddSuccess(int a)
        {
            Database db = new Database();
            for (int i = tasks.Count() - 1; i >= 0; --i)
            {
                if (tasks[i].getID() == a)
                {
                    db.doneTask(a);
                    tasks[i].setSuccess(true);
                }
            }

        }

        static void updateTask(int a)
        {
            Database db = new Database();
            for (int i = tasks.Count() - 1; i >= 0; --i)
            {
                if (tasks[i].getID() == a)
                {
                    Console.Write("Введите новую задачу: ");
                    string tsk = Console.ReadLine();
                    db.updTask(a, tsk);
                    tasks[i].editTask(tsk);
                }
            }
        }

        static bool searcTask(int a)
        {
            bool k = false;
            for (int i = tasks.Count() - 1; i >= 0; --i)
            {
                if (tasks[i].getID() == a)
                {
                    Console.WriteLine(tasks[i].getCategory());
                    if (tasks[i].getCategory() == 3)
                    {
                        k = true;
                    }
                    break;
                    break;
                }
                else
                {
                    k = false;
                }
            }
            return k;

        }

    }
}