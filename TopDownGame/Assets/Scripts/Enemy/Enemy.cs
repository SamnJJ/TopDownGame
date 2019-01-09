using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int HP = 0;
    public int AMMO = 0;
    public float MOVEMENT = 0;
    public bool ISDEAD = false;
    public float ATTACKRANGE = 0;
    public GameObject Player = null;
    public NavMeshAgent AGENT = null;
    public bool CANATTACK = false;
    public GameObject BulletPrefab = null;
    public GameObject GunLoc = null;
    public float FIRERATE = 0.0f;
    public int BURSTAMOUNT = 0;
    public bool IGNORE = false;
    public bool IsTutorial = false;

    private float _timer = 0.0f;

    public void BasicMove(float dt)
    {
        if (!Player || IsTutorial) return;
        if (IGNORE) return;
        if ((Player.transform.position - transform.position).sqrMagnitude > ATTACKRANGE)
        {
            AGENT.isStopped = false;
            AGENT.SetDestination(Player.transform.position);
            CANATTACK = false;
            _timer = 0;
        }
        else
        {
            Debug.Log("now");
            AGENT.isStopped = true;
            transform.LookAt(Player.transform.position);
            CANATTACK = true;

            _timer += dt;
            if (_timer >= FIRERATE)
            {
                _timer -= FIRERATE;
                for (int i = 0; i < BURSTAMOUNT; i++)
                    Invoke("Shoot", i * 0.2f);
            }
        }
    }

    public virtual void Shoot() { }

    public void ShootOnce(float angle)
    {
        var bullet = (GameObject)Instantiate(BulletPrefab, GunLoc.transform.position, GunLoc.transform.rotation);
        bullet.tag = "Respawn";
        bullet.transform.Rotate(Vector3.up, angle);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
        Destroy(bullet, 2.0f);
        AMMO--;
    }
}
