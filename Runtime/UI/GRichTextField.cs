using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// GRichTextField class.
    /// </summary>
    public class GRichTextField : GTextField
    {
        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public RichTextField richTextField { get; private set; }

        [UnityEngine.Scripting.Preserve]
        public GRichTextField()
            : base()
        {
        }

        override protected void CreateDisplayObject()
        {
            richTextField = new RichTextField();
            richTextField.gOwner = this;
            displayObject = richTextField;

            _textField = richTextField.textField;
        }

        override protected void SetTextFieldText()
        {
            string str = _text;
            if (_templateVars != null)
                str = ParseTemplate(str);

            _textField.maxWidth = maxWidth;
            if (_ubbEnabled)
                richTextField.htmlText = UBBParser.inst.Parse(str);
            else
                richTextField.htmlText = str;
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public Dictionary<uint, Emoji> emojies
        {
            get { return richTextField.emojies; }
            set { richTextField.emojies = value; }
        }
    }
}
