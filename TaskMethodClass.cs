using System.Text.Json;

namespace TaskMethodClass;


//Class som skapar object för vår JSON
public class TaskObjectClass
{

    //Enum med spcifika värden
    public enum TaskCategory
    {
        Low,
        Medium,
        High
    }

    public string? TaskName { get; set; }
    public string? TaskContent { get; set; }
    public DateTime TaskDateTime { get; set; }
    public TaskCategory TaskUrgencyLevel { get; set; }


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

}