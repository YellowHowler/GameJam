using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private float moveDur = 0.05f;
    [SerializeField] private float monsterSpeed = 1.5f;
    void Start()
    {
        StartCoroutine("MoveToPlayer");
    }
    void Update()
    {
        
    }

    private IEnumerator MoveToPlayer()
    {
        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerManager.Instance.playerPos, monsterSpeed * moveDur);
            yield return new WaitForSeconds(moveDur);
        }
    }
}
