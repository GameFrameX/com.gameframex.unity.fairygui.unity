using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    public class EventContext
    {
        /// <summary>
        /// 事件发送者。
        /// </summary>
        public EventDispatcher sender { get; internal set; }

        /// <summary>
        /// 事件发起者。
        /// </summary>
        public object initiator { get; internal set; }

        /// <summary>
        /// 输入事件。
        /// </summary>
        public InputEvent inputEvent { get; internal set; }

        /// <summary>
        /// 事件类型。
        /// </summary>
        public string type;

        /// <summary>
        /// 事件数据。
        /// </summary>
        public object data;

        internal bool _defaultPrevented;
        internal bool _stopsPropagation;
        internal bool _touchCapture;

        internal List<EventBridge> callChain = new List<EventBridge>();

        /// <summary>
        /// 停止事件传播。
        /// </summary>
        public void StopPropagation()
        {
            _stopsPropagation = true;
        }

        /// <summary>
        /// 阻止事件默认行为。
        /// </summary>
        public void PreventDefault()
        {
            _defaultPrevented = true;
        }

        /// <summary>
        /// 捕获触摸事件。
        /// </summary>
        public void CaptureTouch()
        {
            _touchCapture = true;
        }

        /// <summary>
        /// 是否阻止事件默认行为。
        /// </summary>
        public bool isDefaultPrevented
        {
            get { return _defaultPrevented; }
        }

        static Stack<EventContext> pool = new Stack<EventContext>();

        internal static EventContext Get()
        {
            if (pool.Count > 0)
            {
                EventContext context = pool.Pop();
                context._stopsPropagation = false;
                context._defaultPrevented = false;
                context._touchCapture = false;
                return context;
            }
            else
                return new EventContext();
        }

        internal static void Return(EventContext value)
        {
            pool.Push(value);
        }
#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitializeOnLoad()
        {
            pool.Clear();
        }
#endif
    }
}