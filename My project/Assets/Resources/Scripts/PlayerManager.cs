using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEditor;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Tilemap FloorGrid;
    [SerializeField] private float characterSpeed = 3f;
    [SerializeField] private float moveDur = 0.05f;
    [SerializeField] private float yBorder = 0.5f;

    public GameObject playerObj;

    public Vector3 playerPos {get;set;}
    public bool isFalling{get;set;}

    public int maxHealth {get;set;}    
    public int curHealth {get;set;}

    private float slope;
    private float height = 0.25f;
    private Vector2 direction;

    public void ChangeHealth(int healthChange)
    {
        int nextCurHealth = Mathf.Clamp(curHealth+healthChange, 0, maxHealth);
        for(int i = curHealth; i > nextCurHealth; i--)
        {
            HeartManager.Instance.FadeHeart(i);
        }
    }

    protected override void Awake() {
        base.Awake();
        float xDif = FloorGrid.GetCellCenterWorld(new Vector3Int(1, 0, 0)).x - FloorGrid.GetCellCenterWorld(new Vector3Int(0, 0, 0)).x;
        float yDif = FloorGrid.GetCellCenterWorld(new Vector3Int(0, 1, 0)).y - FloorGrid.GetCellCenterWorld(new Vector3Int(0, 0, 0)).y;
        slope = xDif/yDif;
        isFalling = false;

        maxHealth = 5;
        curHealth = maxHealth;

        playerObj = gameObject;
    }
    private void Start() {
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        StartCoroutine("PlayerMove");
    }

    private void Update() {
        playerPos = transform.position;
        CheckFall();
    }

    private void OnTriggerExit2D(Collider2D other) {
        
    }

    private void CheckFall()
    {
        Vector3 botPos = new Vector3(transform.position.x, transform.position.y-height, 0);
        Vector3Int gridPos = FloorGrid.WorldToCell(botPos);
        if(!FloorGrid.HasTile(gridPos))
        {
            if(botPos.y > yBorder)
                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerFall";
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            isFalling = true;
        }
    }

    // private IEnumerator DisableMove()
    // {
    //     yield return new WaitForSeconds(0.3f);
    //     isFalling = true;
    //     yield break;
    // }

    private IEnumerator PlayerMove()
    {
        while(true)
        {
            if(!isFalling)
            {
                direction = new Vector2(0, 0);
                if(Input.GetKey("w"))
                {
                    direction = new Vector2(direction.x, direction.y+1);
                }
                if(Input.GetKey("s"))
                {
                    direction = new Vector2(direction.x, direction.y-1);
                }
                if(Input.GetKey("a"))
                {
                    direction = new Vector2(direction.x-1, direction.y);
                }
                if(Input.GetKey("d"))
                {
                    direction = new Vector2(direction.x+1, direction.y);
                }
                direction.Normalize();
                direction = new Vector2(direction.x, direction.y*(1/slope));
                transform.Translate(direction*moveDur*characterSpeed);
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                HeartManager.Instance.MoveHearts();
            }

            yield return new WaitForSeconds(moveDur);
        }
    }
}
