// Copyright 2014 Kallyn Gowdy
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReCode.Net.Collections
{
    /// <summary>
    /// Defines a class for a collection that contains objects from multiple mutable collections.
    /// </summary>
    public class MergedCollection<T> : 
        ICollection<T>
    {
        /// <summary>
        /// Gets the list of collections that are contained in this collection.
        /// </summary>
        public IList<ICollection<T>> Collections
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection that items are added to.
        /// </summary>
        public ICollection<T> MainCollection
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MergedCollection{T}"/> class.
        /// </summary>
        /// <param name="collections">The list of collections to merge into one.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the given list of collections is null.</exception>
        public MergedCollection(params ICollection<T>[] collections)
        {
            if (collections == null)
            {
                throw new ArgumentNullException("collections");
            }
            this.Collections = new List<ICollection<T>>(collections);
            MainCollection = this.Collections.First();
        }

        /// <summary>
        /// Adds the given list of items to the main collection.
        /// </summary>
        /// <param name="items">The items to add.</param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Adds an item to the main <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the main <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(T item)
        {
            MainCollection.Add(item);
        }

        /// <summary>
        /// Removes all items from each of the collections merged into this collection.
        /// </summary>
        public void Clear()
        {
            foreach (ICollection<T> c in Collections)
            {
                c.Clear();
            }
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            foreach (ICollection<T> c in Collections)
            {
                if (c.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public int Count
        {
            get { return Collections.Sum(c => c.Count); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(T item)
        {
            foreach (ICollection<T> c in Collections)
            {
                if (c.Remove(item))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collections.SelectMany(c => c).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
