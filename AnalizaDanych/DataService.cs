using System.Text.Json;
using System.IO;
using AnalizaDanych.Model;

public class DataService
{
    public Root LoadData(string path)
    {
        var jsonString = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Root>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}