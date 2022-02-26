using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : Singleton<CloudManager>
{
    [SerializeField] GameObject cloudObj;
    [SerializeField] Sprite[] cloudSprites;
    void Start()
    {
        StartCoroutine("SpawnCloud");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnCloud()
    {
        while(true)
        {
            int i = Random.Range(0, cloudSprites.Length);
            Vector3 cloudPos = new Vector3(-9.5f, Random.Range(-2, 2)*1.5f, 10);
            GameObject newCloud = Instantiate(cloudObj, cloudPos + Camera.main.transform.position, Quaternion.identity);
            newCloud.GetComponent<SpriteRenderer>().sprite = cloudSprites[i];
            newCloud.transform.parent = Camera.main.transform;
            newCloud.transform.localPosition = cloudPos;

            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
        }
    }
}
