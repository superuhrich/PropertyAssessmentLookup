using System.Globalization;
using CsvHelper.Configuration;
using System.Windows.Input;

namespace PropertyAssessmentLookup; 

using CsvHelper;

public static class CsvUtils {
   public static IList<Models.CsvModels.SalePrice> GetAssessmentData() {

      Console.WriteLine("Enter path for sale csv");
      var filePath = @"C:\Repos\AssessmentValues.csv";
      
      using (var reader = new StreamReader(filePath))
      using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
         return csv.GetRecords<Models.CsvModels.SalePrice>().ToList();
      }

   }

   public static void WriteAssessmentData(IList<Models.CsvModels.AssessedPropertyValues> assessedPropertyValuesList) {
      using (var writer = new StreamWriter(@"C:\Repos\CompletedAssessmentValues.csv"))
      using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
         csv.WriteRecords(assessedPropertyValuesList);
      }
   }
}