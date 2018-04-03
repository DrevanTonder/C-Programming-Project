using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class ItemRepository
    {
        private static ItemRepository instance;

        private ItemRepository() { }


        public static ItemRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemRepository();
                }
                return instance;
            }
        }

        public IEnumerable<Item> Retrieve()
        {
            using (FileStream fileStream = File.OpenRead("c:/Stockfile/file.csv"))
            using (var reader = new StreamReader(fileStream))
            using (var csv = new CsvHelper.CsvReader(reader))
            {
                csv.Configuration.TypeConverterCache.AddConverter(typeof(bool), new MyBooleanConverter());
                csv.Configuration.RegisterClassMap<ItemMap>();
                return new List<Item>(csv.GetRecords<Item>());
            }
        }

        public void Save(IEnumerable<Item> items)
        {
            using (FileStream fileStream = File.OpenWrite("c:/Stockfile/export.csv"))
            using (var writer = new StreamWriter(fileStream))
            using (var csv = new CsvHelper.CsvWriter(writer))
            {
                csv.Configuration.TypeConverterCache.AddConverter(typeof(bool), new MyBooleanConverter());
                csv.Configuration.RegisterClassMap<ItemMap>();
                csv.WriteRecords(items);
            }
            
        }

        public class MyBooleanConverter : CsvHelper.TypeConversion.DefaultTypeConverter
        {
            public override string ConvertToString(object value, CsvHelper.IWriterRow row, CsvHelper.Configuration.MemberMapData memberMapData)
            {
                if (value == null)
                {
                    return string.Empty;
                }

                var boolValue = (bool)value;

                return boolValue ? "Yes" : "No";
            }

            public override object ConvertFromString(string text, CsvHelper.IReaderRow row, CsvHelper.Configuration.MemberMapData memberMapData)
            {
                if (text == null)
                {
                    return string.Empty;
                }

                return text.ToLower() == "yes" ? true : false;
            }
        }

        public sealed class ItemMap : CsvHelper.Configuration.ClassMap<Item>
        {
            public ItemMap()
            {
                Map(m => m.Code).Name("Item Code");
                Map(m => m.Description).Name("Item Description");
                Map(m => m.CurrentCount).Name("Current Count");
                Map(m => m.OnOrder).Name("On Order");
            }
        }
    }
}
