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


    private float _wanderTimer = 0.0f;
    private float _timer = 0.0f;

    protected float _wanderTimeCount;
    protected int _random = 3;
    public virtual void BasicMove(float dt)
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

    protected virtual void Wander()
    {
        _wanderTimer += Time.deltaTime;
        if (_wanderTimer > _wanderTimeCount)
        {
            switch (_random)
            {
                case 1:
                    AGENT.isStopped = false;
                    AGENT.SetDestination(GetRandomPoint(transform.position, Random.Range(1.0f, 20.0f)));
                    break;
                case 2:
                    AGENT.isStopped = false;
                    AGENT.SetDestination(Player.transform.position);
                    break;
                case 3:
                    AGENT.isStopped = true;
                    break;
                default:
                    Debug.Log("Error code: " + _random);
                    break;
            }
            _wanderTimer = 0.0f;
            _wanderTimeCount = Random.Range(1.5f, 3.5f);
            _random = Random.Range(1, 4);
            Debug.Log(_random);
        }


    }

    private Vector3 GetRandomPoint(Vector3 origin, float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance,NavMesh.AllAreas);
        return navHit.position;
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
