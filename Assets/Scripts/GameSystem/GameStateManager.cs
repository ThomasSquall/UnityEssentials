using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// Utilities for saving and loading game state.
/// </summary>
public static class GameStateManager
{
    #region Methods

    /// <summary>
    /// Call this method to save the game.
    /// </summary>
    public static void Save()
    {
        Delete();

        PlayerPrefs.SetString(Miscellaneous.__VARIABLES_COUNT__, GameVariables.Collection.Count.ToString());

        var index = 0;

        GameVariables.Collection.ToList().ForEach
        (
            variable =>
            {
                PlayerPrefs.SetString("Key" + index, variable.Key);
                PlayerPrefs.SetString(variable.Key, variable.Value);

                ++index;
            }
        );

        PlayerPrefs.Save();
    }

    /// <summary>
    /// Call this method to load the game.
    /// </summary>
    public static void Load()
    {
        foreach (string key in GetAllKeys())
        {
            if (string.IsNullOrEmpty(key)) return;
            GameVariables.Update(key, PlayerPrefs.GetString(key, ""));
        }
    }

    /// <summary>
    /// Call this method to delete the game save.
    /// </summary>
    public static void Delete()
    {
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// Get all the saved keys.
    /// </summary>
    /// <returns>The list of keys.</returns>
    private static IEnumerable GetAllKeys()
    {
        var count = PlayerPrefs.GetString(Miscellaneous.__VARIABLES_COUNT__);

        if (string.IsNullOrEmpty(count)) yield break;

        for (var index = 0; index < int.Parse(count); index++) yield return PlayerPrefs.GetString("Key" + index);
    }

    #endregion Methods
}