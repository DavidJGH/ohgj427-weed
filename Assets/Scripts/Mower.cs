using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Mower : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 velocity;

    [SerializeField] private Transform particleSpawner;

    [SerializeField] private ParticleSystem mowP1;

    [SerializeField] private ParticleSystem mowP2;
    [SerializeField] private ParticleSystem mowP0;

    private float emissionTimer = float.MinValue;

    private float emissionLength = 0.1f;

    private bool emitting = false;

    private Vector2 prevPos;

    [SerializeField]
    private float scale = 11.5f;

    private Vector2 scaleVector;

    void Start()
    {
        camera = Camera.current;
        
        scaleVector = new Vector2(scale * ((float)Screen.width / Screen.height), scale);
    }

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;

        
        var position = new Vector2(mouseScreenPos.x / Screen.width, mouseScreenPos.y / Screen.height) * scaleVector - scaleVector / 2f;

        transform.position = position;
        spriteRenderer.sortingOrder = -(int)(position.y * 100);

        if (velocity.sqrMagnitude != 0)
        {
            particleSpawner.rotation = Quaternion.LookRotation(Vector3.back, velocity);
        }

        if (emitting && Time.time > emissionTimer + emissionLength)
        {
            mowP1.Stop();
            mowP2.Stop();
            emitting = false;
        }

        velocity = Vector2.Lerp(velocity, position - prevPos, 5 * Time.deltaTime);
        prevPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.TryGetComponent(out Grass grass))
        {
            if (grass.Cut())
            {
                emissionTimer = Time.time;
                mowP1.Play();
                mowP2.Play();
                mowP0.Emit(Random.Range(0, 2));
                emitting = true;
            }
        }
    }
}