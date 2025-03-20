using UnityEngine;

public class ObitoSpecial1 : MonoBehaviour
{
    public float speed = 20f;
    private float direction;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator animator;
    public int damage = 15;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");

        Debug.Log("Special hit");
        Invoke(nameof(Deactivate), 0.5f);
    }

    public void SetDirection(float _direction)
    {
        Debug.Log("Special1");
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
