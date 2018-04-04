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
        private Dictionary<string, Item> items;

        private static ItemRepository instance;

        private ItemRepository() {
            items = new Dictionary<string, Item>();
        }

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

        public IEnumerable<Item> Retrieve(Stream stream)
        {
            List<Item> itemList;

            using (var reader = new StreamReader(stream))
            using (var csv = new CsvHelper.CsvReader(reader))
            {
                csv.Configuration.TypeConverterCache.AddConverter(typeof(bool), new MyBooleanConverter());
                csv.Configuration.RegisterClassMap<ItemMap>();
                try
                {
                    itemList = new List<Item>(csv.GetRecords<Item>());
                }
                catch (CsvHelper.MissingFieldException e)
                {
                    throw new ArgumentException("CSV Source has incomplete items", e);
                }
                
            }
            
            foreach(var item in itemList)
            {
                items[item.Code] = item;
            }

            return itemList;
        }

        public void Save(Stream stream)
        {
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvHelper.CsvWriter(writer))
            {
                csv.Configuration.TypeConverterCache.AddConverter(typeof(bool), new MyBooleanConverter());
                csv.Configuration.RegisterClassMap<ItemMap>();
                csv.WriteRecords(items.Values);
            }
        }

        public void Update(string itemCode, int CurrentCount)
        {
            items[itemCode].CurrentCount = CurrentCount;
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
