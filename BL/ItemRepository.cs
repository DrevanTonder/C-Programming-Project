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

        public List<Item> Retrieve()
        {
            var csv = new CsvHelper.CsvReader(new StreamReader(File.OpenRead("c:/Stockfile/file.csv")));
            csv.Configuration.TypeConverterCache.AddConverter(typeof(bool), new MyBooleanConverter());
            csv.Configuration.PrepareHeaderForMatch = header => { return header.Replace(" ", string.Empty).Replace("Item", string.Empty); };

            return new List<Item>(csv.GetRecords<Item>());
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
    }
}
