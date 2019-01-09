using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGateMove : MonoBehaviour {

    public Transform _targetTransform;
    public ParticleSystem _closeMainEmitter;
    public ParticleSystem _openMainEmitter;

    public float _openHeight;
    public float _openSpeed = 5.0f;
    public float _closeSpeed = 10.0f;

    private List<GameObject> _targets = new List<GameObject>();
    private bool _needsToOpen = false;
    private float _startHeight;

    private bool _needsToPlayEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
        {
            GameObject go = other.gameObject;
            if (!_targets.Contains(go))
            {
                _targets.Add(go);
                _needsToOpen = true;
                if(_openMainEmitter != null && _closeMainEmitter != null) _needsToPlayEffect = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
        {
            GameObject go = other.gameObject;
            _targets.Remove(go);
            if(_targets.Count == 0)
            {
                _needsToOpen = false;
                if (_openMainEmitter != null && _closeMainEmitter != null) _needsToPlayEffect = true;
            }
        }
    }
 
	// Use this for initialization
	void Start () {
        _startHeight = _targetTransform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

        if (_needsToOpen == false )
        {
            if (_targetTransform.position.y >= _startHeight)
            {
                // the gate needs to close
                _targetTransform.Translate(new Vector3(0, 0, -_closeSpeed * Time.deltaTime));
                return;
            }
            if (_needsToPlayEffect)
            {
                _needsToPlayEffect = false;
                _closeMainEmitter.Play();
            }
        }

        if(_needsToOpen)
        {
            if (_needsToPlayEffect && _targetTransform.position.y <= _startHeight)
            {
                _needsToPlayEffect = false;
                _openMainEmitter.Play();
            }

            if (_targetTransform.position.y <= _openHeight + _startHeight)
            {
                // the gate needs to open
                _targetTransform.Translate(new Vector3(0, 0, _openSpeed * Time.deltaTime));
                return;
            }
           
        }
	}

}
