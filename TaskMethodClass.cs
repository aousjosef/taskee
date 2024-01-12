using System.Text.Json;
using System.Globalization;



namespace TaskMethod;


//Class som skapar object för vår JSON
public class TaskObjectClass
{
    public string? TaskName { get; set; }

    public DateTime TaskDateTime { get; set; }
    public string? TaskUrgencyLevel { get; set; }

    public string? TaskDetails { get; set; }
}

class TaskMethodClass
{

    // Skapa en lista med typen objekt getter och setters. Listan är av typen objekt som kommer från klassen NoteClass
    public List<TaskObjectClass>? Tasks { get; set; }

    string fileName = @"taskdblist.json";

    string? jsonContent;


    // konstruktor som intierar metoden converJsonFileToList
    public TaskMethodClass()
    {
        converJsonFileToList();
    }

    //Metod för att kolla om json file existerar för att sedan skapa/konvertera den till en lista av typen objekt
    public void converJsonFileToList()
    {

        //check if file exist
        if (!File.Exists(fileName))
        {
            // If the file doesn't exist, create a new empty JSON file
            File.WriteAllText(fileName, "[]");

            //Create empty list
            Tasks = new List<TaskObjectClass>();
        }

        else

        {
            //Read all content of json file. put into string.

            jsonContent = File.ReadAllText(fileName);

            // Deserialize the JSON content into a list of TaskeObject class
            Tasks = JsonSerializer.Deserialize<List<TaskObjectClass>>(jsonContent);

        }


    }


