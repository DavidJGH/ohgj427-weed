using UnityEngine;

public class Mower : MonoBehaviour
{
    private Camera camera;
    
    private SpriteRenderer spriteRenderer;

    private Vector2 velocity;
    
    void Start()
    {
        camera = Camera.current;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10;
        Vector2 goalPos = camera.ScreenToWorldPoint(mouseScreenPos);
        
        Vector2 position = transform.position;
        velocity = (goalPos - position) * Time.deltaTime;
        
        position = goalPos;
        transform.position = position;
        spriteRenderer.sortingOrder = -(int)(position.y * 100);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.TryGetComponent(out Grass grass))
        {
            if (grass.Cut())
            {
                
            }
        }
    }
}
