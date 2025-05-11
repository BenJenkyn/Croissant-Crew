using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteAnimator : MonoBehaviour
{

    [SerializeField] private Sprite[] currentSprite;
    public string currentSpriteName;
    int currentRange;
    int start;
    int end;
    public int currentIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRange(int min, int max, string sprite)
    {
        if (currentSpriteName != sprite || min != start)
        {
            start = min;
            end = max;
            currentIndex = start;
            currentSprite = Resources.LoadAll<Sprite>(sprite);
            currentSpriteName = sprite;
        }
    }

    IEnumerator Animate()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().sprite = currentSprite[currentIndex];
            currentIndex++;
            if (currentIndex >= end)
            {
                currentIndex = start;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
