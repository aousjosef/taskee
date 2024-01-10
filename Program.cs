namespace taskee;
using TaskMethod;


class Program
{
    static void Main(string[] args)
    {

        TaskMethodClass task = new TaskMethodClass();

        Console.WriteLine("\n ");
        Console.WriteLine($"**************** WELCOME TO **************** \n");
        Console.WriteLine("              T  A  S  K  E  E \n ");
        Console.WriteLine("           By A O U S J O S E F \n ");
        Console.WriteLine("    Please choose on of the options below: ");

        void showOptions()
        {
            Console.WriteLine("\n");
            Console.WriteLine("1. Show all tasks. \n");

            Console.WriteLine("2. Add a new task. \n");

            Console.WriteLine("3. Show tasks and order by date.\n");

            Console.WriteLine("4. Show tasks and order by urgency.\n");

            Console.WriteLine("5. Show task by ID. \n");

            Console.WriteLine("6. Delete a task. \n");

            Console.WriteLine("7. Edit task by ID. \n");

            Console.WriteLine("0. Exit program. \n ");

        }

        // while loop som körs om varje gång för att visa om alternativen 
        while (true)
        {
            showOptions();
            var option = Console.ReadLine();
            Console.CursorVisible = true;

            if (int.TryParse(option, out int num))
            {

                switch (num)
                {
                    //Case 1 visa all  notes
                    case 1:

                        task.showAllTasks();
                        break;


                    case 2:

                        task.addNewTask();
                        break;

                    case 3:

                        task.showAllTasksOrderByDate();
                        break;

                    case 4:


                        task.showAllTaskOrderByUrgency();
                        break;

                    case 5:


                        task.showTaskById();
                        break;

                    case 6:

                        task.deleteTaskById();
                        break;


                    case 7:
                        task.editTaskById();
                        break;

                    case 0:

                        Console.WriteLine("Bye bye! :)\n");
                        Environment.Exit(0);
                        break;

                }


            }

        }
        //while loop ends









        // task.addNewTask();

    }

    //main program ends
}

