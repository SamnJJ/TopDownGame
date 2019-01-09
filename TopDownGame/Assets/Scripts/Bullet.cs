using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag.Equals("Player") && (other.tag.Equals("Enemy1") || other.tag.Equals("Enemy2")))
        {
            var e = other.gameObject.GetComponent<Enemy>();
            e.HP -= 50;
            if (e.HP <= 0) e.IGNORE = true;
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (this.tag.Equals("Respawn") && other.tag.Equals("Player"))
        {
            var p = other.gameObject.GetComponent<PlayerClick>();
            if (!p)
                return;
            p.Damage();
                
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
