using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LSL4Unity.Scripts
{
    public static class Util
    {


        public static void CheckIfTheRequestedTypeIsAvailable<SampleType>()
        {
            CheckSampleType<SampleType, float>();
            CheckSampleType<SampleType, Double>();
            CheckSampleType<SampleType, Int16>();
            CheckSampleType<SampleType, Int32>();
            CheckSampleType<SampleType, Int64>();
            CheckSampleType<SampleType, int>();
            CheckSampleType<SampleType, string>();
        }


        /// <summary>
        /// Helper methode to ensure that only samples would be used that are actually implemented through LSL
        /// </summary>
        /// <typeparam name="AvailableSampleType"></typeparam>
        /// <typeparam name="RequesteSampleType"></typeparam>
        public static void CheckSampleType<AvailableSampleType, RequesteSampleType>()
        {
            var requestedSampleType = typeof(RequesteSampleType);
            var availableSampleType = typeof(AvailableSampleType);

            if (requestedSampleType != availableSampleType)
            {
                var message = string.Format("The requested sample type '{0}' is currently not supported by LSL!", requestedSampleType.Name);
                throw new NotImplementedException(message);
            }
        }
    }
}
