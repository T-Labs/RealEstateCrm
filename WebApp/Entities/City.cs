namespace WebApp.Entities
{
    public class City
    {
        public int Id { get; set; }

        public string  Name  { get; set; }

        public virtual District  Districts { get; set; }

        public int ClientId { get; set; }

        public virtual Client Clients { get; set; }
    }
}