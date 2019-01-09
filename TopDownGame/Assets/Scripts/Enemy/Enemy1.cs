using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : Enemy {

	// Use this for initialization
	void Start () {
        HP = 100;
        AMMO = 40;
        MOVEMENT = 10;
        ATTACKRANGE = 200;
        AGENT = GetComponent<NavMeshAgent>();
        FIRERATE = 2.0f;
        BURSTAMOUNT = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0 || ISDEAD)
        {
            if (Random.Range(0, 100) > 85)
                Player.GetComponent<PlayerClick>().ResetEnergy();
            if(!IsTutorial) Player.GetComponent<PlayerClick>().AddScore();
            Destroy(this.gameObject);
        }
        BasicMove(Time.deltaTime);
	}

    override public void Shoot()
    {
        ShootOnce(0);
    }
}
