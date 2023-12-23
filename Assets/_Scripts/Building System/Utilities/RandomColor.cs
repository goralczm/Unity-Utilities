using UnityEngine;

public class RandomColor : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f),
                                                         Random.Range(0f, 1f),
                                                         Random.Range(0f, 1f));
    }
}
