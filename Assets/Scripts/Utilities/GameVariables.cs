using System.Collections.Generic;

/// <summary>
/// Utilities for the game variables management.
/// </summary>
public static class GameVariables
{
    #region Properties

    /// <summary>
    /// The collection of dynamic variables.
    /// </summary>
    public static Dictionary<string, string> Collection { get; private set; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes the <see cref="GameVariables"/> class.
    /// </summary>
    static GameVariables() { Collection = new Dictionary<string, string>(); }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Updates the variable if exists, create it otherwise.
    /// </summary>
    /// <param name="variableName">The name of the variable to update/create.</param>
    /// <param name="value">The value we want to give to the variable.</param>
    public static void Update(string variableName, string value)
    {
        if (Collection.ContainsKey(variableName)) Collection[variableName] = value;
        else Collection.Add(variableName, value);
    }

    /// <summary>
    /// Updates the variable if exists, create it otherwise.
    /// </summary>
    /// <param name="variableName">The name of the variable to update/create.</param>
    /// <param name="value">The value we want to give to the variable. (The value will be saved calling ToString() function)</param>
    public static void Update(string variableName, object value) { Update(variableName, value.ToString()); }

    /// <summary>
    /// Returns the variable if exists, defaultValue otherwise.
    /// </summary>
    /// <param name="variableName">The name of the variable we want to get.</param>
    /// <param name="defaultValue">The value to return if the variable has been not initialized.</param>
    /// <returns>
    /// The variable.
    /// </returns>
    public static string Get(string variableName, string defaultValue) { return Collection.ContainsKey(variableName) ? Collection[variableName] : defaultValue; }

    /// <summary>
    /// Returns the variable if exists, "" otherwise.
    /// </summary>
    /// <param name="variableName">The name of the variable we want to get.</param>
    /// <param name="defaultValue">The value to return if the variable has been not initialized. (The value will be returned calling ToString() method)</param>
    /// <returns>
    /// The variable.
    /// </returns>
    public static string Get(string variableName, object defaultValue) { return Get(variableName, defaultValue.ToString()); }

    /// <summary>
    /// Deletes the specified variable if exists.
    /// </summary>
    /// <param name="variableName">The name of the variable.</param>
    public static void Delete(string variableName) { if (Collection.ContainsKey(variableName)) Collection.Remove(variableName); }

    #endregion Methods
}