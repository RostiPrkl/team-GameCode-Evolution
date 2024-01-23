using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    void OnDestroy()
    {
        //check for exiting the scene, no more error!
        if (!gameObject.scene.isLoaded)
            return;

        float randomNumber = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new();

        foreach (Drops rate in drops)
        {
            if(randomNumber <= rate.dropRate)
                possibleDrops.Add(rate);
        }
        if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }

        Player player = GetComponent<Player>();
        if (player == null)
            Destroy(gameObject);
    }
}
