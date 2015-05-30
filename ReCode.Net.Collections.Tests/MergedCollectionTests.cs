using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReCode.Net.Collections.Tests
{
    public class MergedCollectionTests
    {
        [Fact]
        public void TestRetrieveItemsInSequence()
        {
            ICollection<int> first = new List<int>
            {
                5,
                10,
                15
            };

            ICollection<int> second = new List<int>
            {
                2,
                4,
                6
            };

            ICollection<int> third = new List<int>
            {
                9,
                18,
                27
            };

            MergedCollection<int> merged = new MergedCollection<int>(first, second, third);

            Assert.True(merged.Take(first.Count).SequenceEqual(first));

            Assert.True(merged.Skip(first.Count).Take(second.Count).SequenceEqual(second));

            Assert.True(merged.Skip(first.Count + second.Count).SequenceEqual(third));
        }

        [Theory]
        [InlineData(new[] { 5, 10, 15, 20 })]
        public void TestAddItems(params int[] items)
        {
            ICollection<int> first = new List<int>();

            ICollection<int> second = new List<int>
            {
                2,
                4,
                6,
                8
            };

            MergedCollection<int> merged = new MergedCollection<int>(first, second);

            Assert.True(merged.SequenceEqual(second));

            merged.AddRange(items);

            Assert.True(merged.SequenceEqual(first.Concat(second)));

            Assert.True(first.SequenceEqual(items));

            first.Clear();

            Assert.True(merged.SequenceEqual(second));
        }
    }
}
