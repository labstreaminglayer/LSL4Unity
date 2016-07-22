using Assets.LSL4Unity.Scripts.AbstractInlets;
using UnityEngine;

public class TransformMapping : AFloatInlet
{
    public Transform targetTransform;

    protected override void Process(float[] newSample, double timeStamp)
    {
        //Assuming that a sample contains at least 3 values for x,y,z
        float x = newSample[0];
        float y = newSample[1];
        float z = newSample[2];

        // we map the coordinates to a rotation
        var targetRotation = Quaternion.Euler(x, y, z);

        // apply the rotation to the target transform
        targetTransform.rotation = targetRotation;
    }
}
