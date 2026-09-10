



//1. Create two related collections (e.g. Customers and Orders) with at least 6 items each, sharing a foreign key
using System.IO.Pipelines;

List<Customer> customers = new List<Customer>
{
    new Customer { Id = 1, Name = "Ahmed", Email = "ahmed@gmail.com" },
    new Customer { Id = 2, Name = "Ali", Email = "ali@gmail.com" },
    new Customer { Id = 3, Name = "Sara", Email = "sara@gmail.com" },
    new Customer { Id = 4, Name = "Omar", Email = "omar@gmail.com" },
    new Customer { Id = 5, Name = "Lina", Email = "lina@gmail.com" },
    new Customer { Id = 6, Name = "Noor", Email = "noor@gmail.com" }
};

List<Order> orders = new List<Order>
{
    new Order { Id = 1, CustomerId = 1, ProductName = "Laptop", Price = 1200 },
    new Order { Id = 2, CustomerId = 2, ProductName = "Mouse", Price = 25 },
    new Order { Id = 3, CustomerId = 1, ProductName = "Keyboard", Price = 70 },
    new Order { Id = 4, CustomerId = 3, ProductName = "Monitor", Price = 300 },
    new Order { Id = 5, CustomerId = 5, ProductName = "Headset", Price = 80 },
    new Order { Id = 6, CustomerId = 4, ProductName = "Phone", Price = 900 }
};

//2. Write a GroupBy query summarizing total order amount per customer
System.Console.WriteLine("\n\n\nTASK 2: Write a GroupBy query summarizing total order amount per customer");
var totals  = orders.GroupBy(o=> o.CustomerId).Select(g=> new
{
    customerID=g.Key,
    totalPrice=g.Sum(o=>o.Price)
});
System.Console.WriteLine("CustomerID    Total Amount");
foreach (var pair in totals)
{
    System.Console.WriteLine($"{pair.customerID}    {pair.totalPrice}");
}


//3. Write a Join query combining customer names with their order amounts
System.Console.WriteLine("\n\n\nTASK 3: Write a Join query combining customer names with their order amounts");
var result =orders.Join(
    customers,
    order=>order.CustomerId,
    customer=>customer.Id,
    (order,customer) => new
    {
        customer.Name,
        order.Price
    }
).GroupBy(o=>o.Name).Select(g=>
    new
    {
       CustomerName=g.Key,
        TotalAmount=g.Sum(o=>o.Price)
    });

foreach(var item in result)
{
    System.Console.WriteLine($"{item.CustomerName}     {item.TotalAmount}");
}


//4. Write a SelectMany query flattening a nested collection (e.g. every order line item across every order) into one sequence.
System.Console.WriteLine("\n\n\nTASK 4: Write a SelectMany query flattening a nested collection (e.g. every order line item across every order) into one sequence");
var OrderPerCustomer = orders.GroupBy(o=>o.CustomerId); // just for example (nested collection)

var products= OrderPerCustomer.SelectMany(g=>g.Select(o=>o.ProductName));

foreach (var item in products)
{
    System.Console.WriteLine(item);
    
}



//5. Demonstrate deferred execution by modifying the source collection after defining a query but before enumerating it, and explain the result.
System.Console.WriteLine("\n\n\nTASK 5: Demonstrate deferred execution by modifying the source collection after defining a query but before enumerating it, and explain the result");
// define a Query
var expensiveOrders = orders.Where(o => o.Price > 100);

// add new order
orders.Add(
    new Order { Id = 7, CustomerId = 2, ProductName = "NewOrder", Price = 101 }
);

// enumerating the query
foreach (var order in expensiveOrders)
{
    Console.WriteLine($"{order.ProductName} - {order.Price}");
}
// Deferred execution:
// The query reflects the latest state of the source collection
// because it is executed only during enumeration.