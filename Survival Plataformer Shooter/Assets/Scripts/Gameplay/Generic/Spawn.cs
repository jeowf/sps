using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float baseTime = 3f;
    public float decrementTimeRate = 1f;
    public float minTime = 0.2f;
    
    public GameObject spawnObject;

    private bool _canSpawn = true;

    private float _time;

    void Awake()
    {
        _time = baseTime;
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime * decrementTimeRate;
        if (_time < minTime)
            _time = minTime;
        StartCoroutine(SpawnObject(_time));
    }

    private IEnumerator SpawnObject(float waitTime)
    {

        if (_canSpawn)
        {
            _canSpawn = false;
            yield return new WaitForSeconds(waitTime);
            GameObject obj = GameObject.Instantiate(spawnObject);
            obj.transform.position = transform.position;
            _canSpawn = true;

        }
            
            
        
    }
}
