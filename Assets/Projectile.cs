using UnityEngine;

public class Projectile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float speed = 10;
    public float life_span = 1;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    { 
        if (life_span > 0)
        {
            transform.position += transform.TransformDirection(Vector3.up * speed * Time.deltaTime);
            life_span -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Overlay"))
        {
            Destroy(gameObject);
        }
    }
}
