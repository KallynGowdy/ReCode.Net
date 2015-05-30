using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReCode.Net.Collections.Tests
{
    public class FilteredCollectionTests
    {
        [Theory]
        [InlineData(new object[] { "Cool", 458, 79843, 1, 4984.58f, "Ho!" }, new string[] { "Hello", "Oh My!" })]
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

        [Theory]
        [InlineData(3, new object[] { "Hello!", 50.2, 14.2f, 13, "Wow!", "Cool" })]
        public void TestFiltering(int expectedCount, params object[] objects)
        {
            ICollection<object> collection = objects.ToList();

            FilteredCollection<string, object> filtered = new FilteredCollection<string, object>(objects);

            Assert.Equal(expectedCount, filtered.Count);

            Assert.True(objects.OfType<string>().SequenceEqual(filtered));
        }

        [Theory]
        [InlineData(new object[] { 459, 16, 1.45f, "Oh My!", 14.2f, 12.897, "Some Other Stuff" }, new object[] { "Oh My!" })]
        public void TestContains(object[] objects, object[] contains)
        {
            ICollection<object> collection = objects.ToList();

            ICollection<string> filtered = new FilteredCollection<string, object>(collection);

            Assert.Equal(objects.OfType<string>().Count(), filtered.Count);

            foreach (object obj in contains)
            {
                Assert.True(filtered.Contains(obj), string.Format("The filtered collection does not contain: \"{0}\" when it should.", obj));
            }

            foreach (object obj in collection.Where(o => !(o is string)))
            {
                Assert.DoesNotContain(obj, filtered);
            }
        }
    }
}
