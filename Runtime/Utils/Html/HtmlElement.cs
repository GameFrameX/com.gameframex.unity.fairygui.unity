using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public enum HtmlElementType
    {
        Text,
        Link,
        Image,
        Input,
        Select,
        Object,

        //internal
        LinkEnd,
    }

    /// <summary>
    /// 
    /// </summary>
    public class HtmlElement
    {
        public HtmlElementType type;
        public string name;
        public string text;
        public TextFormat format;
        public int charIndex;

        public IHtmlObject htmlObject;
        public int status; //1 hidden 2 clipped 4 added
        public int space;
        public Vector2 position;

        Hashtable attributes;

        [UnityEngine.Scripting.Preserve]
        public HtmlElement()
        {
            format = new TextFormat();
        }

        [UnityEngine.Scripting.Preserve]
        public object Get(string attrName)
        {
            if (attributes == null)
                return null;

            return attributes[attrName];
        }

        [UnityEngine.Scripting.Preserve]
        public void Set(string attrName, object attrValue)
        {
            if (attributes == null)
                attributes = new Hashtable();

            attributes[attrName] = attrValue;
        }

        [UnityEngine.Scripting.Preserve]
        public string GetString(string attrName)
        {
            return GetString(attrName, null);
        }

        [UnityEngine.Scripting.Preserve]
        public string GetString(string attrName, string defValue)
        {
            if (attributes == null)
                return defValue;

            object ret = attributes[attrName];
            if (ret != null)
                return ret.ToString();
            else
                return defValue;
        }

        [UnityEngine.Scripting.Preserve]
        public int GetInt(string attrName)
        {
            return GetInt(attrName, 0);
        }

        [UnityEngine.Scripting.Preserve]
        public int GetInt(string attrName, int defValue)
        {
            string value = GetString(attrName);
            if (value == null || value.Length == 0)
                return defValue;

            if (value[value.Length - 1] == '%')
            {
                int ret;
                if (int.TryParse(value.Substring(0, value.Length - 1), out ret))
                    return Mathf.CeilToInt(ret / 100.0f * defValue);
                else
                    return defValue;
            }
            else
            {
                int ret;
                if (int.TryParse(value, out ret))
                    return ret;
                else
                    return defValue;
            }
        }

        [UnityEngine.Scripting.Preserve]
        public float GetFloat(string attrName)
        {
            return GetFloat(attrName, 0);
        }

        [UnityEngine.Scripting.Preserve]
        public float GetFloat(string attrName, float defValue)
        {
            string value = GetString(attrName);
            if (value == null || value.Length == 0)
                return defValue;

            float ret;
            if (float.TryParse(value, out ret))
                return ret;
            else
                return defValue;
        }

        [UnityEngine.Scripting.Preserve]
        public bool GetBool(string attrName)
        {
            return GetBool(attrName, false);
        }

        [UnityEngine.Scripting.Preserve]
        public bool GetBool(string attrName, bool defValue)
        {
            string value = GetString(attrName);
            if (value == null || value.Length == 0)
                return defValue;

            bool ret;
            if (bool.TryParse(value, out ret))
                return ret;
            else
                return defValue;
        }

        [UnityEngine.Scripting.Preserve]
        public Color GetColor(string attrName, Color defValue)
        {
            string value = GetString(attrName);
            if (value == null || value.Length == 0)
                return defValue;

            return ToolSet.ConvertFromHtmlColor(value);
        }

        [UnityEngine.Scripting.Preserve]
        public void FetchAttributes()
        {
            attributes = XMLIterator.GetAttributes(attributes);
        }


        [UnityEngine.Scripting.Preserve]
        public bool isEntity
        {
            get { return type == HtmlElementType.Image || type == HtmlElementType.Select || type == HtmlElementType.Input || type == HtmlElementType.Object; }
        }

        #region Pool Support

        static Stack<HtmlElement> elementPool = new Stack<HtmlElement>();
#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitializeOnLoad()
        {
            elementPool.Clear();
        }
#endif
        [UnityEngine.Scripting.Preserve]
        public static HtmlElement GetElement(HtmlElementType type)
        {
            HtmlElement ret;
            if (elementPool.Count > 0)
                ret = elementPool.Pop();
            else
                ret = new HtmlElement();
            ret.type = type;

            if (type != HtmlElementType.Text && ret.attributes == null)
                ret.attributes = new Hashtable();

            return ret;
        }

        [UnityEngine.Scripting.Preserve]
        public static void ReturnElement(HtmlElement element)
        {
            element.name = null;
            element.text = null;
            element.htmlObject = null;
            element.status = 0;
            if (element.attributes != null)
                element.attributes.Clear();
            elementPool.Push(element);
        }

        [UnityEngine.Scripting.Preserve]
        public static void ReturnElements(List<HtmlElement> elements)
        {
            int count = elements.Count;
            for (int i = 0; i < count; i++)
            {
                HtmlElement element = elements[i];
                ReturnElement(element);
            }

            elements.Clear();
        }

        #endregion
    }
}