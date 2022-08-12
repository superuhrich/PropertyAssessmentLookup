using System.Reflection.Emit;

namespace PropertyAssessmentLookup.Models; 

public class CsvModels {
    
    public class SalePrice {
        public string Address { get; set; }
        public int Bd { get; set; }
        public int FB { get; set; }
        public int HB { get; set; }
        public int SqFt { get; set; }
        public int Sold_Price { get; set; }
        
        public DateTime Sold_Date { get; set; }
    }
    
    
    
    public class AssessedPropertyValues {
        public AssessedPropertyValues(
            string address, 
            int bd, 
            int fb, 
            int hb, 
            int sqft, 
            int soldPrice, 
            DateTime soldDate, 
            string honestDoorPrice, 
            string cityAssessedPrice) {
            Address = address;
            Bd = bd;
            FB = fb;
            HB = hb;
            SqFt = sqft;
            Sold_Price = soldPrice;
            Sold_Date = soldDate;
            HonestDoorPrice = honestDoorPrice;
            CityAssessedPrice = cityAssessedPrice;

        }
        public string Address { get; set; }
        public int Bd { get; set; }
        public int FB { get; set; }
        public int HB { get; set; }
        public int SqFt { get; set; }
        public int Sold_Price { get; set; }
        public DateTime Sold_Date { get; set; }
        public string HonestDoorPrice { get; set; }
        public string CityAssessedPrice { get; set; }
    }
}