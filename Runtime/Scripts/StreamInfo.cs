using System;
using UnityEngine.Events;
using LSL;


namespace LSL4Unity.Utils
{ 
   [Serializable]
    public class LSLStreamInfoWrapper
    {
        public string Name;

        public string Type;

        private StreamInfo item;
        private readonly string streamUID;

        private readonly int channelCount;
        private readonly string sessionId;
        private readonly string sourceID;
        private readonly double dataRate;
        private readonly string hostName;
        private readonly int streamVersion;

        public LSLStreamInfoWrapper(StreamInfo streamInfo)
        {
            this.item = streamInfo;
            Name = item.name();
            Type = item.type();
            channelCount = item.channel_count();
            streamUID = item.uid();
            sessionId = item.session_id();
            sourceID = item.source_id();
            dataRate = item.nominal_srate();
            hostName = item.hostname();
            streamVersion = item.version();
        }

        public StreamInfo Item
        {
            get
            {
                return item;
            }
        }

        public string StreamUID
        {
            get
            {
                return streamUID;
            }
        }

        public int ChannelCount
        {
            get
            {
                return channelCount;
            }
        }

        public string SessionId
        {
            get
            {
                return sessionId;
            }
        }

        public string SourceID
        {
            get
            {
                return sourceID;
            }
        }

        public string HostName
        {
            get
            {
                return hostName;
            }
        }

        public double DataRate
        {
            get
            {
                return dataRate;
            }
        }

        public int StreamVersion
        {
            get
            {
                return streamVersion;
            }
        }
    }

    [Serializable]
    public class StreamEvent : UnityEvent<LSLStreamInfoWrapper> { }
}
