using WebApp.Entities;

namespace Data.Query
{
    public class CustomerByIdQuery : IQuery<Customer>
    {
        public int Id { get; }

        public CustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}