using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField]
    private float radius = 9.5f;

    [SerializeField]
    public int count = 20;

    [SerializeField]
    private GameObject prefab;

    private List<GameObject> grasses = new List<GameObject>();

    void Awake()
    {
        Generate();
    }

    [Button]
    public void Generate()
    {
        Clear();
        for (int i = 0; i < count; i++)
        {
            grasses.Add(Instantiate(prefab, transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius)), Quaternion.identity));
        }
    }

    [Button]
    void Clear()
    {
        foreach (GameObject grass in grasses)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(grass);
            }
            else
            {
                Destroy(grass);
            }
        }
        grasses.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Vector3.one * radius * 2);
    }
}
