using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        gameObject.BindToTouch(() => { Debug.Log(gameObject); });
    }

    private void Update()
    {
        if (TouchManager.ScreenTouched()) Debug.Log("Ciao");
    }
}