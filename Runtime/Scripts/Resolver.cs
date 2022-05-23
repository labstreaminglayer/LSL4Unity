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


namespace LSL4Unity.Utils
{
    /// <summary>
    /// Encapsulates the lookup logic for LSL streams with an event based appraoch
    /// your custom stream inlet implementations could be subscribed to the OnStreamFound
    /// </summary>
    public class Resolver : MonoBehaviour
    {
        public List<StreamInfo> knownStreams = new List<StreamInfo>();
        // public List<StreamInfoWrapper> knownStreams = new List<StreamInfoWrapper>;

        public float forgetStreamAfter = 1.0f;

        private ContinuousResolver resolver;

        public delegate void StreamFound(StreamInfo streamInfo);  // Declare callback signature when stream found.
        public StreamFound OnStreamFound;                         // delegate instance to hold callbacks.

        public delegate void StreamLost(StreamInfo streamInfo);
        public StreamLost OnStreamLost;

        public bool Resolve
        {
            get { return (OnStreamFound != null || OnStreamLost != null); }
            set {  }
        }

        public void Start()
        {
            resolver = new ContinuousResolver(forgetStreamAfter);
            StartCoroutine(resolveContinuously());
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
            // We don't bother checking the resolver unless we have any registered callbacks.
            //  This gives other objects time to setup and register before streams go into knownStreams!
            while (Resolve)
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
                        // var newStreamInfo = new StreamInfoWrapper(item);
                        knownStreams.Add(item);  // newStreamInfo);
                        OnStreamFound?.Invoke(item);  // newStreamInfo);
                    }
                }

                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return null;
        }
    }
}
