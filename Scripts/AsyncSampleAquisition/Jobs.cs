using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Assets.LSL4Unity.Scripts.AsyncSampleAquisition
{
    public abstract class BaseSampleJob<T> : ABaseJob<T, Queue<T[]>>
    {
        protected T[] tempSample;
        private int expectedSampleCount = 0;

        protected override void Initialize()
        {
            expectedSampleCount = inlet.info().channel_count();

            if (cache == null)
                cache = new Queue<T[]>();

            tempSample = new T[expectedSampleCount];
        }

    }


    public abstract class BaseChunkJob<T> : ABaseJob<T, Queue<T[]>>
    {
        protected T[] tempSample;
        private int expectedSampleCount = 0;
        private T[,] sampleBuffer;
        private double[] timeStampBuffer;

        protected override void Initialize()
        {
            expectedSampleCount = inlet.info().channel_count();

            if (cache == null)
                cache = new Queue<T[]>();
            
            tempSample = new T[expectedSampleCount];
        }

    }


    public class FloatSampleJob : BaseSampleJob<float>
    {
        protected override void pullAsync()
        {
            if (inlet.samples_available() > 0)
            {
                double ts = 0.0;
                do
                {
                    ts = inlet.pull_sample(tempSample);
                    cache.Enqueue(tempSample);

                } while (ts != 0.0);
            }

        }
    }
}
