using UnityEngine;

/// <summary>
/// The game manager class.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Methods

    private void Update()
    {
        TouchManager.Update();
    }

    #endregion Methods
}