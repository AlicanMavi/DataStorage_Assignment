namespace Business.Models;

public class Customer
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;
    public object CustomerEmail { get; internal set; }
    public object CustomerPhoneNumber { get; internal set; }
}
