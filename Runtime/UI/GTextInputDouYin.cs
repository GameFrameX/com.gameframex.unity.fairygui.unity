#if UNITY_WEBGL && DOUYIN_MINI_GAME && ENABLE_DOUYIN_MINI_GAME

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using StarkSDKSpace;

namespace FairyGUI
{
    [Preserve]
    public sealed class GTextInputDouYin
    {
        private readonly GTextInput _textInput;
        private readonly bool isMultiLine = true;
        private bool _isFocus;

        private bool _isShowKeyboard = false;


        public GTextInputDouYin(GTextInput input)
        {
            _textInput = input;
            isMultiLine = !_textInput.singleLine;
            input.onFocusIn.Add(OnFocusIn);
            input.onFocusOut.Add(OnFocusOut);
        }

        public void Dispose()
        {
            _textInput.onFocusIn.Remove(OnFocusIn);
            _textInput.onFocusOut.Remove(OnFocusOut);
        }

        private void OnFocusOut(EventContext context)
        {
            Debug.Log("OnFocusOut -- ");
            HideKeyboard();
        }

        private void OnFocusIn(EventContext context)
        {
            Debug.Log("OnFocusIn ++ ");
            ShowKeyboard();
        }

        private void OnKeyboardInput(string value)
        {
            Debug.Log($"OnKeyboardInput: {value}");
            if (_textInput.focused)
            {
                _textInput.text = value;
                _textInput.SetSelection(value.Length, value.Length);
            }
        }

        private void OnKeyboardConfirm(string value)
        {
            Debug.Log($"OnKeyboardConfirm: {value}");
        }

        private void OnKeyboardComplete(string value)
        {
            Debug.Log($"OnKeyboardComplete: {value}");
            _textInput.text = value;
            _textInput.SetSelection(value.Length, value.Length);
        }

        private void RegisterKeyboardEvents()
        {
            StarkSDK.API.GetStarkKeyboard().onKeyboardInputEvent += OnKeyboardInput;
            StarkSDK.API.GetStarkKeyboard().onKeyboardConfirmEvent += OnKeyboardConfirm;
            StarkSDK.API.GetStarkKeyboard().onKeyboardCompleteEvent += OnKeyboardComplete;
        }

        private void UnregisterKeyboardEvents()
        {
            StarkSDK.API.GetStarkKeyboard().onKeyboardInputEvent -= OnKeyboardInput;
            StarkSDK.API.GetStarkKeyboard().onKeyboardConfirmEvent -= OnKeyboardConfirm;
            StarkSDK.API.GetStarkKeyboard().onKeyboardCompleteEvent -= OnKeyboardComplete;
        }

        private void ShowKeyboard()
        {
            if (!_isShowKeyboard)
            {
                StarkSDK.API.GetStarkKeyboard().ShowKeyboard(new StarkKeyboard.ShowKeyboardOptions()
                {
                    defaultValue = _textInput.text,
                    maxLength = _textInput.maxLength,
                    multiple = isMultiLine,
                    confirmType = "done"
                });

                RegisterKeyboardEvents();
                _isShowKeyboard = true;
            }
        }

        private void HideKeyboard()
        {
            if (_isShowKeyboard)
            {
                StarkSDK.API.GetStarkKeyboard().HideKeyboard();
                //删除掉相关事件监听
                UnregisterKeyboardEvents();
                _isShowKeyboard = false;
            }
        }
    }
}

#endif