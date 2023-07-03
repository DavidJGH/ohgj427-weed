using System;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Grass : MonoBehaviour
{
    public Sprite[] sprites = Array.Empty<Sprite>();


    private SpriteRenderer spriteRenderer;
    private static readonly int CutAtt = Shader.PropertyToID("_Cut");

    private bool cut = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }

    public bool Cut()
    {
        if (!cut)
        {
            cut = true;
            spriteRenderer.material.SetInt(CutAtt, 1);
            return true;
        }

        return false;
    }

}
