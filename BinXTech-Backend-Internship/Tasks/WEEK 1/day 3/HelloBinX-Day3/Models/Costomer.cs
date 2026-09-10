using HelloBinX_Day3.Models;
public class Customer : Person
{
    public Customer()
    {
    }

    public Customer(string name, string email,int age,int salary)
        : base(name, email,age,salary)
    {
    }

    public void CreateOrder(Store store)
    {
        Console.Write("Enter The number of products: ");
        int productCounts = int.Parse(Console.ReadLine()!);

        store.ShowProducts();

        Product[] products = new Product[productCounts];

        for (int i = 0; i < productCounts; i++)
        {
            Console.Write("Product number: ");
            int idx = int.Parse(Console.ReadLine()!);

            products[i] = store.products[idx - 1];

            Console.WriteLine();
        }

        Order order = new Order(this, products);

        store.orders[0] = order;

        Console.WriteLine("Order Created Successfully!");
        order.PrintOrder();
    }
}




