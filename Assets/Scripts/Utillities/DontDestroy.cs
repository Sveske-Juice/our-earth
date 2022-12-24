using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Start()
    {
        // Register this object to be in the DontDestroyOnLoad scene
        DontDestroyOnLoad(gameObject);
    }
}
