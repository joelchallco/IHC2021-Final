using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public Quaternion startQuaternion;
    public float lerpTime = 1;

    public bool rotate ;

    void Update()
    {
        //if(rotate)
        //    transform.rotation = Quaternion.Lerp(transform.rotation, startQuaternion, Time.deltaTime * lerpTime);

    }


    void snapRotation()
    {
        //transform.rotation = startQuaternion;
    }

}
