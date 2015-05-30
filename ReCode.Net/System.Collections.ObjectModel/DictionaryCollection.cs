using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.ObjectModel
{
    /// <summary>
    /// Defines a class that wraps a collection as a dictionary.
    /// </summary>
    public class DictionaryCollection<TKey, TValue> : 
        IDictionary<TKey, TValue>,
        IEnumerable<TValue>,
        ICollection<TValue>
    {
        ICollection<TValue> internalCollection;

        /// <summary>
        /// Creates a new <see cref="DictionaryCollection"/> object that represents a collection of values.
        /// </summary>
        /// <param name="keySelector">A function that, given a value, returns a key that represents the value.</param>
        /// <param name="elements">The list of elements to represent.</param>
        public DictionaryCollection(Func<TValue, TKey> keySelector)
        {
            internalCollection = new Collection<TValue>();
            KeySelector = keySelector;
        }

        /// <summary>
        /// Creates a new <see cref="DictionaryCollection"/> object that represents the given collection of elements as a dictionary using the given function as a selector for keys.
        /// </summary>
        /// <param name="keySelector">A function that, given a value, returns a key that represents the value.</param>
        /// <param name="elements">The list of elements to represent.</param>
        public DictionaryCollection(Func<TValue, TKey> keySelector, IEnumerable<TValue> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            internalCollection = new List<TValue>(elements);
            KeySelector = keySelector;
        }

        /// <summary>
        /// Creates a new <see cref="DictionaryCollection"/> object that represents the given collection of elements as a dictionary using the given function as a selector for keys.
        /// </summary>
        /// <param name="keySelector">A function that, given a value, returns a key that represents the value.</param>
        /// <param name="elements">The list of elements to represent.</param>
        public DictionaryCollection(Func<TValue, TKey> keySelector, ICollection<TValue> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            internalCollection = elements;
            KeySelector = keySelector;
        }

        /// <summary>
        /// Gets the function that, given a value returns a key for that value.
        /// </summary>
        public Func<TValue, TKey> KeySelector
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value)
        {
            internalCollection.Add(value);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
        /// </returns>
        public virtual bool ContainsKey(TKey key)
        {
            foreach (TValue val in internalCollection)
            {
                if (KeySelector(val).Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
        public virtual ICollection<TKey> Keys
        {
            get { return internalCollection.Select(v => KeySelector(v)).ToArray(); }
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual bool Remove(TKey key)
        {
            foreach (TValue val in internalCollection)
            {
                if (KeySelector(val).Equals(key))
                {
                    return internalCollection.Remove(val);
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
        /// </returns>
        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            foreach (TValue val in internalCollection)
            {
                if (KeySelector(val).Equals(key))
                {
                    value = val;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
        public ICollection<TValue> Values
        {
            get { return internalCollection; }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                foreach (TValue val in internalCollection)
                {
                    if (KeySelector(val).Equals(key))
                    {
                        return val;
                    }
                }
                TValue defaultValue = default(TValue);
                if (defaultValue != null)
                {
                    throw new KeyNotFoundException(string.Format("The value for the given key: {0}, could not be found.", key));
                }
                else
                {
                    return defaultValue;
                }
            }
            set
            {
                foreach (TValue val in internalCollection)
                {
                    if (KeySelector(val).Equals(key))
                    {
                        internalCollection.Remove(val);
                        internalCollection.Add(value);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            internalCollection.Add(item.Value);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public virtual void Clear()
        {
            internalCollection.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return internalCollection.Contains(item.Value);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            array = internalCollection.Skip(arrayIndex).ToDictionary(v => KeySelector(v), v => v).ToArray();
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public int Count
        {
            get { return internalCollection.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get { return internalCollection.IsReadOnly; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return internalCollection.ToDictionary(v => KeySelector(v)).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds the given items to the collection.
        /// </summary>
        /// <param name="items">The items to add to the collection.</param>
        public virtual void AddRange(IEnumerable<TValue> items)
        {
            foreach (TValue val in items)
            {
                this.Add(val);
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public virtual void Add(TValue item)
        {
            internalCollection.Add(item);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public virtual bool Contains(TValue item)
        {
            return internalCollection.Contains(item);
        }

        public virtual void CopyTo(TValue[] array, int arrayIndex)
        {
            internalCollection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public virtual bool Remove(TValue item)
        {
            return internalCollection.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return internalCollection.GetEnumerator();
        }
    }
}
