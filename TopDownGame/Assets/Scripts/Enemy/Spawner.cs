using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
    [SerializeField]
    private List<Enemy> Enemies;
    [SerializeField]
    private List<bool> boolEnemies;
    [SerializeField]
    private List<GameObject> SpawnLocations;
    [SerializeField]
    private Enemy e1;
    [SerializeField]
    private Enemy e2;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Text Wave = null;

    private int wave = 1;
    private int waveIncrease = 7;
    private int waveAmount = 0;
    private float _timer = 0.0f;

    private void Start()
    {
        waveAmount = waveIncrease;
        for (int i = 0; i < waveAmount; i++)
        {
            Spawn();
        }
    }

    private void Update()
    {
        Wave.text = wave.ToString();


        for (int i = 0; i < Enemies.Count; i++)
        {
            if (!Enemies[i])
                Enemies.RemoveAt(i);
        }
        if (Enemies.Count <= 0)
        {
            Debug.Log("all dead");
            _timer += Time.deltaTime;
            if (_timer >= 3.0f)
            {
                _timer = 0;
                waveAmount += waveIncrease;
                for (int i = 0; i < waveAmount; i++)
                {
                    Spawn();
                }
                Player.GetComponent<PlayerClick>().ResetEnergy();
                wave++;
            }
        }


    }

    public void Spawn()
    {
        var l = Random.Range(0, SpawnLocations.Count);
        var e = Random.Range(0, 2);
        var m = Instantiate(e == 0 ? e1 : e2, SpawnLocations[l].transform.position, SpawnLocations[l].transform.rotation);
        m.Player = Player;
        boolEnemies.Add(false);
        Enemies.Add(m);
    }
}
