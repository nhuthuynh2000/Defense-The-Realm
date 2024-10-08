using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHP = 5f;

    Enemy enemy;
    float curHP;
    // Start is called before the first frame update
    void OnEnable()
    {
        curHP = maxHP;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        curHP--;
        if (curHP <= 0)
        {
            enemy.GiveGold(enemy.RewardGold);
            gameObject.SetActive(false);
        }
    }
}
