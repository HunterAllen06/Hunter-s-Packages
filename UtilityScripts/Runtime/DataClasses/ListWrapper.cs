using System.Collections;
using System.Collections.Generic;

namespace HunterAllen.Utility
{
    [System.Serializable]
    public class ListWrapper<T> : IEnumerable<List<T>>
    {
        public List<T> List = new();

        public IEnumerator<List<T>> GetEnumerator()
        {
            return List.GetEnumerator() as IEnumerator<List<T>>;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}