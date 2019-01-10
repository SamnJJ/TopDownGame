using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField]
    private int _hp = 100;
    [SerializeField]
    private GameObject _shieldObj = null;

    public void DamageShield(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(this);
            Destroy(_shieldObj);
        }
    }
}
