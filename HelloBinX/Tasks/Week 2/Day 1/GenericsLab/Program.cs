using HelloBinX_Day3.Models;

Repository<Person> personRepo = new Repository<Person>();
Repository<Customer> customerRepo = new Repository<Customer>();



personRepo.Add(new Person());
personRepo.Add(new Person());
personRepo.Add(new Person());
personRepo.Add(new Person());

var persons = personRepo.GetAll();


System.Console.WriteLine("All persons:");
foreach (var person in persons)
{
    Console.WriteLine(person.Name);
}

// persons.Add(new Person()); Error: Cannot add items to IReadOnlyList