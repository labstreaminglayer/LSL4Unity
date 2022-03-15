using UnityEngine;
using System;


namespace LSL4Unity
{
    /// <summary>
    /// This attribute is used to define specific script execution orders when necessary!
    /// Example: LSLTimeSync -> should be called at the beginning of each frame before other scripts use its properties.
    /// Original from Unity forum: https://forum.unity3d.com/threads/script-execution-order-manipulation.130805/
    /// </summary>
    public class ScriptOrder : Attribute
    {
        public int order;

        public ScriptOrder(int order)
        {
            this.order = order;
        }
    }
}

