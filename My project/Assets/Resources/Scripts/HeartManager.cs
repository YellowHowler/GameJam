using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : Singleton<HeartManager>
{
    [SerializeField] GameObject heartObj;

    int maxHealth;

    GameObject[] hearts;
    Vector3[] heartPos;

    private void Awake() 
    {
        
    }

    public void FadeHeart(int num)
    {
        hearts[num].GetComponent<Animator>().SetTrigger("Dissolve");
    }

    public void MoveHearts()
    {
        for(int i = 0; i < maxHealth; i++)
        {
            hearts[i].transform.position = Camera.main.transform.position + heartPos[i];
        }
    }
    void Start()
    {
        maxHealth = PlayerManager.Instance.maxHealth;
        heartPos = new Vector3[maxHealth];
        hearts = new GameObject[maxHealth];

        for(int i = 0; i < maxHealth; i++)
        {
            heartPos[i] = new Vector3(-3.65f + 0.55f * i, 2.1f, 10);
            hearts[i] = Instantiate(heartObj, heartPos[i], Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
