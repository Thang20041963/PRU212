using System.Collections;
using UnityEngine;

public class SlipAnimation : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float waitTime = 0f;
    [SerializeField] private float duration = .5f;

    private void Start()
    {
        transform.position = startPos.position;
        StartCoroutine(SlipAnimationCoroutine());
    }

    IEnumerator SlipAnimationCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
            yield return null;
        }

        transform.position = endPos.position;
    }
}
