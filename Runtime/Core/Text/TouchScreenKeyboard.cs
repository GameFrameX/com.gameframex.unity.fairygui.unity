using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    public class TouchScreenKeyboard : IKeyboard
    {
        UnityEngine.TouchScreenKeyboard _keyboard;

        [UnityEngine.Scripting.Preserve]
        public bool done
        {
#if UNITY_2017_2_OR_NEWER
            get
            {
                return _keyboard == null
              || _keyboard.status == UnityEngine.TouchScreenKeyboard.Status.Done
              || _keyboard.status == UnityEngine.TouchScreenKeyboard.Status.Canceled
              || _keyboard.status == UnityEngine.TouchScreenKeyboard.Status.LostFocus;
            }
#else
            get { return _keyboard == null || _keyboard.done || _keyboard.wasCanceled; }
#endif
        }

        [UnityEngine.Scripting.Preserve]
        public bool supportsCaret
        {
            get { return false; }
        }

        [UnityEngine.Scripting.Preserve]
        public string GetInput()
        {
            if (_keyboard != null)
            {
                string s = _keyboard.text;

                if (this.done)
                    _keyboard = null;

                return s;
            }
            else
                return null;
        }

        [UnityEngine.Scripting.Preserve]
        public void Open(string text, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int keyboardType, bool hideInput)
        {
            if (_keyboard != null)
                return;

            UnityEngine.TouchScreenKeyboard.hideInput = hideInput;
            _keyboard = UnityEngine.TouchScreenKeyboard.Open(text, (TouchScreenKeyboardType)keyboardType, autocorrection, multiline, secure, alert, textPlaceholder);
        }
        
        [UnityEngine.Scripting.Preserve]
        public void Open(string text, int keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, bool hideInput, string textPlaceholder, int characterLimit)
        {
            if (_keyboard != null)
                return;
            UnityEngine.TouchScreenKeyboard.hideInput = hideInput;
            this._keyboard = UnityEngine.TouchScreenKeyboard.Open(text, (TouchScreenKeyboardType)keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
        }

        [UnityEngine.Scripting.Preserve]
        public void Close()
        {
            if (_keyboard != null)
            {
                _keyboard.active = false;
                _keyboard = null;
            }
        }
    }
}
