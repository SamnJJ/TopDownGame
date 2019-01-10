using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1 : Enemy {
    // Use this for initialization
    private float _timer2 = 0.0f;
    [SerializeField]
    private float _timerMax;
    [SerializeField]
    private float _explodeRadius;
    void Start () {
        HP = 200;
        AMMO = 1;
        MOVEMENT = 10;
        ATTACKRANGE = 1;
        AGENT = GetComponent<NavMeshAgent>();
        FIRERATE = 0.0f;
        BURSTAMOUNT = 0;
        _wanderTimeCount = 2.0f;
    }
    public override void BasicMove(float dt)
    {
        if (!Player || IsTutorial) return;
        if (IGNORE) return;
        Wander();
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

        _timer2 += Time.deltaTime;
        if (_timer2 >= _timerMax)
        {
            Detonate();
        }
	}
    private void Detonate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explodeRadius);
        for (int i = 0; i < hitColliders.Length; ++i)
        {
            if (hitColliders[i].gameObject == gameObject) continue;
            if (hitColliders[i].gameObject.tag == "Enemy1" || hitColliders[i].gameObject.tag == "Enemy2" || hitColliders[i].gameObject.tag == "Enemy3")
            {
                hitColliders[i].gameObject.GetComponent<Enemy>().HP -= 9999;
            }
            if (hitColliders[i].gameObject.tag == "Player")
            {
                PlayerClick player = hitColliders[i].gameObject.GetComponent<PlayerClick>();
                if (player.GetType() == PlayerType.Ghost)
                {
                    player.Damage();
                }
                else
                {
                    player.Damage(9999);
                }
                
            }
        }
        Destroy(gameObject);
    }
}
