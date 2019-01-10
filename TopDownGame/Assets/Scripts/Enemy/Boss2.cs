using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : Enemy
{
    [SerializeField]
    private GameObject _shield = null;
    private float _timer3 = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        HP = 50;
        AMMO = 1;
        MOVEMENT = 12;
        ATTACKRANGE = 200;
        AGENT = GetComponent<NavMeshAgent>();
        FIRERATE = 1.0f;
        BURSTAMOUNT = 3;
    }

    override public void BasicMove(float dt)
    {
        if (!Player || IsTutorial) return;
        if (IGNORE) return;
        if(AMMO>0) _timer3 += dt;
         if(_timer3 > 5.0f)
        {
            AGENT.isStopped = true;
            transform.LookAt(Player.transform.position);
            CANATTACK = true;

            if (AMMO > 0)
            {
                _timer3 -= FIRERATE;
                Invoke("Shoot", 0.2f);
                Invoke("TurnOffShield", 3.0f);
                Invoke("ResetPower", 5.0f);
            }
        }


    }

    override public void Shoot()
    {
        AMMO--;
        _shield.SetActive(true);
    }

    private void ResetPower()
    {
        AMMO++;
    }
    private void TurnOffShield()
    {
        _shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 || ISDEAD)
        {
            if (Random.Range(0, 100) > 85)
                Player.GetComponent<PlayerClick>().ResetEnergy();
            if (!IsTutorial) Player.GetComponent<PlayerClick>().AddScore();
            Destroy(this.gameObject);
        }
        BasicMove(Time.deltaTime);
    }
}
