using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    float _timer = 0;

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer < 1.0f) 
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 0.5f, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y- Time.deltaTime * 0.5f, transform.position.z);
        if (_timer > 2.0f)
            _timer = 0.0f;
    }
}
