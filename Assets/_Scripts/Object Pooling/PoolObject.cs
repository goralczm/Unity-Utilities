using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private string _tag;

    private void OnEnable()
    {
        Invoke("Disable", 1f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Release();
    }

    public void Init(string tag)
    {
        _tag = tag;
    }

    protected void Release()
    {
        PoolManager.Instance?.ReleaseObject(_tag, gameObject);
    }
}
