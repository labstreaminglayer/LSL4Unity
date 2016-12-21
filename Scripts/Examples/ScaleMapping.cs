using Assets.LSL4Unity.Scripts.AbstractInlets;
using UnityEngine;

public class ScaleMapping : AFloatInlet
{
    public Transform targetTransform;

    public bool useX ;
    public bool useY ;
    public bool useZ ;

    protected override void Process(float[] newSample, double timeStamp)
    {
        //Assuming that a sample contains at least 3 values for x,y,z
        float x = useX ? newSample[0] : 1;
        float y = useY ? newSample[1] : 1;
        float z = useZ ? newSample[2] : 1;
        
        // we map the data to the scale factors
        var targetScale = new Vector3(x, y, z);

        // apply the rotation to the target transform
        targetTransform.localScale = targetScale;
    }
}
