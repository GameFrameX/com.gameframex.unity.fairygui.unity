
namespace FairyGUI.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class HtmlLink : IHtmlObject
    {
        RichTextField _owner;
        HtmlElement _element;
        SelectionShape _shape;

        EventCallback1 _clickHandler;
        EventCallback1 _rolloverHandler;
        EventCallback0 _rolloutHandler;

        [UnityEngine.Scripting.Preserve]
        public HtmlLink()
        {
            _shape = new SelectionShape();
            _shape.gameObject.name = "HtmlLink";
            _shape.cursor = "text-link";

            _clickHandler = (EventContext context) =>
            {
                _owner.BubbleEvent("onClickLink", _element.GetString("href"));
            };
            _rolloverHandler = (EventContext context) =>
            {
                if (_owner.htmlParseOptions.linkHoverBgColor.a > 0)
                    _shape.color = _owner.htmlParseOptions.linkHoverBgColor;
            };
            _rolloutHandler = () =>
            {
                if (_owner.htmlParseOptions.linkHoverBgColor.a > 0)
                    _shape.color = _owner.htmlParseOptions.linkBgColor;
            };
        }

        [UnityEngine.Scripting.Preserve]
        public DisplayObject displayObject
        {
            get { return _shape; }
        }

        [UnityEngine.Scripting.Preserve]
        public HtmlElement element
        {
            get { return _element; }
        }

        [UnityEngine.Scripting.Preserve]
        public float width
        {
            get { return 0; }
        }

        [UnityEngine.Scripting.Preserve]
        public float height
        {
            get { return 0; }
        }

        [UnityEngine.Scripting.Preserve]
        public void Create(RichTextField owner, HtmlElement element)
        {
            _owner = owner;
            _element = element;
            _shape.onClick.Add(_clickHandler);
            _shape.onRollOver.Add(_rolloverHandler);
            _shape.onRollOut.Add(_rolloutHandler);
            _shape.color = _owner.htmlParseOptions.linkBgColor;
        }

        [UnityEngine.Scripting.Preserve]
        public void SetArea(int startLine, float startCharX, int endLine, float endCharX)
        {
            if (startLine == endLine && startCharX > endCharX)
            {
                float tmp = startCharX;
                startCharX = endCharX;
                endCharX = tmp;
            }
            _shape.rects.Clear();
            _owner.textField.GetLinesShape(startLine, startCharX, endLine, endCharX, true, _shape.rects);
            _shape.Refresh();
        }

        [UnityEngine.Scripting.Preserve]
        public void SetPosition(float x, float y)
        {
            _shape.SetXY(x, y);
        }

        [UnityEngine.Scripting.Preserve]
        public void Add()
        {
            //add below _shape
            _owner.AddChildAt(_shape, 0);
        }

        [UnityEngine.Scripting.Preserve]
        public void Remove()
        {
            if (_shape.parent != null)
                _owner.RemoveChild(_shape);
        }

        [UnityEngine.Scripting.Preserve]
        public void Release()
        {
            _shape.RemoveEventListeners();

            _owner = null;
            _element = null;
        }

        [UnityEngine.Scripting.Preserve]
        public void Dispose()
        {
            _shape.Dispose();
            _shape = null;
        }
    }
}
