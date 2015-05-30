using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ReCode.Tests
{
    [TestFixture]
    public class FilteredCollectionTests
    {
        [TestCase(new object[] { "Cool", 458, 79843, 1, 4984.58f, "Ho!" }, new string[] { "Hello", "Oh My!" })]
        public void TestAdd(object[] objects, string[] newObjects)
        {
            ICollection<object> collection = objects.ToList();

            FilteredCollection<string, object> filtered = new FilteredCollection<string, object>(collection);

            foreach (string str in newObjects)
            {
                filtered.Add(str);
            }

            Assert.True(objects.Concat(newObjects).SequenceEqual(collection));
        }

        [TestCase(3, "Hello!", 50.2, 14.2f, 13, "Wow!", "Cool")]
        public void TestFiltering(int expectedCount, params object[] objects)
        {
            ICollection<object> collection = objects.ToList();

            FilteredCollection<string, object> filtered = new FilteredCollection<string, object>(objects);

            Assert.AreEqual(expectedCount, filtered.Count);

            Assert.True(objects.OfType<string>().SequenceEqual(filtered));
        }

        [TestCase(new object[] { 459, 16, 1.45f, "Oh My!", 14.2f, 12.897, "Some Other Stuff" }, new object[] { "Oh My!" })]
        public void TestContains(object[] objects, object[] contains)
        {
            ICollection<object> collection = objects.ToList();

            ICollection<string> filtered = new FilteredCollection<string, object>(collection);

            Assert.AreEqual(objects.OfType<string>().Count(), filtered.Count);

            foreach (object obj in contains)
            {
                Assert.True(filtered.Contains(obj), string.Format("The filtered collection does not contain: \"{0}\" when it should.", obj));
            }

            foreach (object obj in collection.Where(o => !(o is string)))
            {
                Assert.That(filtered, Is.Not.Contains(obj));
            }
        }
    }
}
