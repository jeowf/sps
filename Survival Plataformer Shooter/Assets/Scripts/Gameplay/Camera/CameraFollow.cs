using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region Inspector Members

    public bool enable = true;
    public Transform target;
    public float smoothTime = 1f;

    #endregion

    #region Monobehaviour Methods


    void LateUpdate()
    {
        Vector3 to = target.transform.position;
        to.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, to, Time.deltaTime * smoothTime);
    }

    #endregion
}
