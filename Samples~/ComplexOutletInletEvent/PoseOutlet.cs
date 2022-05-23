using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;
using LSL4Unity.Utils;

namespace LSL4Unity.Samples.Complex
{
    public enum PoseFormat { PosEul6D, PosQuat7D, Transform12D }

    public class PoseOutlet : AFloatOutlet
    {
        public PoseFormat transformFormat = PoseFormat.PosQuat7D;

        public void Reset()
        {
            StreamName = "Unity.Pose";
            StreamType = "Unity.Transform";
            moment = MomentForSampling.FixedUpdate;
        }

        public override List<string> ChannelNames
        {
            get
            {
                List<string> chanNames = new List<string>();
                if ((transformFormat == PoseFormat.PosEul6D) || (transformFormat == PoseFormat.PosQuat7D))
                {
                    chanNames.AddRange(new string[] { "PosX", "PosY", "PosZ" });

                    if (transformFormat == PoseFormat.PosEul6D)
                    {
                        chanNames.AddRange(new string[] { "Pitch", "Yaw", "Roll" });
                    }
                    else
                    {
                        chanNames.AddRange(new string[] { "RotX", "RotY", "RotZ", "RotW" });
                    }
                }
                else if (transformFormat == PoseFormat.Transform12D)
                {
                    var pose = gameObject.transform.localToWorldMatrix;
                    for (int row_ix = 0; row_ix < 3; row_ix++)
                    {
                        for (int col_ix = 0; col_ix < 4; col_ix++)
                        {
                            chanNames.Add(string.Format("{0},{1}", row_ix, col_ix));
                        }
                    }
                }
                return chanNames;
            }
        }

        protected override void ExtendHash(Hash128 hash)
        {
            hash.Append(transformFormat.ToString());
        }

        protected override bool BuildSample()
        {
            if ((transformFormat == PoseFormat.PosEul6D) || (transformFormat == PoseFormat.PosQuat7D)) {
                var position = gameObject.transform.position;
                sample[0] = position.x;
                sample[1] = position.y;
                sample[2] = position.z;
                
                if (transformFormat == PoseFormat.PosEul6D)
                {
                    var rotation = gameObject.transform.eulerAngles;
                    sample[3] = rotation.x;
                    sample[4] = rotation.y;
                    sample[5] = rotation.z;
                }
                else
                {
                    var rotation = gameObject.transform.rotation;
                    sample[3] = rotation.x;
                    sample[4] = rotation.y;
                    sample[5] = rotation.z;
                    sample[6] = rotation.w;
                }
            }
            else if (transformFormat == PoseFormat.Transform12D)
            {
                var pose = gameObject.transform.localToWorldMatrix;
                for (int row_ix = 0; row_ix < 3; row_ix++)
                {
                    for (int col_ix = 0; col_ix < 4; col_ix++)
                    {
                        sample[col_ix + 4 * row_ix] = pose[row_ix, col_ix];
                    }
                }
            }
            return true;
        }
    }
}