    //Lägg till ny task
    public void addNewTask()
    {
        string? taskNameInput;
        string? inputDateTime;
        DateTime taskDateTimeOutput;
        string? taskUrgencyLvlInput = "";
        string? taskDetailsInput;

        Console.WriteLine("You have choosen to add a new task.\n");


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
                        taskUrgencyLvlInput = "Urgent";
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


                Tasks.Add(taskObject);
                string jsonString = JsonSerializer.Serialize(Tasks);

                File.WriteAllText(fileName, jsonString);

                Console.WriteLine("Great, a new task has been added! \n");
                break;


            }
            else
            {
                Console.WriteLine("Erorr! task content can not be empty!  \n ");

            }
        }

    }



    public void showAllTasks()
    {
        Console.WriteLine("Here are all tasks \n");

        int indexOfNote = 0;

        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("ID | Task Name                           | Date and Time           | Urgency  | Details                                     |");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
        foreach (var t in Tasks)
        {
            Console.WriteLine($"|{indexOfNote,-2}| {TruncateString(t.TaskName, 35),-35} | {t.TaskDateTime,-23} | {t.TaskUrgencyLevel,-8} | {TruncateString(t.TaskDetails, 43),-43} |");

            indexOfNote++;
        }
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
    }


    public void showAllTasksOrderByDate()
    {
        Console.WriteLine("You have choosen to show all tasks orderd by date.\n");

        var orderedTasksDescending = Tasks.OrderByDescending(task => task.TaskDateTime);

        presentListData(orderedTasksDescending);

    }

    public void showAllTaskOrderByUrgency()
    {
        Console.WriteLine("You have choosen to show all tasks orderd by urgency.\n");

        var orderedTasksByUrgency = Tasks.OrderByDescending(task => task.TaskUrgencyLevel);
        presentListData(orderedTasksByUrgency);

    }


    public void showTaskById()
    {

        Console.WriteLine("Please enter the ID of the task you want to see.\n");

        var taskIdinput = Console.ReadLine();

        if (int.TryParse(taskIdinput, out int taskId) && taskId >= 0 && taskId <= Tasks.Count - 1)
        {
            var taskById = Tasks[taskId];

            Console.WriteLine($"({taskId}) - {taskById.TaskName} \n");
            Console.WriteLine($"Date: {taskById.TaskDateTime} \n");
            Console.WriteLine($"Level of urgency: {taskById.TaskUrgencyLevel} \n");
            Console.WriteLine($"Details: {taskById.TaskDetails} \n \n");

        }

        else
        {
            Console.WriteLine("Invalid task ID. Please provide a valid ID between 0 and " + (Tasks.Count - 1));
            return;
        }

    }



    public void editTaskById()
    {
        Console.WriteLine("Please enter the ID of the task you want to edit: \n");
        var taskIdInput = Console.ReadLine();

        if (int.TryParse(taskIdInput, out int taskId) && taskId >= 0 && taskId <= Tasks.Count - 1)
        {
          
            Console.WriteLine($"Editing Task with ID: {taskId}");
            Console.WriteLine("Enter new Task Name (press Enter to keep existing): \n");
            var newTaskName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTaskName))
            {
                Tasks[taskId].TaskName = newTaskName;
            }

            Console.WriteLine("Enter new Date and Time (press Enter to keep existing):");
            Console.WriteLine("Please enter task date and time (24h) in the following format *yyyy-MM-dd HH:mm* example: 2024-01-01 15:30  : \n ");
            var newDateTimeInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDateTimeInput) && DateTime.TryParse(newDateTimeInput, out DateTime newDateTime))
            {
                Tasks[taskId].TaskDateTime = newDateTime;
            }

            Console.WriteLine("Enter new Urgency Level (press Enter to keep existing): ");

            Console.WriteLine("Kindly choose urgency level: ");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Urgent \n");
            var newUrgencyLevel = Console.ReadLine();
            if (int.TryParse(newUrgencyLevel, out int num) && num >= 1 && num <= 3)
            {

                switch (num)
                {
                    //Case 1 visa all  notes
                    case 1:
                        Tasks[taskId].TaskUrgencyLevel = "Low";
                        break;


                    case 2:
                        Tasks[taskId].TaskUrgencyLevel = "Medium";
                        break;

                    case 3:
                        Tasks[taskId].TaskUrgencyLevel = "Urgent";
                        break;
                }


            }

            Console.WriteLine("Enter new Task Details (press Enter to keep existing): \n");
            var newTaskDetails = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTaskDetails))
            {
                Tasks[taskId].TaskDetails = newTaskDetails;
            }

            // Save the updated list to the file
            string jsonString = JsonSerializer.Serialize(Tasks);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine($"Task with ID {taskId} successfully edited! \n");
        }
        else
        {
            Console.WriteLine($"Error! Please provide a valid ID between 0 and {Tasks.Count - 1} \n");
        }
    }


    public void deleteTaskById()
    {

        Console.WriteLine("Please enter the ID of the task you want to delete.\n");

        var taskIdinput = Console.ReadLine();

        if (int.TryParse(taskIdinput, out int taskId) && taskId >= 0 && taskId <= Tasks.Count - 1)
        {
            Tasks.RemoveAt(taskId);
            string jsonString = JsonSerializer.Serialize(Tasks);

            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Task with index {taskId} succesfully removed!");

        }

        else
        {
            Console.WriteLine($"Erorr! value must be a a number between 0 and {Tasks.Count - 1}");
            return;
        }

    }






    //IEnumerable tar emot båda lista och IEnumerable
    void presentListData(IEnumerable<TaskObjectClass> tasks)
    {
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("| Task Name                           | Date and Time           | Urgency  | Details                                      |");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");

        foreach (var t in tasks)
        {
            Console.WriteLine($"| {TruncateString(t.TaskName, 35),-35} | {t.TaskDateTime,-23} | {t.TaskUrgencyLevel,-8} | {TruncateString(t.TaskDetails, 43),-43} |");
        }

        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
    }



    // Helper method to truncate long strings for display
    string TruncateString(string input, int maxLength)
    {
        // if longer than max
        if (input.Length > maxLength)
        {
            return input.Substring(0, maxLength - 3) + "...";
        }

        else
        {
            return input;
        }

    }

}