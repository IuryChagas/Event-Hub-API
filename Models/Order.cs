namespace Event_Hub_API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Units { get; set; }
        public Event Event { get; set; }
    }
}