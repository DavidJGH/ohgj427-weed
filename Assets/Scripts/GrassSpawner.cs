using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField] private float radius = 9.5f;

    [SerializeField] private int count = 20;

    [FormerlySerializedAs("prefab")] [SerializeField]
    private GameObject grassPrefab;

    [SerializeField] private GameObject[] flowerPrefabs;

    private List<GameObject> grasses = new();

    [SerializeField] private int modulo = 100;

    [SerializeField] private int flowerMin = 0;
    [SerializeField] private int flowerMax = 15;

    [SerializeField] private int flowerPerClusterMin = 1;
    [SerializeField] private int flowerPerClusterMax = 6;

    [SerializeField] private float clusterDistanceMin = 0.5f;
    [SerializeField] private float clusterDistanceMax = 3;

    private int flowerCount = 0;

    public int GetCount()
    {
        return count + flowerCount;
    }

    void Awake()
    {
        Generate();
    }

    [Button]
    public void Generate()
    {
        Clear();
        StartCoroutine(GenerateCoroutine());
    }

    IEnumerator GenerateCoroutine()
    {
        flowerCount = 0;
        
        for (int i = 0; i < count; i++)
        {
            grasses.Add(Instantiate(grassPrefab,
                transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius)),
                Quaternion.identity));
            if (i % modulo == 0)
            {
                yield return null;
            }
        }

        var flowerClusters = Random.Range(flowerMin, flowerMax + 1);
        for (int i = 0; i < flowerClusters; i++)
        {
            var origin = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius));
            var flowerType = Random.Range(0, flowerPrefabs.Length);
            var flowers = Random.Range(flowerPerClusterMin, flowerPerClusterMax + 1);
            var clusterDistance = Random.Range(clusterDistanceMin, clusterDistanceMax);
            for (int j = 0; j < flowers; j++)
            {
                Vector3 position = origin + new Vector3(Random.Range(-clusterDistance, clusterDistance),
                    Random.Range(-clusterDistance, clusterDistance));
                if (position.x < -radius || position.x > radius || position.y < -radius || position.y > radius)
                {
                    continue;
                }

                flowerCount++;

                grasses.Add(Instantiate(flowerPrefabs[flowerType],
                    transform.position + position,
                    Quaternion.identity));
            }
        }
    }

    [Button]
    void Clear()
    {
        var oldGrass = grasses;
        grasses = new List<GameObject>();
        StartCoroutine(ClearCoroutine(oldGrass));
    }

    IEnumerator ClearCoroutine(List<GameObject> grass)
    {
        for (int i = 0; i < grass.Count; i++)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(grass[i]);
            }
            else
            {
                Destroy(grass[i]);
            }

            if (i % modulo == 0)
            {
                yield return null;
            }
        }

        grass.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Vector3.one * radius * 2);
    }
}