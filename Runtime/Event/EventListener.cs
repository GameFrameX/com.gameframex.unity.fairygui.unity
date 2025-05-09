#if FAIRYGUI_TOLUA
using LuaInterface;
#endif

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    public class EventListener
    {
        EventBridge _bridge;
        string _type;

        public EventListener(EventDispatcher owner, string type)
        {
            _bridge = owner.GetEventBridge(type);
            _type = type;
        }

        /// <summary>
        /// 事件类型。
        /// </summary>
        public string type
        {
            get { return _type; }
        }

        /// <summary>
        /// 添加捕获事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void AddCapture(EventCallback1 callback)
        {
            _bridge.AddCapture(callback);
        }

        /// <summary>
        /// 移除捕获事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void RemoveCapture(EventCallback1 callback)
        {
            _bridge.RemoveCapture(callback);
        }

        /// <summary>
        /// 添加事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Add(EventCallback1 callback)
        {
            _bridge.Add(callback);
        }

        /// <summary>
        /// 移除事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Remove(EventCallback1 callback)
        {
            _bridge.Remove(callback);
        }

        /// <summary>
        /// 添加事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
#if FAIRYGUI_TOLUA
        [NoToLua]
#endif
        public void Add(EventCallback0 callback)
        {
            _bridge.Add(callback);
        }

        /// <summary>
        /// 移除事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
#if FAIRYGUI_TOLUA
        [NoToLua]
#endif
        public void Remove(EventCallback0 callback)
        {
            _bridge.Remove(callback);
        }

        /// <summary>
        /// 重置事件监听器。注册前先移除再添加
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Reset(EventCallback0 callback)
        {
            _bridge.Remove(callback);
            _bridge.Add(callback);
        }

        /// <summary>
        /// 重置事件监听器。注册前先移除再添加
        /// </summary>
        /// <param name="callback">事件回调</param>
        public void Reset(EventCallback1 callback)
        {
            _bridge.Remove(callback);
            _bridge.Add(callback);
        }

        /// <summary>
        /// 设置事件监听器。
        /// </summary>
        /// <param name="callback">事件回调 </param>
        public void Set(EventCallback1 callback)
        {
            _bridge.Clear();
            if (callback != null)
                _bridge.Add(callback);
        }

        /// <summary>
        /// 设置事件监听器。
        /// </summary>
        /// <param name="callback">事件回调</param>
#if FAIRYGUI_TOLUA
        [NoToLua]
#endif
        public void Set(EventCallback0 callback)
        {
            _bridge.Clear();
            if (callback != null)
                _bridge.Add(callback);
        }

#if FAIRYGUI_TOLUA
        /// <summary>
        /// 添加事件监听器。
        /// </summary>
        /// <param name="func">Lua函数</param>
        /// <param name="self">Lua表</param>
        public void Add(LuaFunction func, LuaTable self)
        {
            _bridge.Add(func, self);
        }

        /// <summary>
        /// 添加事件监听器。
        /// </summary>
        /// <param name="func">Lua函数</param>
        /// <param name="self">GComponent</param>
        public void Add(LuaFunction func, GComponent self)
        {
            _bridge.Add(func, self);
        }

        /// <summary>
        /// 移除事件监听器。
        /// </summary>
        /// <param name="func">Lua函数</param>
        /// <param name="self">Lua表</param>
        public void Remove(LuaFunction func, LuaTable self)
        {
            _bridge.Remove(func, self);
        }

        /// <summary>
        /// 移除事件监听器。
        /// </summary>
        /// <param name="func">Lua函数</param>
        /// <param name="self">GComponent</param>
        public void Remove(LuaFunction func, GComponent self)
        {
            _bridge.Remove(func, self);
        }

        /// <summary>
        /// 设置事件监听器。
        /// </summary>
        /// <param name="func">Lua函数</param>
        /// <param name="self">Lua表</param>
        public void Set(LuaFunction func, LuaTable self)
        {
            _bridge.Clear();
            if (func != null)
                Add(func, self);
        }

        /// <summary>
        /// 设置事件监听器。
        /// </summary>
        /// <param name="func">Lua函数</param>
        /// <param name="self">GComponent</param>
        public void Set(LuaFunction func, GComponent self)
        {
            _bridge.Clear();
            if (func != null)
                Add(func, self);
        }
#endif

        /// <summary>
        /// 是否为空。  
        /// </summary>
        public bool isEmpty
        {
            get
            {
                return !_bridge.owner.hasEventListeners(_type);
            }
        }

        /// <summary>
        /// 是否正在分发事件。
        /// </summary>
        public bool isDispatching
        {
            get
            {
                return _bridge.owner.isDispatching(_type);
            }
        }

        /// <summary>
        /// 清除事件监听器。
        /// </summary>
        public void Clear()
        {
            _bridge.Clear();
        }

        /// <summary>
        /// 调用事件监听器。
        /// </summary>
        /// <returns>是否阻止事件继续分发   </returns>
        public bool Call()
        {
            return _bridge.owner.InternalDispatchEvent(_type, _bridge, null, null);
        }

        /// <summary>
        /// 调用事件监听器。
        /// </summary>
        /// <param name="data">事件数据</param>
        /// <returns>是否阻止事件继续分发</returns>
        public bool Call(object data)
        {
            return _bridge.owner.InternalDispatchEvent(_type, _bridge, data, null);
        }

        /// <summary>
        /// 冒泡事件。
        /// </summary>
        /// <param name="data">事件数据</param>
        /// <returns>是否阻止事件继续分发</returns>
        public bool BubbleCall(object data)
        {
            return _bridge.owner.BubbleEvent(_type, data);
        }

        /// <summary>
        /// 冒泡事件。
        /// </summary>
        /// <returns>是否阻止事件继续分发</returns>
        public bool BubbleCall()
        {
            return _bridge.owner.BubbleEvent(_type, null);
        }

        /// <summary>
        /// 广播事件。  
        /// </summary>
        /// <param name="data">事件数据</param>
        /// <returns>是否阻止事件继续分发</returns>
        public bool BroadcastCall(object data)
        {
            return _bridge.owner.BroadcastEvent(_type, data);
        }

        /// <summary>
        /// 广播事件。
        /// </summary>
        /// <returns>是否阻止事件继续分发</returns>
        public bool BroadcastCall()
        {
            return _bridge.owner.BroadcastEvent(_type, null);
        }
    }
}
