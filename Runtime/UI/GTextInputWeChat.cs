#if UNITY_WEBGL && WECHAT_MINI_GAME
using UnityEngine;
using UnityEngine.Scripting;
using WeChatWASM;

namespace FairyGUI
{
    [Preserve]
    public sealed class GTextInputWeChat
    {
        private readonly GTextInput _textInput;
        private readonly bool _isMultiline;
        private bool _isShowKeyboard = false;

        public GTextInputWeChat(GTextInput input)
        {
            _textInput = input;
            _isMultiline = !_textInput.singleLine;
            input.onFocusIn.Add(OnFocusIn);
            input.onFocusOut.Add(OnFocusOut);
        }

        public void Dispose()
        {
            _textInput.onFocusIn.Remove(OnFocusIn);
            _textInput.onFocusOut.Remove(OnFocusOut);
        }

        public void OnInput(OnKeyboardInputListenerResult v)
        {
            if (_textInput.focused)
            {
                _textInput.text = v.value;
                _textInput.SetSelection(v.value.Length, v.value.Length);
            }
        }

        public void OnConfirm(OnKeyboardInputListenerResult v)
        {
            // 输入法confirm回调
            Debug.Log("onConfirm");
            Debug.Log(v.value);
            HideKeyboard();
        }

        public void OnComplete(OnKeyboardInputListenerResult v)
        {
            // 输入法complete回调
            HideKeyboard();
        }

        private void OnFocusOut(EventContext context)
        {
            HideKeyboard();
        }

        private void OnFocusIn(EventContext context)
        {
            ShowKeyboard();
        }

        private void ShowKeyboard()
        {
            if (!_isShowKeyboard)
            {
                WX.ShowKeyboard(new ShowKeyboardOption()
                {
                    multiple = _isMultiline,
                    confirmHold = true,
                    defaultValue = _textInput.text,
                    maxLength = _textInput.maxLength,
                    confirmType = "done"
                });

                //绑定回调
                WX.OnKeyboardConfirm(OnConfirm);
                WX.OnKeyboardComplete(OnComplete);
                WX.OnKeyboardInput(OnInput);
                _isShowKeyboard = true;
            }
        }

        private void HideKeyboard()
        {
            if (_isShowKeyboard)
            {
                WX.HideKeyboard(new HideKeyboardOption());
                //删除掉相关事件监听
                WX.OffKeyboardInput(OnInput);
                WX.OffKeyboardConfirm(OnConfirm);
                WX.OffKeyboardComplete(OnComplete);
                _isShowKeyboard = false;
            }
        }
    }
}
#endif