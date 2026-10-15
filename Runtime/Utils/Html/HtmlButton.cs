using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class HtmlButton : IHtmlObject
    {
        [UnityEngine.Scripting.Preserve]
        public GComponent button { get; private set; }

        public const string CLICK_EVENT = "OnHtmlButtonClick";

        public static string resource;

        RichTextField _owner;
        HtmlElement _element;
        EventCallback1 _clickHandler;

        [UnityEngine.Scripting.Preserve]
        public HtmlButton()
        {
            button = UIPackage.CreateObjectFromURL(resource).asCom;
            _clickHandler = (EventContext context) =>
            {
                _owner.DispatchEvent(CLICK_EVENT, context.data, this);
            };
        }

        [UnityEngine.Scripting.Preserve]
        public DisplayObject displayObject
        {
            get { return button != null ? button.displayObject : null; }
        }

        [UnityEngine.Scripting.Preserve]
        public HtmlElement element
        {
            get { return _element; }
        }

        [UnityEngine.Scripting.Preserve]
        public float width
        {
            get { return button != null ? button.width : 0; }
        }

        [UnityEngine.Scripting.Preserve]
        public float height
        {
            get { return button != null ? button.height : 0; }
        }

        [UnityEngine.Scripting.Preserve]
        public void Create(RichTextField owner, HtmlElement element)
        {
            _owner = owner;
            _element = element;

            if (button == null)
                return;

            button.onClick.Add(_clickHandler);
            int width = element.GetInt("width", button.sourceWidth);
            int height = element.GetInt("height", button.sourceHeight);
            button.SetSize(width, height);
            button.text = element.GetString("value");
        }

        [UnityEngine.Scripting.Preserve]
        public void SetPosition(float x, float y)
        {
            if (button != null)
                button.SetXY(x, y);
        }

        [UnityEngine.Scripting.Preserve]
        public void Add()
        {
            if (button != null)
                _owner.AddChild(button.displayObject);
        }

        [UnityEngine.Scripting.Preserve]
        public void Remove()
        {
            if (button != null && button.displayObject.parent != null)
                _owner.RemoveChild(button.displayObject);
        }

        [UnityEngine.Scripting.Preserve]
        public void Release()
        {
            if (button != null)
                button.RemoveEventListeners();

            _owner = null;
            _element = null;
        }

        [UnityEngine.Scripting.Preserve]
        public void Dispose()
        {
            if (button != null)
                button.Dispose();
        }
    }
}
