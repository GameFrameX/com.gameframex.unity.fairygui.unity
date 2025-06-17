using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// 文字打字效果。先调用Start，然后Print。
    /// </summary>
    public class TypingEffect
    {
        protected TextField _textField;

        protected int _printIndex;

        protected bool _started;

        /// <summary>
        /// 打字完成事件
        /// </summary>
        public Action OnComplete;

        /// <summary>
        /// 打字进度变化事件
        /// </summary>
        public Action<float> OnProgress;

        /// <summary>
        /// 是否正在打印
        /// </summary>
        public bool IsTyping
        {
            get { return _started; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textField"></param>
        public TypingEffect(TextField textField)
        {
            _textField = textField;
            _textField.EnableCharPositionSupport();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textField"></param>
        public TypingEffect(GTextField textField)
        {
            if (textField is GRichTextField)
            {
                _textField = ((RichTextField)textField.displayObject).textField;
            }
            else
            {
                _textField = (TextField)textField.displayObject;
            }

            _textField.EnableCharPositionSupport();
        }

        /// <summary>
        /// 总输出次数
        /// </summary>
        public int TotalTimes
        {
            get
            {
                int times = 0;
                for (int i = 0; i < _textField.parsedText.Length; i++)
                {
                    if (!char.IsWhiteSpace(_textField.parsedText[i]))
                    {
                        times++;
                    }
                }

                if (_textField.richTextField != null)
                {
                    for (int i = 0; i < _textField.richTextField.htmlElementCount; i++)
                    {
                        if (_textField.richTextField.GetHtmlElementAt(i).isEntity)
                        {
                            times++;
                        }
                    }
                }

                return times;
            }
        }

        /// <summary>
        /// 开始打字效果。可以重复调用重复启动。
        /// </summary>
        public void Start()
        {
            if (_textField.SetTypingEffectPos(0) != -1)
            {
                _started = true;
                _printIndex = 1;

                //隐藏所有混排的对象
                if (_textField.richTextField != null)
                {
                    int ec = _textField.richTextField.htmlElementCount;
                    for (int i = 0; i < ec; i++)
                    {
                        _textField.richTextField.ShowHtmlObject(i, false);
                    }
                }
            }
            else
            {
                _started = false;
            }
        }

        /// <summary>
        /// 输出一个字符。如果已经没有剩余的字符，返回false。
        /// </summary>
        /// <returns></returns>
        private bool Print()
        {
            if (!_started)
            {
                return false;
            }

            _printIndex = _textField.SetTypingEffectPos(_printIndex);
            if (_printIndex != -1)
            {
                var listCnt = _textField.parsedText.Length;
                // 更新进度
                OnProgress?.Invoke((float)_printIndex / (listCnt - 1));
                return true;
            }

            // 打字完成
            OnComplete?.Invoke();
            Cancel();
            _started = false;
            return false;
        }

        /// <summary>
        /// 打印的协程。
        /// </summary>
        /// <param name="interval">每个字符输出的时间间隔</param>
        /// <returns></returns>
        public IEnumerator Print(float interval)
        {
            while (Print())
            {
                yield return new WaitForSeconds(interval);
            }
        }

        /// <summary>
        /// 使用固定时间间隔完成整个打印过程。
        /// </summary>
        /// <param name="interval"></param>
        public void PrintAll(float interval)
        {
            Timers.inst.StartCoroutine(Print(interval));
        }

        public void Cancel()
        {
            if (!_started)
            {
                return;
            }

            _started = false;
            _textField.SetTypingEffectPos(-1);
        }
    }
}