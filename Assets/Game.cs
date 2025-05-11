using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
