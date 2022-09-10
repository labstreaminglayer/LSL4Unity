using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;


namespace LSL4Unity.Utils
{
    public abstract class ABaseInlet<TData> : MonoBehaviour
    {
        public string StreamName;
        public string StreamType;
        // Hook in frame lifecycle in which to pull data.
        public MomentForSampling moment;

        // Set this to true to only pass the last sample in a chunk to Process.
        public bool ProcessLastInChunkOnly = false;

        // Duration, in seconds, of buffer passed to pull_chunk. This must be > than average frame interval.
        double maxChunkDuration = 0.2;

        protected StreamInlet inlet;
        public Resolver resolver;
        protected double[] timestamp_buffer;
        protected TData[,] data_buffer;
        protected TData[] single_sample;
        protected List<string> ChannelNames = new List<string>();
        public virtual int ChannelCount { get { return ChannelNames.Count; } }

        protected virtual void Start()
        {
            registerAndLookUpStream();
        }

        /// <summary>
        /// Call this method when your inlet implementation got created at runtime
        /// </summary>
        protected virtual void registerAndLookUpStream()
        {
            resolver = FindObjectOfType<Resolver>();
            if (resolver == null)
            {
                resolver = gameObject.AddComponent<Resolver>();
            }

            // Check already-present streams.
            if (resolver.knownStreams.Any(isTheExpected))
            {
                var stream = resolver.knownStreams.First(isTheExpected);
                AStreamIsFound(stream);
            }

            // Register callbacks for new streams or lost streams.
            resolver.OnStreamFound += AStreamIsFound;
            resolver.OnStreamLost += AStreamGotLost;
        }

        /// <summary>
        /// Callback method for the Resolver gets called each time the resolver found a stream
        /// </summary>
        /// <param name="stream"></param>
        public virtual void AStreamIsFound(StreamInfo stream_info)
        {
            if (!isTheExpected(stream_info))
                return;

            Debug.Log(string.Format("LSL Stream {0} found for {1}", stream_info.name(), name));

            inlet = new StreamInlet(stream_info);
            int buf_samples = (int)Mathf.Ceil((float)(inlet.info().nominal_srate() * maxChunkDuration));
            int nChannels = inlet.info().channel_count();

            timestamp_buffer = new double[buf_samples];
            data_buffer = new TData[buf_samples, nChannels];
            single_sample = new TData[nChannels];

            XMLElement channels = inlet.info().desc().child("channels");
            XMLElement chan;
            if (!channels.empty())
                chan = channels.first_child();
            else
                chan = channels;
            
            ChannelNames.Clear();
            for (int chan_ix = 0; chan_ix < nChannels; chan_ix++)
            {
                if (!chan.empty())
                {
                    ChannelNames.Add(chan.child_value("label"));
                    chan = chan.next_sibling();
                }
                else
                    // Pad with empty strings to make ChannelNames length == nChannels.
                    ChannelNames.Add("Chan" + chan_ix);
            }

            OnStreamAvailable();
        }

        /// <summary>
        /// Callback method for the Resolver gets called each time the resolver misses a stream within its cache
        /// </summary>
        /// <param name="stream"></param>
        public virtual void AStreamGotLost(StreamInfo stream_info)
        {
            if (!isTheExpected(stream_info))
                return;

            Debug.Log(string.Format("LSL Stream {0} Lost for {1}", stream_info.name(), name));

            OnStreamLost();
        }

        protected virtual bool isTheExpected(StreamInfo stream_info)
        {
            bool predicate = StreamName == "" || StreamName.Equals(stream_info.name());
            predicate &= StreamType == "" || StreamType.Equals(stream_info.type());

            return predicate;
        }

        // Must override for each data type because LSL can't handle Generics.
        protected abstract void pullChunk();

        // Process will be called by one of FixedUpdate, Update, LateUpdate, or a 
        protected abstract void Process(TData[] newSample, double timestamp);

        protected void ProcessChunk(int n_samples)
        {
            if (n_samples == 0)
                return;
            for (int sample_ix = ProcessLastInChunkOnly ? n_samples-1 : 0; sample_ix < n_samples; sample_ix++)
            {
                for (int chan_ix = 0; chan_ix < ChannelCount; chan_ix++)
                {
                    single_sample[chan_ix] = data_buffer[sample_ix, chan_ix];
                }
                Process(single_sample, timestamp_buffer[sample_ix]);
            }
        }

        // Child class should implement a method to do more setup after the target stream is available.
        protected virtual void OnStreamAvailable()
        {
            // base implementation may not decide what happens when the stream gets available
            // throw new NotImplementedException("Please override this method in a derived class!");
        }

        // Child class should implement a method to cleanup any resources that depend on the stream,
        //  and might change if a stream becomes available again but has somehow changed.
        protected virtual void OnStreamLost()
        {
            // base implementation may not decide what happens when the stream gets lost
            // throw new NotImplementedException("Please override this method in a derived class!");
        }

        protected virtual void FixedUpdate()
        {
            if (moment == MomentForSampling.FixedUpdate && inlet != null)
                pullChunk();
        }

        protected virtual void Update()
        {
            if (moment == MomentForSampling.Update && inlet != null)
                pullChunk();
            else if (moment == MomentForSampling.EndOfFrame && inlet != null)
            {
                // Pull and process as close to render-time as possible.
                // No idea why you'd want to use this; it won't affect anything in the game until the next frame.
                StartCoroutine(PullAfterRendered());
            }
        }

        protected virtual void LateUpdate()
        {
            if (moment == MomentForSampling.LateUpdate && inlet != null)
                pullChunk();
        }

        IEnumerator PullAfterRendered()
        {
            yield return new WaitForEndOfFrame();
            pullChunk();
            yield return null;
        }
    }

    public abstract class AFloatInlet : ABaseInlet<float>
    {
        protected override void pullChunk()
        {
            ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
        }
    }

    public abstract class ADoubleInlet : ABaseInlet<double>
    {
        protected override void pullChunk()
        {
            ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
        }
    }

    public abstract class AIntInlet : ABaseInlet<int>
    {
        protected override void pullChunk()
        {
            ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
        }
    }

    public abstract class ACharInlet : ABaseInlet<char>
    {
        protected override void pullChunk()
        {
            ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
        }
    }

    public abstract class InletAStringInlet : ABaseInlet<String>
    {
        protected override void pullChunk()
        {
            ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
        }
    }

    public abstract class AShortInlet : ABaseInlet<short>
    {
        protected override void pullChunk()
        {
            ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
        }
    }

}
