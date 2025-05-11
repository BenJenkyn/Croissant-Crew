using System.Collections;
using UnityEngine;

public class SquishStretch : MonoBehaviour
{
    float animationFrame = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while (true)
        {
            transform.localScale = new Vector3(
                1,
                0.8f + 0.4f * Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * (animationFrame + 90) * 6)),
                1);
            animationFrame++;
            yield return new WaitForSecondsRealtime(1 / 60f);
        }
    }
}
