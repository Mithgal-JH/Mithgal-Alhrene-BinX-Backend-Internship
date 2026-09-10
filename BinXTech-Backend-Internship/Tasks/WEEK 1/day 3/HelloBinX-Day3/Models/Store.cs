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
