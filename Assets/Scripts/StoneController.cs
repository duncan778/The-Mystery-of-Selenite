using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField] static int maxPennantCount = 10;
    static int pennantCount = 0;
    static GameObject stoneContainer;
    public GameObject pennantPrefab;

    private void Start() 
    {
        stoneContainer = GameObject.Find("Stones").gameObject;
    }

    private void OpenStone() 
    {   
        int pennantToFind = maxPennantCount - pennantCount;
        if (pennantToFind > 0)
        {
            int stoneCount = stoneContainer.transform.childCount;
            int range = stoneCount > pennantToFind ? stoneCount/pennantToFind : 1;
            if (Random.Range(1, range) == 1)
            {
                Vector3 pennantPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Instantiate(pennantPrefab, pennantPosition, pennantPrefab.transform.rotation);
                pennantCount++;
            }
        }
        //VFX
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            OpenStone();
        }
    }
}
