namespace WebApp.Entities
{
    public class Area
    {
        public int Id { get; set; }

        public string NameOfDistrict { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }


    }
}