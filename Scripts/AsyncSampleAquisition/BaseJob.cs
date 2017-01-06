using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using LSL;
using System;

namespace Assets.LSL4Unity.Scripts.AsyncSampleAquisition
{
    /// <summary>
    /// Abstraction for asynch thread based sample aquisition
    /// </summary>
    /// <typeparam name="T">Sample Type</typeparam>
    /// <typeparam name="C">Cache implementation (Arrays, Collections etc.)</typeparam>
    public abstract class ABaseJob<T, C> where C : class, IEnumerable<T[]>, new()
    {

        private object lockObject = new object();
        private Thread thread;

        protected liblsl.StreamInlet inlet;
        protected C cache;

        protected double timeOut = 0.0;

        public bool HasData
        {
            get
            {
                lock (lockObject)
                {
                    return cache.Any();
                }
            }
        }

        public void GetData(out C cacheContent)
        {
            lock (lockObject)
            {
                cacheContent = cache.ToArray() as C;
                cache = new C();
            }
        }

        public void Start(liblsl.StreamInlet inlet, double aquisitionTimeOut = 0.0)
        {
            this.inlet = inlet;

            this.timeOut = aquisitionTimeOut;

            thread = new Thread(new ThreadStart(pullAsync));

            thread.Start();
        }

        protected abstract void Initialize();

        protected abstract void pullAsync();

    }
}