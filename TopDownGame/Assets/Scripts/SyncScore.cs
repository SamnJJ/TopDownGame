using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncScore : MonoBehaviour {
    [SerializeField]
    private Text Score = null;

    [SerializeField]
    private Text Score2 = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Score2.text = Score.text;
	}
}
