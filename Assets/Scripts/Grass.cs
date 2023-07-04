using System;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Grass : MonoBehaviour
{
    public Sprite[] sprites = Array.Empty<Sprite>();


    private SpriteRenderer spriteRenderer;
    private static readonly int CutAtt = Shader.PropertyToID("_Cut");
    private static readonly int SeedAtt = Shader.PropertyToID("_Seed");

    private bool cut = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }

    private void Start()
    {
        spriteRenderer.material.SetFloat(SeedAtt, Game.instance.seed);
    }

    public bool Cut()
    {
        if (!cut)
        {
            cut = true;
            spriteRenderer.material.SetInt(CutAtt, 1);
            Game.instance.Cut();
            return true;
        }

        return false;
    }
}