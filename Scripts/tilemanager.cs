using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilemanager : MonoBehaviour
{
    public GameObject[] tileprefabs;
    public float zspawn = 0;
    public float tilelenght = 30;
    public int numberoftiles = 5;
    public Transform playertransform;
    private List<GameObject> activetiles=new List<GameObject>();

    void Start()
    { for (int i = 0; i < numberoftiles; i++)
        {    if (i == 0)
                spawntile(0);
             else 
            spawntile(Random.Range(0, tileprefabs.Length));
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if (playertransform.position.z -35 > zspawn - numberoftiles * tilelenght)
        {
            spawntile(Random.Range(0, tileprefabs.Length));
            deleteTile();
        }
    }
    public void spawntile(int tileindex)
    {
       GameObject go = Instantiate(tileprefabs[tileindex],transform.forward*zspawn,transform.rotation);
        activetiles.Add(go);
        zspawn += tilelenght;
    }
    private void deleteTile()
    {
        Destroy(activetiles[0]);
        activetiles.RemoveAt(0);
    }
}
