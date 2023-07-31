using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Student
{
    public int RollNo { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Grade { get; set; }
}

class Program
{
    static void Main()
    {
        
        List<Student> students = ReadStudentsFromFile("students.txt");

       
        students.Sort((s1, s2) => string.Compare(s1.Name, s2.Name, StringComparison.OrdinalIgnoreCase));

        
        Console.WriteLine("Sorted Student Data:");
        DisplayStudents(students);

        
        Console.Write("\nEnter a student name to search: ");
        string searchName = Console.ReadLine();

        List<Student> searchResults = SearchStudentsByName(students, searchName);

        if (searchResults.Count > 0)
        {
            Console.WriteLine("\nSearch Results:");
            DisplayStudents(searchResults);
        }
        else
        {
            Console.WriteLine("\nNo matching student found.");
        }
    }

    static List<Student> ReadStudentsFromFile(string filePath)
    {
        List<Student> students = new List<Student>();
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] data = line.Split(',');

                    if (data.Length == 4)
                    {
                        int rollNo = int.Parse(data[0].Trim());
                        string name = data[1].Trim();
                        int age = int.Parse(data[2].Trim());
                        string grade = data[3].Trim();
                        students.Add(new Student { RollNo = rollNo, Name = name, Age = age, Grade = grade });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the file: " + ex.Message);
        }
        return students;
    }

    static List<Student> SearchStudentsByName(List<Student> students, string searchName)
    {
        return students.Where(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    static void DisplayStudents(List<Student> students)
    {
        foreach (Student student in students)
        {
            Console.WriteLine($"Roll No: {student.RollNo}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
        }
    }
}
