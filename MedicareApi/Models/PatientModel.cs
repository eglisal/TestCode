namespace MedicareApi.Models
{
    public class Patient
    {
        public required string FullName { get; set; }
        public required string VisitedCities { get; set; }
        public char Category { get; set; }
    }
}
