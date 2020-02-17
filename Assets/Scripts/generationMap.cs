using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class generationMap : MonoBehaviour
{
    public GameObject prefab;
    private float posX = -495;
    private float posZ = -495;
    private float randomX, randomZ;

    void Start()
    {
        for (int i = -490; i < 490; i += 10)
        {
            for (int j = -490; j < 490; j += 10)
            {
                if ((i<300 || i>400) || (j<200 || j>300))
                {
                    Vector3 position = new Vector3(x: i + Random.Range(0f, 8f), 3f, z: j + Random.Range(0f, 8f));
                    Instantiate(prefab, position, prefab.transform.rotation);
                }
                
            }
        }
    }

    //créer des zones sans arbre
}
