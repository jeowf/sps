using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTime = 1f;

    void Awake()
    {
        StartCoroutine(DestroyThis(destroyTime));
    }

    private IEnumerator DestroyThis(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }
}
