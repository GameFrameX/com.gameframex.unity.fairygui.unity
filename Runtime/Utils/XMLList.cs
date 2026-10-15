using System;
using System.Collections.Generic;

namespace FairyGUI.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class XMLList
    {
        public List<XML> rawList;

        [UnityEngine.Scripting.Preserve]
        public XMLList()
        {
            rawList = new List<XML>();
        }

        [UnityEngine.Scripting.Preserve]
        public XMLList(List<XML> list)
        {
            rawList = list;
        }

        [UnityEngine.Scripting.Preserve]
        public void Add(XML xml)
        {
            rawList.Add(xml);
        }

        [UnityEngine.Scripting.Preserve]
        public void Clear()
        {
            rawList.Clear();
        }

        [UnityEngine.Scripting.Preserve]
        public int Count
        {
            get { return rawList.Count; }
        }

        [UnityEngine.Scripting.Preserve]
        public XML this[int index]
        {
            get { return rawList[index]; }
        }

        [UnityEngine.Scripting.Preserve]
        public Enumerator GetEnumerator()
        {
            return new Enumerator(rawList, null);
        }

        [UnityEngine.Scripting.Preserve]
        public Enumerator GetEnumerator(string selector)
        {
            return new Enumerator(rawList, selector);
        }

        static List<XML> _tmpList = new List<XML>();
        [UnityEngine.Scripting.Preserve]
        public XMLList Filter(string selector)
        {
            bool allFit = true;
            _tmpList.Clear();
            int cnt = rawList.Count;
            for (int i = 0; i < cnt; i++)
            {
                XML xml = rawList[i];
                if (xml.name == selector)
                    _tmpList.Add(xml);
                else
                    allFit = false;
            }

            if (allFit)
                return this;
            else
            {
                XMLList ret = new XMLList(_tmpList);
                _tmpList = new List<XML>();
                return ret;
            }
        }

        [UnityEngine.Scripting.Preserve]
        public XML Find(string selector)
        {
            int cnt = rawList.Count;
            for (int i = 0; i < cnt; i++)
            {
                XML xml = rawList[i];
                if (xml.name == selector)
                    return xml;
            }
            return null;
        }

        [UnityEngine.Scripting.Preserve]
        public void RemoveAll(string selector)
        {
            rawList.RemoveAll(xml => xml.name == selector);
        }

        public struct Enumerator
        {
            List<XML> _source;
            string _selector;
            int _index;
            int _total;
            XML _current;

            [UnityEngine.Scripting.Preserve]
            public Enumerator(List<XML> source, string selector)
            {
                _source = source;
                _selector = selector;
                _index = -1;
                if (_source != null)
                    _total = _source.Count;
                else
                    _total = 0;
                _current = null;
            }

            [UnityEngine.Scripting.Preserve]
            public XML Current
            {
                get { return _current; }
            }

            [UnityEngine.Scripting.Preserve]
            public bool MoveNext()
            {
                while (++_index < _total)
                {
                    _current = _source[_index];
                    if (_selector == null || _current.name == _selector)
                        return true;
                }

                return false;
            }

            [UnityEngine.Scripting.Preserve]
            public void Erase()
            {
                _source.RemoveAt(_index);
                _total--;
            }

            [UnityEngine.Scripting.Preserve]
            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
