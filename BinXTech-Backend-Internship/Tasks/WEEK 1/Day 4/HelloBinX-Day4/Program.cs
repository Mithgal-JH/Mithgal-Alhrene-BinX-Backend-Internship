using HelloBinX_Day3.Models;


Console.WriteLine(@"
==============================
        Day 4 Tasks
==============================

1. Display Persons

2. LINQ Queries
   - Filter
   - Projection
   - Aggregation

3. Async Method

4. Exception Handling

5. Exit

Choice:
");

List<Person> Persons = new()
                                    {
                                        new Person("Mithgal", "mithgaljamal@gmail.com", 20, 1200),
                                        new Person("Omer", "omer@gmail.com", 40, 2500),
                                        new Person("Ahmad", "ahmad@gmail.com", 22, 1500),
                                        new Person("Sara", "sara@gmail.com", 19, 1000),
                                        new Person("Lina", "lina@gmail.com", 30, 3200),
                                        new Person("Ali", "ali@gmail.com", 27, 2800),
                                        new Person("Yousef", "yousef@gmail.com", 35, 4000),
                                        new Person("Noor", "noor@gmail.com", 18, 900)
                                    };

try
{
    int Choice = int.Parse(Console.ReadLine()!);
    switch (Choice)
    {
        case 1:
            // 1. Create a List of at least 8 objects from your Day 3 domain model with varied property values
            foreach (var p in Persons)
            {
                System.Console.WriteLine(p.Name);
            }
            break;
        case 2:

            //2. Write 3 LINQ queries against the list: one filter, one projection, and one aggregation (Count, Sum, or Average).

            var PersonsHigherThan1500 = Persons.Where(p => p.Salary > 1500);
            System.Console.WriteLine("persons with salary higher than 1500:");
            foreach (var p in PersonsHigherThan1500)
            {
                System.Console.WriteLine(p.Name);
            }

            var EmailsList = Persons.Select(p => p.Email);
            foreach (var email in EmailsList)
            {
                System.Console.WriteLine(email);
            }
            var PersonsCount = Persons.Count();
            System.Console.WriteLine($"Persons count: {PersonsCount}");
            break;
        case 3:
            //3. Write an async method that simulates an I/O delay (Task.Delay) and returns a result, then await it from Main.

            string result = await loading();
            System.Console.WriteLine(result);

            static async Task<string> loading()
            {
                System.Console.WriteLine("loading 3000 ms.....");
                await Task.Delay(3000);
                return "3000 MS Delay";
            }
            break;
        case 4:
            //4. Wrap a risky operation (e.g. parsing user input) in a try/catch that catches a specific exception type and handles it meaningfully.
            System.Console.Write("Enter your Salary:");
            try
            {
                int salary = int.Parse(Console.ReadLine()!);

            }
            catch (FormatException)
            {

                Console.WriteLine("Invalid input! Please enter a valid number.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            break;
        case 5:
            System.Console.WriteLine("Good By 🙋‍♂️");
            break;
        default:
            System.Console.WriteLine("Please enter valid number.");
            break;
    }

}
catch (FormatException)
{

    Console.WriteLine("Invalid input! Please enter a valid number.");
}
catch (Exception ex)
{
    System.Console.WriteLine(ex.Message);
}











