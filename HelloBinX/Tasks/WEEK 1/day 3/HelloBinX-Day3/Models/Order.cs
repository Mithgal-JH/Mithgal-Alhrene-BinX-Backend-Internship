public class Order
{
    Customer customer;
    Product[] products;
    public double TotalPrice { get; private set; }

    public Order()
    {
        this.customer= new Customer();
        this.products= new Product[1];
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
        Console.WriteLine("Customer: " + customer.Name);
        Console.WriteLine();

        for (int i = 0; i < products.Length; i++)
        {
            Console.WriteLine(products[i].name + "    " + products[i].price);
        }

        Console.WriteLine();
        Console.WriteLine("Total Price = " + TotalPrice);
    }
}
