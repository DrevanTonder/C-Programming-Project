using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BL.Tests
{
    [TestClass()]
    public class ItemRepositoryTests
    {

        [TestMethod()]
        public void RetrieveTest()
        {
            //Assign
            FileStream stream = File.OpenRead("c:/stockfile/importtest.csv");
            var expected = new Item()
            {
                Code = "A0001",
                Description = "Horse on Wheels",
                CurrentCount = 5,
                OnOrder = false
            };

            //Act
            var items = (List<Item>)ItemRepository.Instance.Retrieve(stream);

            //Assert
            Assert.AreEqual(expected.Code, items[0].Code);
            Assert.AreEqual(expected.Description, items[0].Description);
            Assert.AreEqual(expected.CurrentCount, items[0].CurrentCount);
            Assert.AreEqual(expected.OnOrder, items[0].OnOrder);
        }

        [TestMethod()]
        public void SaveTest()
        {
            //Assign
            var read = "c:/stockfile/importtest.csv";
            var write = "c:/stockfile/exporttest.csv";
            
            var expected = new Item()
            {
                Code = "A0001",
                Description = "Horse on Wheels",
                CurrentCount = 5,
                OnOrder = false
            };

            //Act
            using (FileStream stream = File.OpenRead(read))
            {
                ItemRepository.Instance.Retrieve(stream);
            }

            using (FileStream stream = File.OpenWrite(write))
            {
                ItemRepository.Instance.Save(stream);
            }

            List<Item> items;
            using (FileStream stream = File.OpenRead(write))
            {
                items = (List<Item>)ItemRepository.Instance.Retrieve(stream);
            }

            //Assert
            Assert.AreEqual(expected.Code, items[0].Code);
            Assert.AreEqual(expected.Description, items[0].Description);
            Assert.AreEqual(expected.CurrentCount, items[0].CurrentCount);
            Assert.AreEqual(expected.OnOrder, items[0].OnOrder);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            //Assign
            var read = "c:/stockfile/importtest.csv";
            var write = "c:/stockfile/exporttest.csv";

            var expected = new Item()
            {
                Code = "A0001",
                Description = "Horse on Wheels",
                CurrentCount = 56,
                OnOrder = false
            };

            //Act

            using (FileStream stream = File.OpenRead(read))
            {
                ItemRepository.Instance.Retrieve(stream);
            }

            ItemRepository.Instance.Update("A0001", 56);
            using (FileStream stream = File.OpenWrite(write))
            {
                ItemRepository.Instance.Save(stream);
            }

            List<Item> items;
            using (FileStream stream = File.OpenRead(write))
            {
                items = (List<Item>)ItemRepository.Instance.Retrieve(stream);
            }

            //Assert
            Assert.AreEqual(expected.Code, items[0].Code);
            Assert.AreEqual(expected.Description, items[0].Description);
            Assert.AreEqual(expected.CurrentCount, items[0].CurrentCount);
            Assert.AreEqual(expected.OnOrder, items[0].OnOrder);
        }
    }
}