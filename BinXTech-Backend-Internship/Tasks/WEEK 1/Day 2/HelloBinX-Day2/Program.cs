//Task 1

void PrintTypes()
{

    int a = 1;
    bool check = true;
    long bigNumber = 23232323;
    System.Console.WriteLine("Value type:");
    System.Console.WriteLine("A is a " + a.GetType());
    System.Console.WriteLine("check is a " + check.GetType());
    System.Console.WriteLine("bigNumber is a " + bigNumber.GetType());

    User user = new User();
    string s = "message";
    int[] numbers = { 1, 2, 3 };
    System.Console.WriteLine("reference type:");
    System.Console.WriteLine("user is a " + user.GetType());
    System.Console.WriteLine("s is a " + s.GetType());
    System.Console.WriteLine("numbers is a " + numbers.GetType());
}
PrintTypes();

// Task 2
void ValueVsReferenceCopy()
{
    /* 
        value type : it means store a value

        reference type: it means store the reference of the palce in memmorey where the object store
     */

    Console.WriteLine("Value type copy:");
    int a = 10;
    int b = a;
    Console.WriteLine("a=" + a);
    Console.WriteLine("b=" + b);
    // it will be a = 10 and b = 10
    // if the value of b changes to be 5, the value of a still equal 10
    b = 5;
    Console.WriteLine("a=" + a);
    Console.WriteLine("b=" + b);

    Console.WriteLine("Reference type copy:");

    User user1 = new User();
    user1.Name = "Mithgal";
    User user2 = user1;
    System.Console.WriteLine(user1.Name);
    System.Console.WriteLine(user2.Name);

    user2.Name = "Omer";
    System.Console.WriteLine(user1.Name);
    System.Console.WriteLine(user2.Name);

}
// ValueVsReferenceCopy();




//Task 3
string GradeClassifier(int mark)
{
    return mark switch
    {
        >= 90 => "A+",
        >= 80 => "A",
        >= 70 => "B",
        >= 50 => "Pass",
        _ => "Fail"
    };
}

//Task 3
string GetName()
{
    System.Console.WriteLine("Enter your name: ");
    string? name = Console.ReadLine();

    if (name is null)
    {
        System.Console.WriteLine("No name was entered.");
        return "";
    }
    return "Your name is:" + name;
}




class User
{
    public string? Name { get; set; }
}