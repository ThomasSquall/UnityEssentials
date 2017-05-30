#if (UNITY_IPHONE || UNITY_ANDROID)
#define MOBILE
#endif

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The touch manager class.
/// </summary>
public static class TouchManager
{
    #region Properties

    private static bool atLeastOneObjectTouched;
    private static Dictionary<int, Vector2> touchPositions;

    #endregion Properties

    #region Methods

    /// <summary>
    /// The Update method.
    /// </summary>
    public static void Update()
    {
        atLeastOneObjectTouched = false;

        foreach (var obj in TouchManagerExtensions.bindings.Keys)
        {
            if (!obj.Touched()) continue;

            atLeastOneObjectTouched = true;

            foreach (var func in TouchManagerExtensions.bindings[obj]) func.Invoke();
        }
    }

    /// <summary>
    /// Tells if the screen has been touched.
    /// </summary>
    /// <param name="touch">The touch id to check. If negative number provided every touch would be checked.</param>
    /// <returns>True if the screen has been touched, false otherwise.</returns>
    public static bool ScreenTouched(int touch = -1)
    {
#if MOBILE
        var result = false;

        if (touch >= 0) result = Input.touches[touch].phase == TouchPhase.Began;
        else if (Input.touches.Any(t => t.phase == TouchPhase.Began)) result = true;

        return result;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }

    /// <summary>
    /// Tells if the right part of the screen has been touched.
    /// </summary>
    /// <param name="touch">The touch id to check.</param>
    /// <returns>True if the right part of the screen has been touched, false otherwise.</returns>
    public static bool RightScreenTouched(int touch = 0)
    {
#if MOBILE
        if (touch < 0 || Input.touches.Length < touch + 1) return false;

        var t = Input.touches[touch];

        return ScreenTouched(touch) && (t.position.x > Screen.width / 2.0f);
#else
        return ScreenTouched() && (Input.mousePosition.x > Screen.width / 2.0f);
#endif
    }

    /// <summary>
    /// Tells if the left part of the screen has been touched.
    /// </summary>
    /// <param name="touch">The touch id to check.</param>
    /// <returns>True if the left part of the screen has been touched, false otherwise.</returns>
    public static bool LeftScreenTouched(int touch = 0)
    {
#if MOBILE
        if (touch < 0 || Input.touches.Length < touch + 1) return false;

        var t = Input.touches[touch];

        return ScreenTouched(touch) && (t.position.x < Screen.width / 2.0f);
#else
        return ScreenTouched() && (Input.mousePosition.x < Screen.width / 2.0f);
#endif
    }

    /// <summary>
    /// Tells if just the screen has been touched.
    /// </summary>
    /// <returns>True if the screen has been touched, false otherwise.</returns>
    public static bool PureScreenTouched()
    {
        return ScreenTouched() && !atLeastOneObjectTouched;
    }

    /// <summary>
    /// Clears the bindings for a new usage.
    /// </summary>
    internal static void ClearBindings()
    {
        TouchManagerExtensions.bindings.Clear();
    }

    #endregion Methods
}

public static class TouchManagerExtensions
{
    #region Properties

    internal static readonly Dictionary<GameObject, List<TouchDelegate>> bindings;

    #endregion Properties

    #region Constructors

    static TouchManagerExtensions()
    {
        bindings = new Dictionary<GameObject, List<TouchDelegate>>();
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Checks if the object has been touched.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns>True if the object has been touched, false otherwise.</returns>
    public static bool Touched(this GameObject obj)
    {
#if MOBILE
        if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return false;

        var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
#else
        if (!Input.GetMouseButtonDown(0)) return false;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif

        if (obj.GetComponent<Collider>())
        {
            RaycastHit hit;
            return Physics.Raycast(ray, out hit) && hit.collider.gameObject == obj;
        }

        if (!obj.GetComponent<Collider2D>()) return false;

        RaycastHit2D hit2D;
        return (hit2D = Physics2D.Raycast(ray.origin, ray.direction)) && hit2D.collider.gameObject == obj;
    }

    /// <summary>
    /// Binds a function to the touch event.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="funcTouchDelegate">The function to raise.</param>
    public static void BindToTouch(this GameObject obj, TouchDelegate funcTouchDelegate)
    {
        if (bindings.ContainsKey(obj)) bindings[obj].Add(funcTouchDelegate);
        else
        {
            bindings.Add(obj, new List<TouchDelegate>());
            bindings[obj].Add(funcTouchDelegate);
        }
    }

    /// <summary>
    /// Unbinds a function to the touch event.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="funcTouchDelegate">The function to remove.</param>
    public static void UnbindToTouch(this GameObject obj, TouchDelegate funcTouchDelegate)
    {
        if (bindings.ContainsKey(obj) && bindings[obj].Contains(funcTouchDelegate)) bindings[obj].Remove(funcTouchDelegate);
    }

    #endregion Methods
}

public delegate void TouchDelegate();