namespace BookStore.Models
{
    public class CheckoutViewModel
    {
        public User User { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
