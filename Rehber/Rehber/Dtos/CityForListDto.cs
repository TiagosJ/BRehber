namespace Rehber.Dtos
{
    public class CityForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public int UserId { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public byte[] QrCode { get; set; }
    }
}
