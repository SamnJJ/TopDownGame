using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialScript : MonoBehaviour {
    [SerializeField]
    private List<bool> check = new List<bool>();

    [SerializeField]
    private GameObject obj = null;

    [SerializeField]
    private GameObject des = null;  



    private bool _playerIsIn = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (check[0] && _playerIsIn)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                Destroy(obj, 2);
            }
        }

        if (check[1] && _playerIsIn)
        {
            Debug.Log(!des);
            if (!des)
            {
                Destroy(obj, 2);
            }
        }

        if (check[2] && _playerIsIn)
        {
            if (!des)
            {
                Destroy(obj, 2);
            }
        }

        if (check[3] && _playerIsIn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(obj, 2);
            }
        }
        if (check[4] && _playerIsIn)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _playerIsIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _playerIsIn = false;
        }

    }

}
