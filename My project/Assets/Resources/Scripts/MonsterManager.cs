using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private float moveDur = 0.05f;
    [SerializeField] private float monsterSpeed = 1.5f;

    Animator ani;

    private bool isTouching = false;

    private Vector3 offsetPos;

    private void Awake() 
    {
        ani = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        offsetPos = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0);
        StartCoroutine("MoveToPlayer");
        StartCoroutine("AttackPlayer");
    }
    void Update()
    {
        if(Vector3.Distance(PlayerManager.Instance.playerPos, transform.position) < 0.4f)
        {
            isTouching = true;
            Debug.Log("touching");
        }
        else
        {
            isTouching = false;
        }
    }

    private IEnumerator MoveToPlayer()
    {
        while(!PlayerManager.Instance.isFalling)
        {
            Vector3 goalPos = new Vector3(PlayerManager.Instance.playerPos.x, PlayerManager.Instance.playerPos.y, 0) + offsetPos;
            transform.position = Vector3.MoveTowards(transform.position, goalPos, monsterSpeed * moveDur);

            if(goalPos.x > transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(goalPos.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            yield return new WaitForSeconds(moveDur);
        }

        ani.SetBool("Fade", true);
        Destroy(gameObject, 0.5f);
        yield break;
    }

    private IEnumerator AttackPlayer()
    {
        while(!PlayerManager.Instance.isFalling)
        {
            if(isTouching)
            {
                yield return new WaitForSeconds(0.5f);
                if(isTouching) PlayerManager.Instance.ChangeHealth(-1);
                Debug.Log("attack");
                PlayerManager.Instance.playerObj.GetComponent<SpriteRenderer>().color = new Color(0.8f, 1, 0.8f, 1);
            }
        }
    }
}
