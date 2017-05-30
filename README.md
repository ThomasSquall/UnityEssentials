# UnityEssentials
Essentials scripts for Unity.

**Version:** 0.0.1

**Creator:** Thomas Cocchiara aka ThomasSquall

## Usage
### TouchManager
The TouchManager utility allows you to fasten develop touch based games, it is also capable to use the mouse click when in Unity Editor to help you debug better.

Example 1:
```c#
gameObject.BindToTouch(() => { Debug.Log(gameObject); });
```
In this first example each time the gameObject will be touched the gameObject info will be written to the console.

Example 2:
```c#
if (TouchManager.ScreenTouched()) Debug.Log("Screen touched");
```
In the second example each time the Screen will be touched The "Screen touched" string will be written to the console.
Some other methods can be found inside the TouchManager class.

### GameStateManager
The GameStateManager helps you to Save and Load the game very easily and efficiently.
It serialize and deserialize the GameVariables class using the core PlayerPrefs functions of Unity.
Let's say for example that you have created a GameVariable called *MoneyAmount* using the following syntax:
```c#
GameVariables.Update("MoneyAmount", 1500);
```
and now you want to save the game for a later usage. The following syntax will do the trick:
```c#
GameStateManager.Save();
```
now you want to log the differences of money amount between a new game and the loaded one, just do:

```c#
// 0 is the default value.
Debug.log("Money Amount at beginning: " + GameVariables.Get("MoneyAmount", 0));
GameStateManager.Load();
Debug.log("Money Amount after game loaded: " + GameVariables.Get("MoneyAmount", 0));
```
If you had a previously saved game you should see the difference.

**PS: You can find more utilities inside the repository and more are coming soon**
