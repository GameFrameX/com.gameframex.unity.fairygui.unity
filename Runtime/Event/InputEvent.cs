using UnityEngine;
#if FAIRYGUI_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    public class InputEvent
    {
        /// <summary>
        /// x position in stage coordinates.
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public float x { get; internal set; }

        /// <summary>
        /// y position in stage coordinates.
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public float y { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public KeyCode keyCode { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public char character { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public EventModifiers modifiers { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public float mouseWheelDelta { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int touchId { get; internal set; }

        /// <summary>
        /// -1-none,0-left,1-right,2-middle
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int button { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [UnityEngine.Scripting.Preserve]
        public int clickCount { get; internal set; }

        /// <summary>
        /// Duraion of holding the button. You can read this in touchEnd or click event.
        /// </summary>
        /// <value></value>
        [UnityEngine.Scripting.Preserve]
        public float holdTime { get; internal set; }

        [UnityEngine.Scripting.Preserve]
        public InputEvent()
        {
            touchId = -1;
            x = 0;
            y = 0;
            clickCount = 0;
            keyCode = KeyCode.None;
            character = '\0';
            modifiers = 0;
            mouseWheelDelta = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public Vector2 position
        {
            get { return new Vector2(x, y); }
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool isDoubleClick
        {
            get { return clickCount > 1 && button == 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool ctrlOrCmd
        {
            get { return ctrl || command; }
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool ctrl
        {
            get
            {
#if FAIRYGUI_INPUT_SYSTEM
                Keyboard keyboard = Keyboard.current;
                return keyboard != null && (keyboard.leftCtrlKey.isPressed || keyboard.rightCtrlKey.isPressed);
#else
                return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool shift
        {
            get
            {
#if FAIRYGUI_INPUT_SYSTEM
                Keyboard keyboard = Keyboard.current;
                return keyboard != null && (keyboard.leftShiftKey.isPressed || keyboard.rightShiftKey.isPressed);
#else
                return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool alt
        {
            get
            {
#if FAIRYGUI_INPUT_SYSTEM
                Keyboard keyboard = Keyboard.current;
                return keyboard != null && (keyboard.leftAltKey.isPressed || keyboard.rightAltKey.isPressed);
#else
                return Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool command
        {
            get
            {
                //In win, as long as the win key and other keys are pressed at the same time, the getKey will continue to return true. So it can only be shielded.
                if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
                {
#if FAIRYGUI_INPUT_SYSTEM
                    Keyboard keyboard = Keyboard.current;
                    return keyboard != null && (keyboard.leftCommandKey.isPressed || keyboard.rightCommandKey.isPressed);
#else
                    return Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand);
#endif
                }
                else
                {
                    return false;
                }
            }
        }
    }
}