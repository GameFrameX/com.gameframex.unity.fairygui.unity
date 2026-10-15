using System;
using System.Collections.Generic;
using System.Text;

namespace FairyGUI.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class HtmlImage : IHtmlObject
    {
        [UnityEngine.Scripting.Preserve]
        public GLoader loader { get; private set; }

        RichTextField _owner;
        HtmlElement _element;
        bool _externalTexture;

        [UnityEngine.Scripting.Preserve]
        public HtmlImage()
        {
            loader = (GLoader)UIObjectFactory.NewObject(ObjectType.Loader);
            loader.gameObjectName = "HtmlImage";
            loader.fill = FillType.ScaleFree;
            loader.touchable = false;
        }

        [UnityEngine.Scripting.Preserve]
        public DisplayObject displayObject
        {
            get { return loader.displayObject; }
        }

        [UnityEngine.Scripting.Preserve]
        public HtmlElement element
        {
            get { return _element; }
        }

        [UnityEngine.Scripting.Preserve]
        public float width
        {
            get { return loader.width; }
        }

        [UnityEngine.Scripting.Preserve]
        public float height
        {
            get { return loader.height; }
        }

        [UnityEngine.Scripting.Preserve]
        public void Create(RichTextField owner, HtmlElement element)
        {
            _owner = owner;
            _element = element;

            int sourceWidth = 0;
            int sourceHeight = 0;
            NTexture texture = owner.htmlPageContext.GetImageTexture(this);
            if (texture != null)
            {
                sourceWidth = texture.width;
                sourceHeight = texture.height;

                loader.texture = texture;
                _externalTexture = true;
            }
            else
            {
                string src = element.GetString("src");
                if (src != null)
                {
                    PackageItem pi = UIPackage.GetItemByURL(src);
                    if (pi != null)
                    {
                        sourceWidth = pi.width;
                        sourceHeight = pi.height;
                    }
                }

                loader.url = src;
                _externalTexture = false;
            }

            int width = element.GetInt("width", sourceWidth);
            int height = element.GetInt("height", sourceHeight);

            if (width == 0)
                width = 5;
            if (height == 0)
                height = 10;
            loader.SetSize(width, height);
        }

        [UnityEngine.Scripting.Preserve]
        public void SetPosition(float x, float y)
        {
            loader.SetXY(x, y);
        }

        [UnityEngine.Scripting.Preserve]
        public void Add()
        {
            _owner.AddChild(loader.displayObject);
        }

        [UnityEngine.Scripting.Preserve]
        public void Remove()
        {
            if (loader.displayObject.parent != null)
                _owner.RemoveChild(loader.displayObject);
        }

        [UnityEngine.Scripting.Preserve]
        public void Release()
        {
            loader.RemoveEventListeners();
            if (_externalTexture)
            {
                _owner.htmlPageContext.FreeImageTexture(this, loader.texture);
                _externalTexture = false;
            }

            loader.url = null;
            _owner = null;
            _element = null;
        }

        [UnityEngine.Scripting.Preserve]
        public void Dispose()
        {
            if (_externalTexture)
                _owner.htmlPageContext.FreeImageTexture(this, loader.texture);
            loader.Dispose();
        }
    }
}
