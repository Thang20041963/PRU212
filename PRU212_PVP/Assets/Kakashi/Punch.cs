using UnityEngine;

public class Punch : MonoBehaviour
{
    public float range = 0.5f;
    public float speed = 5f;
    private float direction;
    private bool hit;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        // Move the punch
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Deactivate the punch if it exceeds the range
        if (Vector3.Distance(new Vector3(direction, transform.position.y, transform.position.z), transform.position) >= range)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        Deactivate();
        Debug.Log("Hit");
    }

    public void SetDirection(float _direction)
    {
        Debug.Log("Punch");
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Adjust the local scale to match the direction
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        // Deactivate the punch object
        hit = false;
        boxCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
