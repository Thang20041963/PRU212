using System.Collections;
using UnityEngine;

public class CrackEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    private float fadeTime = 1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            Color color = sr.color;
            color.a = Mathf.Lerp(1, 0, elapsed / fadeTime);
            sr.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }
}
