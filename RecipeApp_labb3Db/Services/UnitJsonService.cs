using RecipeDbAccess.Models;
using System.IO;
using System.Reflection;
using System.Text.Json;


namespace RecipeApp_labb3Db.Presentation.Services
{
    public class UnitJsonService
    {
        //public List<Unit> LoadUnits()
        //{
        //    var assembly = Assembly.GetExecutingAssembly();
        //    var resourceName = "RecipeApp_labb3Db.Presentation.Resources";

        //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        var json = reader.ReadToEnd();
        //        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Unit>>(json);

        //    }
        //}

        public List<Unit> LoadUnits()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "units.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                List<string> unitNames = JsonSerializer.Deserialize<List<string>>(json);

                List<Unit> units = unitNames.Select(unitName => new Unit { UnitName = unitName, Amount = 0 }).ToList();
                return units;
                
            }
            else
            {
                throw new FileNotFoundException("The units file was not found.", filePath);
            }
        }

    }
}
