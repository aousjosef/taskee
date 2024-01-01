using System.Text.Json;

namespace TaskMethod;
using System.Collections.Generic;



//Class som skapar object för vår JSON
public class TaskObjectClass
{

    //Enum med spcifika värden


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

            // Deserialize the JSON content into a list of NoteClass objects
            Tasks = JsonSerializer.Deserialize<List<TaskObjectClass>>(jsonContent);

        }


    }


    //Lägg till ny task
    public void addNewTask(TaskObjectClass task)
    {

        Tasks.Add(task);

        string jsonString = JsonSerializer.Serialize(Tasks);

        File.WriteAllText(fileName, jsonString);

        Console.WriteLine("Great, a new task has been added! \n");

    }



    public void showAllTasks()
    {

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

        var orderedTasksDescending = Tasks.OrderByDescending(task => task.TaskDateTime);

        presentListData(orderedTasksDescending);


    }

    public void showAllTaskOrderByUrgency()
    {
        var orderedTasksByUrgency = Tasks.OrderByDescending(task => task.TaskUrgencyLevel);
        presentListData(orderedTasksByUrgency);

    }


    public void showTaskById()
    {
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

    public void deleteTaskById()
    {
        var taskIdinput = Console.ReadLine();

        if (int.TryParse(taskIdinput, out int taskId) && taskId >= 0 && taskId <= Tasks.Count - 1)
        {
            Tasks.RemoveAt(taskId);
            string jsonString = JsonSerializer.Serialize(Tasks);

            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"note with index {taskId} succesfully removed!");

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
    static string TruncateString(string input, int maxLength)
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