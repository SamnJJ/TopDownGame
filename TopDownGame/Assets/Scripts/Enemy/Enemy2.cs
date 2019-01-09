using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : Enemy {

	// Use this for initialization
	void Start () {
        HP = 150;
        AMMO = 20;
        MOVEMENT = 5;
        ATTACKRANGE = 50;
        AGENT = GetComponent<NavMeshAgent>();
        FIRERATE = 2.0f;
        BURSTAMOUNT = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0 || ISDEAD)
        {
            if (Random.Range(0, 100) > 90)
                Player.GetComponent<PlayerClick>().ResetEnergy();
            if (!IsTutorial)Player.GetComponent<PlayerClick>().AddScore();
            Destroy(this.gameObject);
        }
        BasicMove(Time.deltaTime);
	}

    override public void Shoot()
    {
        ShootOnce(0);
        ShootOnce(45);
        ShootOnce(-45);
    }
}
