using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;

namespace LSL4Unity.Utils
{
    public abstract class ABaseOutlet<TData> : MonoBehaviour
    {
        public string StreamName;
        public string StreamType;
        public MomentForSampling moment;
        public bool IrregularRate = false;
        private bool UniqueFromInstanceId = true;

        public abstract List<string> ChannelNames { get; }
        public virtual int ChannelCount { get { return ChannelNames.Count; } }
        public abstract channel_format_t Format { get; }

        protected StreamOutlet outlet;
        protected TData[] sample;
        // A singleton of a TimeSync object can ensure that all pushes that happen on the same moment (update/fixed/late etc)
        // on the same frame will have the same timestamp.
        private TimeSync timeSync;

        // Add an XML element for each channel. The automatic version adds only channel labels. Override to add unit, location, etc.
        protected virtual void FillChannelsHeader(XMLElement channels)
        {
            foreach (var chanName in ChannelNames)
            {
                XMLElement chan = channels.append_child("channel");
                chan.append_child_value("label", chanName);
            }
        }

        protected virtual void Start()
        {
            // TODO: Check StreamName, StreamType, and moment are not null.

            timeSync = gameObject.GetComponent<TimeSync>();
            sample = new TData[ChannelCount];

            var hash = new Hash128();
            hash.Append(StreamName);
            hash.Append(StreamType);
            hash.Append(moment.ToString());
            if (UniqueFromInstanceId)
                hash.Append(gameObject.GetInstanceID());
            ExtendHash(hash);

            double dataRate = IrregularRate ? LSL.LSL.IRREGULAR_RATE : LSLCommon.GetSamplingRateFor(moment);
            StreamInfo streamInfo = new StreamInfo(StreamName, StreamType, ChannelCount, dataRate, Format, hash.ToString());

            // Build XML header. See xdf wiki for recommendations: https://github.com/sccn/xdf/wiki/Meta-Data
            XMLElement acq_el = streamInfo.desc().append_child("acquisition");
            acq_el.append_child_value("manufacturer", "LSL4Unity");
            XMLElement channels = streamInfo.desc().append_child("channels");
            FillChannelsHeader(channels);
            
            outlet = new StreamOutlet(streamInfo);
        }

        // Override to extend hash
        protected virtual void ExtendHash(Hash128 hash) { }

        protected abstract bool BuildSample();
        protected abstract void pushSample(double timestamp = 0);

        // Update is called once per frame
        protected virtual void FixedUpdate()
        {
            if (moment == MomentForSampling.FixedUpdate && outlet != null)
            {
                if (BuildSample())
                    pushSample((timeSync != null) ? timeSync.FixedUpdateTimeStamp : 0.0);
            }
        }

        protected virtual void Update()
        {
            if (outlet != null)
            {
                if (BuildSample())
                {
                    if (moment == MomentForSampling.Update)
                    {
                        pushSample((timeSync != null) ? timeSync.UpdateTimeStamp : 0.0);
                    }
                    else if (moment == MomentForSampling.EndOfFrame)
                    {
                        // Send as close to render-time as possible. Use this to log stimulus events.
                        StartCoroutine(PushAfterRendered());
                    }
                }
            }
        }

        protected virtual void LateUpdate()
        {
            if (moment == MomentForSampling.LateUpdate && outlet != null)
            {
                if (BuildSample())
                    pushSample((timeSync != null) ? timeSync.LateUpdateTimeStamp : 0.0);
            }
        }

        IEnumerator PushAfterRendered()
        {
            yield return new WaitForEndOfFrame();
            pushSample();
            yield return null;
        }
    }

    public abstract class AFloatOutlet : ABaseOutlet<float>
    {
        public override channel_format_t Format { get { return channel_format_t.cf_float32; } }
        protected override void pushSample(double timestamp = 0)
        {
            if (outlet == null)
                return;
            outlet.push_sample(sample, timestamp);
        }
    }

    public abstract class ADoubleOutlet : ABaseOutlet<double>
    {
        public override channel_format_t Format { get { return channel_format_t.cf_double64; } }
        protected override void pushSample(double timestamp = 0)
        {
            if (outlet == null)
                return;
            outlet.push_sample(sample, timestamp);
        }
    }

    public abstract class AIntOutlet : ABaseOutlet<int>
    {
        public override channel_format_t Format { get { return channel_format_t.cf_int32; } }
        protected override void pushSample(double timestamp = 0)
        {
            if (outlet == null)
                return;
            outlet.push_sample(sample, timestamp);
        }
    }

    public abstract class ACharOutlet : ABaseOutlet<char>
    {
        public override channel_format_t Format { get { return channel_format_t.cf_int8; } }
        protected override void pushSample(double timestamp = 0)
        {
            if (outlet == null)
                return;
            outlet.push_sample(sample, timestamp);
        }
    }

    public abstract class AStringOutlet : ABaseOutlet<string>
    {
        public override channel_format_t Format { get { return channel_format_t.cf_string; } }
        protected override void pushSample(double timestamp = 0)
        {
            if (outlet == null)
                return;
            outlet.push_sample(sample, timestamp);
        }
    }

    public abstract class AShortOutlet : ABaseOutlet<short>
    {
        public override channel_format_t Format { get { return channel_format_t.cf_int16; } }
        protected override void pushSample(double timestamp = 0)
        {
            if (outlet == null)
                return;
            outlet.push_sample(sample, timestamp);
        }
    }
}