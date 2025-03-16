using UnityEngine;

public class Special2 : MonoBehaviour
{
    private float direction;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(float _direction)
    {
        Debug.Log("Special2");
        direction = _direction;
        gameObject.SetActive(true);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
