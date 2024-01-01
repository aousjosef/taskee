using System.Text.Json;

namespace TaskMethod;


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

        foreach (var t in Tasks)
        {
            Console.WriteLine($"({indexOfNote}) - {t.TaskName} \n");
            Console.WriteLine($"Date: {t.TaskDateTime} \n");
            Console.WriteLine($"Level of urgency: {t.TaskUrgencyLevel} \n");
            Console.WriteLine($"Details: {t.TaskDetails} \n \n");

            indexOfNote++;
        }
    }


}