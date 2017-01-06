using LSL;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.LSL4Unity.Scripts.AbstractInlets
{
    public abstract class ABaseInlet : MonoBehaviour
    {
        public string StreamName;
        
        public string StreamType;
        
        protected liblsl.StreamInlet inlet;

        protected int expectedChannels;

        protected Resolver resolver;

        /// <summary>
        /// Call this method when your inlet implementation got created at runtime
        /// </summary>
        protected virtual void registerAndLookUpStream()
        {
            resolver = FindObjectOfType<Resolver>();

            resolver.onStreamFound.AddListener(new UnityAction<LSLStreamInfoWrapper>(AStreamIsFound));

            resolver.onStreamLost.AddListener(new UnityAction<LSLStreamInfoWrapper>(AStreamGotLost));
            
            if (resolver.knownStreams.Any(isTheExpected))
            {
                var stream = resolver.knownStreams.First(isTheExpected);
                AStreamIsFound(stream);
            }
        }

        /// <summary>
        /// Callback method for the Resolver gets called each time the resolver found a stream
        /// </summary>
        /// <param name="stream"></param>
        public virtual void AStreamIsFound(LSLStreamInfoWrapper stream)
        {
            if (!isTheExpected(stream))
                return;

            Debug.Log(string.Format("LSL Stream {0} found for {1}", stream.Name, name));

            inlet = new LSL.liblsl.StreamInlet(stream.Item);
            expectedChannels = stream.ChannelCount;

            OnStreamAvailable();
        }

        /// <summary>
        /// Callback method for the Resolver gets called each time the resolver misses a stream within its cache
        /// </summary>
        /// <param name="stream"></param>
        public virtual void AStreamGotLost(LSLStreamInfoWrapper stream)
        {
            if (!isTheExpected(stream))
                return;

            Debug.Log(string.Format("LSL Stream {0} Lost for {1}", stream.Name, name));

            OnStreamLost();
        }

        protected virtual bool isTheExpected(LSLStreamInfoWrapper stream)
        {
            bool predicate = StreamName.Equals(stream.Name);
            predicate &= StreamType.Equals(stream.Type);

            return predicate;
        }

        protected abstract void pullSamples();

        protected virtual void OnStreamAvailable()
        {
            // base implementation may not decide what happens when the stream gets available
            throw new NotImplementedException("Please override this method in a derived class!");
        }

        protected virtual void OnStreamLost()
        {
            // base implementation may not decide what happens when the stream gets lost
            throw new NotImplementedException("Please override this method in a derived class!");
        }
    }

    public abstract class InletFloatSamples : ABaseInlet
    {
        protected abstract void Process(float[] newSample, double timeStamp);

        protected float[] sample;

        protected override void pullSamples()
        {
            sample = new float[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }
    }

    public abstract class InletDoubleSamples : ABaseInlet
    {
        protected abstract void Process(double[] newSample, double timeStamp);

        protected double[] sample;

        protected override void pullSamples()
        {
            sample = new double[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }
    }

    public abstract class InletIntSamples : ABaseInlet
    {
        protected abstract void Process(int[] newSample, double timeStamp);

        protected int[] sample;

        protected override void pullSamples()
        {
            sample = new int[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }
    }

    public abstract class InletCharSamples : ABaseInlet
    {
        protected abstract void Process(char[] newSample, double timeStamp);

        protected char[] sample;

        protected override void pullSamples()
        {
            sample = new char[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }
    }

    public abstract class InletStringSamples : ABaseInlet
    {
        protected abstract void Process(String[] newSample, double timeStamp);

        protected String[] sample;

        protected override void pullSamples()
        {
            sample = new String[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }
    }

    public abstract class InletShortSamples : ABaseInlet
    {
        protected abstract void Process(short[] newSample, double timeStamp);

        protected short[] sample;

        protected override void pullSamples()
        {
            sample = new short[expectedChannels];

            try
            {
                double lastTimeStamp = inlet.pull_sample(sample, 0.0f);

                if (lastTimeStamp != 0.0)
                {
                    // do not miss the first one found
                    Process(sample, lastTimeStamp);
                    // pull as long samples are available
                    while ((lastTimeStamp = inlet.pull_sample(sample, 0.0f)) != 0)
                    {
                        Process(sample, lastTimeStamp);
                    }

                }
            }
            catch (ArgumentException aex)
            {
                Debug.LogError("An Error on pulling samples deactivating LSL inlet on...", this);
                this.enabled = false;
                Debug.LogException(aex, this);
            }

        }
    }


}
