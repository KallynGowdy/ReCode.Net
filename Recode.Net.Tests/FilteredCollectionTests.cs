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
        [Test]
        public void TestFiltering()
        {
            ICollection<object> objects = new List<object>
            {
                new object(),
                "Hello!",
                50.2,
                14.2f,
                13,
                "Wow!",
                "Cool"
            };

            FilteredCollection<string, object> filtered = new FilteredCollection<string, object>(objects);

            Assert.AreEqual(3, filtered.Count);

            Assert.AreEqual("Hello!", filtered.First());
            Assert.AreEqual("Wow!", filtered.ElementAt(1));
            Assert.AreEqual("Cool", filtered.Last());
        }
    }
}
