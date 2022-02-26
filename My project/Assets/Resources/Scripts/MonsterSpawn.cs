using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : Singleton<MonsterSpawn>
{
    [SerializeField] GameObject monsterObject;
    private float spawnRate = 3.5f;
    void Start()
    {
        StartCoroutine("SpawnMonster");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 RandomVector3(float angle, float angleMin, float magnitude)
    {
        float random = Random.value * angle + angleMin;
        return (new Vector3(Mathf.Cos(random), Mathf.Sin(random), 0)) * magnitude;
    }
    private IEnumerator SpawnMonster()
    {
        while(true)
        {
            Vector3 monsterSpawnPos = RandomVector3(Mathf.PI * 2, 0, 3) + PlayerManager.Instance.playerPos;
            Instantiate(monsterObject, monsterSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
