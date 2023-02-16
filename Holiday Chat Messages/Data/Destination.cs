namespace Holiday_Chat_Messages.Data
{
    public class Destination
    {
        public int HolidayReference { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public int StarRating { get; set; }
        public string TempRating { get; set; }
        public string Location { get; set; }
        public int PricePerPerNight { get; set; }

        public override string ToString()
        {
            return @$"{HotelName},
                {City},
                {Country},

                Star Rating: {StarRating}
                Price per night: £{PricePerPerNight}";
        }
    }
}
