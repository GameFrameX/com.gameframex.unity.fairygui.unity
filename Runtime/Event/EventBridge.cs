#if FAIRYGUI_TOLUA
using System;
using LuaInterface;
#endif

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    sealed class EventBridge
    {
        public EventDispatcher owner;

        EventCallback0 _callback0;
        EventCallback1 _callback1;
        EventCallback1 _captureCallback;
        internal bool _dispatching;

        public EventBridge(EventDispatcher owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// 添加捕获事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param> 
        public void AddCapture(EventCallback1 callback)
        {
            _captureCallback -= callback;
            _captureCallback += callback;
        }

        /// <summary>
        /// 移除捕获事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param> 
        public void RemoveCapture(EventCallback1 callback)
        {
            _captureCallback -= callback;
        }

        /// <summary>
        /// 添加事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Add(EventCallback1 callback)
        {
            _callback1 -= callback;
            _callback1 += callback;
        }

        /// <summary>
        /// 移除事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Remove(EventCallback1 callback)
        {
            _callback1 -= callback;
        }

        /// <summary>
        /// 添加事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Add(EventCallback0 callback)
        {
            _callback0 -= callback;
            _callback0 += callback;
        }

        /// <summary>
        /// 移除事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Remove(EventCallback0 callback)
        {
            _callback0 -= callback;
        }

#if FAIRYGUI_TOLUA
        public void Add(LuaFunction func, LuaTable self)
        {
            EventCallback1 callback;
            if(self != null)
                callback = (EventCallback1)DelegateTraits<EventCallback1>.Create(func, self);
            else
                callback = (EventCallback1)DelegateTraits<EventCallback1>.Create(func);
            _callback1 -= callback;
            _callback1 += callback;
        }

        public void Add(LuaFunction func, GComponent self)
        {
            if (self._peerTable == null)
                throw new Exception("self is not connected to lua.");

            Add(func, self._peerTable);
        }

        public void Remove(LuaFunction func, LuaTable self)
        {
            LuaState state = func.GetLuaState();
            LuaDelegate target;
            if (self != null)
                target = state.GetLuaDelegate(func, self);
            else
                target = state.GetLuaDelegate(func);

            Delegate[] ds = _callback1.GetInvocationList();

            for (int i = 0; i < ds.Length; i++)
            {
                LuaDelegate ld = ds[i].Target as LuaDelegate;
                if (ld != null && ld.Equals(target))
                {
                    _callback1 = (EventCallback1)Delegate.Remove(_callback1, ds[i]);
                    //DelayDispose will cause problem
                    //state.DelayDispose(ld.func);
                    //if (ld.self != null)
                    //	state.DelayDispose(ld.self);
                    break;
                }
            }
        }

        public void Remove(LuaFunction func, GComponent self)
        {
            if (self._peerTable == null)
                throw new Exception("self is not connected to lua.");

            Remove(func, self._peerTable);
        }
#endif

        /// <summary>
        /// 是否为空。
        /// </summary>
        public bool isEmpty
        {
            get { return _callback1 == null && _callback0 == null && _captureCallback == null; }
        }

        /// <summary>
        /// 清除事件监听器。
        /// </summary>
        public void Clear()
        {
#if FAIRYGUI_TOLUA
            //if (_callback1 != null)
            //{
            //	Delegate[] ds = _callback1.GetInvocationList();
            //	for (int i = 0; i < ds.Length; i++)
            //	{
            //		LuaDelegate ld = ds[i].Target as LuaDelegate;
            //		if (ld != null)
            //		{
            //			LuaState state = ld.func.GetLuaState();
            //			state.DelayDispose(ld.func);
            //			if (ld.self != null)
            //				state.DelayDispose(ld.self);
            //		}
            //	}
            //}
#endif
            _callback1 = null;
            _callback0 = null;
            _captureCallback = null;
        }

        /// <summary>
        /// 调用事件监听器。
        /// </summary>
        /// <param name="context">事件上下文</param>
        public void CallInternal(EventContext context)
        {
            _dispatching = true;
            context.sender = owner;
            try
            {
                if (_callback1 != null)
                    _callback1(context);
                if (_callback0 != null)
                    _callback0();
            }
            finally
            {
                _dispatching = false;
            }
        }

        /// <summary>
        /// 调用捕获事件监听器。
        /// </summary>
        /// <param name="context">事件上下文</param>
        public void CallCaptureInternal(EventContext context)
        {
            if (_captureCallback == null)
                return;

            _dispatching = true;
            context.sender = owner;
            try
            {
                _captureCallback(context);
            }
            finally
            {
                _dispatching = false;
            }
        }
    }
}