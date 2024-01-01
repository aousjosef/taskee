namespace taskee;
using TaskMethod;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {

        TaskMethodClass task = new TaskMethodClass();

        Console.WriteLine("\n ");
        Console.WriteLine($"**************** WELCOME TO **************** \n");
        Console.WriteLine("              T  A  S  K  E  E \n ");
        Console.WriteLine("    Please choose on of the options below: ");

        void showOptions()
        {
            Console.WriteLine("\n");
            Console.WriteLine("1. Show all tasks. ");

            Console.WriteLine("2. Add a new task. ");

            Console.WriteLine("3. Show tasks and order by date.");

            Console.WriteLine("4. Show tasks and order by urgency.");

            Console.WriteLine("5. Show task by ID. ");

            Console.WriteLine("6. Delete a task. ");

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
                        Console.WriteLine("Here are all tasks \n");
                        task.showAllTasks();
                        break;


                    case 2:
                        Console.WriteLine("You have choosen to add a new task.\n");
                        addNewClass();
                        break;

                    case 3:
                        Console.WriteLine("You have choosen to show all tasks orderd by date.\n");
                        task.showAllTasksOrderByDate();
                        break;

                    case 4:
                        Console.WriteLine("You have choosen to show all tasks orderd by urgency.\n");

                        task.showAllTaskOrderByUrgency();
                        break;

                    case 5:
                        Console.WriteLine("Please enter the ID of the task you want to see.\n");

                        task.showTaskById();
                        break;

                    case 6:
                        Console.WriteLine("Please enter the ID of the task you want to delete.\n");

                        task.deleteTaskById();
                        break;


                    case 0:

                        Console.WriteLine("Bye bye! :)\n");
                        Environment.Exit(0);
                        break;

                }


            }

        }
        //while loop ends





        void addNewClass()
        {
            string? taskNameInput;
            string? inputDateTime;
            DateTime taskDateTimeOutput;
            string? taskUrgencyLvlInput = "";
            string? taskDetailsInput;

            while (true)
            {
                Console.WriteLine("Please enter task name:  \n ");
                taskNameInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(taskNameInput))
                {
                    Console.WriteLine("Good! task name Saved \n ");
                    break;
                }
                else
                {
                    Console.WriteLine("Erorr! task name can not be empty!  \n ");

                }
            }




            while (true)
            {
                Console.WriteLine("Please enter task date and time (24h) in the following format *yyyy-MM-dd HH:mm* example: 2024-01-01 15:30  : \n ");
                inputDateTime = Console.ReadLine();
                string format = "yyyy-MM-dd HH:mm";
                CultureInfo culture = CultureInfo.InvariantCulture;
                DateTimeStyles styles = DateTimeStyles.None;

                //If date and time format is correct

                if (DateTime.TryParseExact(inputDateTime, format, culture, styles, out taskDateTimeOutput))
                {
                    Console.WriteLine("Good, date and time input accepted");
                    break;

                }


                else
                {
                    Console.WriteLine("Invalid input. Date or time format is incorrect. ");

                }
            }

            while (true)
            {

                Console.WriteLine("Kindly choose urgency level: ");
                Console.WriteLine("1. Low");
                Console.WriteLine("2. Medium");
                Console.WriteLine("3. Urgent");

                var option = Console.ReadLine();


                //Take in urgency level and check if true
                if (int.TryParse(option, out int num) && num >= 1 && num <= 3)
                {

                    switch (num)
                    {
                        //Case 1 visa all  notes
                        case 1:
                            taskUrgencyLvlInput = "Low";
                            break;


                        case 2:
                            taskUrgencyLvlInput = "Medium";
                            break;

                        case 3:
                            taskUrgencyLvlInput = "High";
                            break;
                    }

                    Console.WriteLine($"Good, you have selected urgency level {taskUrgencyLvlInput} \n");
                    break;
                }

                else
                {
                    Console.WriteLine("Erorr! Input invalid must be a number betwen 1 an 3");
                }

            }




            while (true)
            {
                Console.WriteLine("Please write content of the task \n ");
                taskDetailsInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(taskDetailsInput))
                {
                    TaskObjectClass taskObject = new TaskObjectClass()
                    {
                        TaskName = taskNameInput,
                        TaskDateTime = taskDateTimeOutput,
                        TaskUrgencyLevel = taskUrgencyLvlInput,
                        TaskDetails = taskDetailsInput

                    };

                    task.addNewTask(taskObject);
                    break;


                }
                else
                {
                    Console.WriteLine("Erorr! task content can not be empty!  \n ");

                }
            }

        }



        // task.addNewTask();

    }

    //main program ends
}

