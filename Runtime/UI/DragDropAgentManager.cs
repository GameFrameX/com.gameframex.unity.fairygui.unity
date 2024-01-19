namespace FairyGUI
{
    /// <summary>
    /// Helper for drag and drop.
    /// 这是一个提供特殊拖放功能的功能类。与GObject.draggable不同，拖动开始后，他使用一个替代的图标作为拖动对象。
    /// 当玩家释放鼠标/手指，目标组件会发出一个onDrop事件。
    /// </summary>
    public sealed class DragDropAgentManager
    {
        private GObject _agent;
        private object _sourceData;
        private GObject _source;

        private static DragDropAgentManager _inst;

        public static DragDropAgentManager Instance
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new DragDropAgentManager();
                }

                return _inst;
            }
        }

        /// <summary>
        /// Loader object for real dragging.
        /// 用于实际拖动的对象。你可以根据实际情况设置对象的大小，对齐等。
        /// </summary>
        public GObject DragAgent
        {
            get { return _agent; }
        }

        /// <summary>
        /// Is dragging?
        /// 返回当前是否正在拖动。
        /// </summary>
        public bool Dragging
        {
            get { return _agent.parent != null; }
        }

        /// <summary>
        /// Start dragging.
        /// 开始拖动。
        /// </summary>
        /// <param name="source">Source object. This is the object which initiated the dragging.</param>
        /// <param name="newAgent">Icon to be used as the dragging sign.</param>
        /// <param name="sourceData">Custom data. You can get it in the onDrop event data.</param>
        /// <param name="onDragEndCallback"></param>
        /// <param name="touchPointID">Copy the touchId from InputEvent to here, if has one.</param>
        //public void StartDrag(GObject source, string icon, object sourceData, int touchPointID = -1)
        public void StartDrag(GObject source, GObject newAgent, object sourceData, EventCallback1 onDragEndCallback, int touchPointID = -1)
        {
            if (_agent != null && _agent.parent != null)
            {
                return;
            }

            _sourceData = sourceData;
            _source = source;
            _agent = newAgent;
            SetAgent(onDragEndCallback);
            GRoot.inst.AddChild(_agent);
            _agent.xy = GRoot.inst.GlobalToLocal(Stage.inst.GetTouchPosition(touchPointID));
            _agent.StartDrag(touchPointID);
        }

        void SetAgent(EventCallback1 callback)
        {
            _agent.SetHome(GRoot.inst);
            if (_agent.touchable)
            {
                _agent.touchable = false;
            } //important

            if (!_agent.draggable)
            {
                _agent.draggable = true;
            }

            _agent.SetPivot(0.5f, 0.5f, true);
            //_agent.align = AlignType.Center;
            //_agent.verticalAlign = VertAlignType.Middle;
            _agent.sortingOrder = int.MaxValue;
            _agent.onDragEnd.Clear();
            _agent.onDragEnd.Add(__dragEnd);
            _agent.onDragEnd.Add(callback);
        }

        /// <summary>
        /// 开始拖拽
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceData">拖动的数据</param>
        /// <param name="onDragEndCallback">拖动结束回调</param>
        /// <param name="touchPointID"></param>
        /// <param name="scale">拖动对象的缩放</param>
        public void StartDrag(GObject source, object sourceData, EventCallback1 onDragEndCallback = null, int touchPointID = -1, float scale = 1.5f)
        {
            if (source.parent != null)
            {
                source.RemoveFromParent();
            }

            source.SetHome(GRoot.inst);
            source.touchable = false; //important
            source.draggable = true;
            source.SetScale(scale, scale);
            source.SetPivot(0.5f, 0.5f, true);
            source.sortingOrder = int.MaxValue;

            source.onDragEnd.Add(() =>
            {
                source.RemoveFromParent();
                _sourceData = null;
                _source = null;
                GObject obj = GRoot.inst.touchTarget;
                while (obj != null)
                {
                    if (obj.hasEventListeners(EventName.onDrop))
                    {
                        obj.RequestFocus();
                        obj.DispatchEvent(EventName.onDrop, sourceData, source);
                        return;
                    }

                    obj = obj.parent;
                }
            });
            if (onDragEndCallback != null)
            {
                source.onDragEnd.Add(onDragEndCallback);
            }

            _sourceData = sourceData;
            _source = source;
            GRoot.inst.AddChild(source);
            source.xy = GRoot.inst.GlobalToLocal(Stage.inst.GetTouchPosition(touchPointID));
            source.StartDrag(touchPointID);
        }

        /// <summary>
        /// Cancel dragging.
        /// 取消拖动。
        /// </summary>
        public void Cancel()
        {
            if (_agent.parent != null)
            {
                _agent.StopDrag();
                GRoot.inst.RemoveChild(_agent);
                _sourceData = null;
                //_source = null;
                //_agent = null;
                _agent.onDragEnd.Clear();
            }
        }

        private void __dragEnd(EventContext evt)
        {
            if (_agent.parent == null) //cancelled
                return;

            GRoot.inst.RemoveChild(_agent);

            object sourceData = _sourceData;
            GObject source = _source;
            _sourceData = null;
            _source = null;

            GObject obj = GRoot.inst.touchTarget;
            while (obj != null)
            {
                if (obj.hasEventListeners(EventName.onDrop))
                {
                    obj.RequestFocus();
                    obj.DispatchEvent(EventName.onDrop, sourceData, source);
                    return;
                }

                obj = obj.parent;
            }

            _agent.onDragStart.Clear();
            _agent.onDragMove.Clear();
            _agent.onDragEnd.Clear();
            _agent = null;
        }
    }
}