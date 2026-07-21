Store store = new Store();

Customer customer = new Customer("Mithgal", "mithgal@gmail.com");

customer.CreateOrder(store);









public class Person
{
    public string name { get; set; } = "";
    public string email { get; set; } = "";

    public Person()
    {
        name = "unknown";
        email = "unknown";
    }

    public Person(string name, string email)
    {
        this.name = name;
        this.email = email;
    }
}

public class Customer : Person
{
    public Customer()
    {
    }

    public Customer(string name, string email)
        : base(name, email)
    {
    }

    public void CreateOrder(Store store)
    {
        Console.Write("Enter The number of products: ");
        int productCounts = int.Parse(Console.ReadLine());

        store.ShowProducts();

        Product[] products = new Product[productCounts];

        for (int i = 0; i < productCounts; i++)
        {
            Console.Write("Product number: ");
            int idx = int.Parse(Console.ReadLine());

            products[i] = store.products[idx - 1];

            Console.WriteLine();
        }

        Order order = new Order(this, products);

        store.orders[0] = order;

        Console.WriteLine("Order Created Successfully!");
        order.PrintOrder();
    }
}

public class Product
{
    public string name { get; set; }
    public double price { get; set; }

    public Product(string name, double price)
    {
        this.name = name;
        this.price = price;
    }
}

public class Order
{
    Customer customer;
    Product[] products;
    public double TotalPrice { get; private set; }

    public Order()
    {
        TotalPrice = 0;
    }

    public Order(Customer customer, Product[] products)
    {
        this.customer = customer;
        this.products = products;

        TotalPrice = 0;

        for (int i = 0; i < products.Length; i++)
        {
            TotalPrice += products[i].price;
        }
    }

    public void PrintOrder()
    {
        Console.WriteLine();
        Console.WriteLine("Customer: " + customer.name);
        Console.WriteLine();

        for (int i = 0; i < products.Length; i++)
        {
            Console.WriteLine(products[i].name + "    " + products[i].price);
        }

        Console.WriteLine();
        Console.WriteLine("Total Price = " + TotalPrice);
    }
}

public class Store
{
    public Product[] products { get; set; } = new Product[5];
    public Order[] orders { get; set; } = new Order[5];

    public Store()
    {
        products[0] = new Product("Laptop", 3500);
        products[1] = new Product("Mouse", 80);
        products[2] = new Product("Keyboard", 150);
        products[3] = new Product("Monitor", 900);
        products[4] = new Product("Headphones", 250);
    }

    public void ShowProducts()
    {
        Console.WriteLine("1- " + products[0].name + "      " + products[0].price);
        Console.WriteLine("2- " + products[1].name + "      " + products[1].price);
        Console.WriteLine("3- " + products[2].name + "      " + products[2].price);
        Console.WriteLine("4- " + products[3].name + "      " + products[3].price);
        Console.WriteLine("5- " + products[4].name + "      " + products[4].price);
    }
}


