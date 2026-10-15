using UnityEngine;

namespace FairyGUI.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class HtmlInput : IHtmlObject
    {
        [UnityEngine.Scripting.Preserve]
        public GTextInput textInput { get; private set; }

        RichTextField _owner;
        HtmlElement _element;
        bool _hidden;

        public static int defaultBorderSize = 2;
        public static Color defaultBorderColor = ToolSet.ColorFromRGB(0xA9A9A9);
        public static Color defaultBackgroundColor = Color.clear;

        [UnityEngine.Scripting.Preserve]
        public HtmlInput()
        {
            textInput = (GTextInput)UIObjectFactory.NewObject(ObjectType.InputText);
            textInput.gameObjectName = "HtmlInput";
            textInput.verticalAlign = VertAlignType.Middle;
        }

        [UnityEngine.Scripting.Preserve]
        public DisplayObject displayObject
        {
            get { return textInput.displayObject; }
        }

        [UnityEngine.Scripting.Preserve]
        public HtmlElement element
        {
            get { return _element; }
        }

        [UnityEngine.Scripting.Preserve]
        public float width
        {
            get { return _hidden ? 0 : textInput.width; }
        }

        [UnityEngine.Scripting.Preserve]
        public float height
        {
            get { return _hidden ? 0 : textInput.height; }
        }

        [UnityEngine.Scripting.Preserve]
        public void Create(RichTextField owner, HtmlElement element)
        {
            _owner = owner;
            _element = element;

            string type = element.GetString("type");
            if (type != null)
                type = type.ToLower();

            _hidden = type == "hidden";
            if (!_hidden)
            {
                int width = element.GetInt("width", 0);
                int height = element.GetInt("height", 0);
                int borderSize = element.GetInt("border", defaultBorderSize);
                Color borderColor = element.GetColor("border-color", defaultBorderColor);
                Color backgroundColor = element.GetColor("background-color", defaultBackgroundColor);

                if (width == 0)
                {
                    width = element.space;
                    if (width > _owner.width / 2 || width < 100)
                        width = (int)_owner.width / 2;
                }
                if (height == 0)
                    height = element.format.size + 10;

                textInput.textFormat = element.format;
                textInput.displayAsPassword = type == "password";
                textInput.maxLength = element.GetInt("maxlength", int.MaxValue);
                textInput.border = borderSize;
                textInput.borderColor = borderColor;
                textInput.backgroundColor = backgroundColor;
                textInput.SetSize(width, height);
            }
            textInput.text = element.GetString("value");
        }

        [UnityEngine.Scripting.Preserve]
        public void SetPosition(float x, float y)
        {
            if (!_hidden)
                textInput.SetXY(x, y);
        }

        [UnityEngine.Scripting.Preserve]
        public void Add()
        {
            if (!_hidden)
                _owner.AddChild(textInput.displayObject);
        }

        [UnityEngine.Scripting.Preserve]
        public void Remove()
        {
            if (!_hidden && textInput.displayObject.parent != null)
                _owner.RemoveChild(textInput.displayObject);
        }

        [UnityEngine.Scripting.Preserve]
        public void Release()
        {
            textInput.RemoveEventListeners();
            textInput.text = null;

            _owner = null;
            _element = null;
        }

        [UnityEngine.Scripting.Preserve]
        public void Dispose()
        {
            textInput.Dispose();
        }
    }
}
