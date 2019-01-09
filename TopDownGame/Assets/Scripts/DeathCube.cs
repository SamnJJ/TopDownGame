using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour {

    [SerializeField]
    private GameObject RespawnLoc = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.transform.position = RespawnLoc.transform.position;
            other.gameObject.GetComponent<PlayerClick>().Damage();
        }
    }
}
