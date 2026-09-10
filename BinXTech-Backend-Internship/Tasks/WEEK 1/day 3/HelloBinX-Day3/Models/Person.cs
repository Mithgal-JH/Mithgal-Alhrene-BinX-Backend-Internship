namespace HelloBinX_Day3.Models;

public class Person
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";

    private int _salary;

    public int Salary
    {
        get { return _salary; }
        set
        {
            if (value < 0)
                _salary = 0;
            else _salary = value;
        }
    }
    private int _age;
    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0) _age = 0;
            else _age = value;
        }
    }

    public Person()
    {
        Name = "unknown";
        Email = "unknown";
        Age = 0;
    }

    public Person(string name, string email, int age, int salary)
    {
        this.Name = name;
        this.Email = email;
        this.Age = age;
        this.Salary = salary;
    }
}