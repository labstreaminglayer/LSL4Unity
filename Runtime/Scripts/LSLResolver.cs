///<summary>
/// Copy-Pasted the base Resolver class from the LSL4Unity toolbox to correct 
/// for a few bugs in the code. 
/// 
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LSL;


namespace LSL
{
    /// <summary>
    /// Encapsulates the lookup logic for LSL streams with an event based appraoch
    /// your custom stream inlet implementations could be subscribed to the OnStreamFound
    /// </summary>
    public class LSLResolver : MonoBehaviour
    {

        public List<StreamInfo> knownStreams = new List<StreamInfo>(); // modified by GD to instantiate the list variable.
        
        public float forgetStreamAfter = 1.0f;

        private ContinuousResolver resolver;

        private bool resolve = false;

        public delegate void StreamFound(StreamInfo wrapper);
        public StreamFound OnStreamFound;

        public delegate void StreamLost(StreamInfo wrapper);
        public StreamLost OnStreamLost;

        public bool Resolve
        {
            get { return resolve; }
            set {  }
        }

        // Modified by GD, put the contents of the Start function here. Since we script the addition of the components, 
        // we need to wait for the listeners to be properly configured before starting the stream resolution.
        public void Run()
        {
            resolve = true;
            resolver = new ContinuousResolver(forgetStreamAfter);
            StartCoroutine(resolveContinuously());
        }

        public void Stop()
        {
            resolve = false;
        }

        public bool IsStreamAvailable(out StreamInfo info, string streamName = "", string streamType = "", string hostName = "")
        {
            var result = knownStreams.Where(i =>

            (streamName == "" || i.name().Equals(streamName)) &&
            (streamType == "" || i.type().Equals(streamType)) &&
            (hostName == "" || i.type().Equals(hostName))
            );

            if (result.Any())
            {
                info = result.First();
                return true;
            }
            else
            {
                info = null;
                return false;
            }
        }

        private IEnumerator resolveContinuously()
        {
            while (resolve)
            {
                var results = resolver.results();

                foreach (var item in knownStreams)
                {
                    if (!results.Any(r => r.name().Equals(item.name())))
                    {
                        OnStreamLost?.Invoke(item);
                    }
                }

                // remove lost streams from cache
                knownStreams.RemoveAll(s => !results.Any(r => r.name().Equals(s.name())));

                // add new found streams to the cache
                foreach (var item in results)
                {
                    if (!knownStreams.Any(s => s.name() == item.name() ))
                    {
                        Debug.Log(string.Format("Found new Stream {0}", item.name()));

                        knownStreams.Add(item);
                        OnStreamFound?.Invoke(item);
                    }
                }

                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return null;
        }
    }
}
