using CsvHelper;
using System.Globalization;

namespace Holiday_Chat_Messages.Data
{
    public static class Destinatons
    {
        public static List<Destination>? Data { get; private set; }

        public static bool Load()
        {
            using (var reader = new StreamReader("destinations.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var destinations = csv.GetRecords<Destination>().ToList();

                if (destinations == null || destinations.Count == 0)
                {
                    return false;
                }

                Data = destinations;

                return true;
            }
        }
    }
}
