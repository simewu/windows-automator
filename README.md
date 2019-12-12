# Windows Automator
Create bots, automate yourself using C#. This bot was built to simulate human interaction as realistically as possible, by moving the mouse, typing, clicking, and pressing keys in such a way that it is indistinguishable from a human.

Rather than limiting the user, the application takes advantage of C# reflection to allow the user to customize their bot however they please, then save/load their bot files.

These customizations have limitations that require them to behave like a human, so that the specifics of how the mouse moves, and how long key presses should be pressed for, etc..., is all handled by the application. Then the user has access to functions such as MoveMouse, PressKey, RealisticType, ... that take care of this human-like requirement.

The custom functions are as follows:
```csharp
Alert(string message)     // Displays a message dialog box to the user
Break([string message])   // Stops the program
Click()                   // Sends a left click keystroke
ElapsedHours()            // Returns number of hours since simulation began
ElapsedMilliseconds()     // Returns number of milliseconds since simulation began
ElapsedMinutes()          // Returns number of minutes since simulation began
ElapsedSeconds()          // Returns number of seconds since simulation began
FastType(string message)  // Sends instant keystrokes to type a message
GetColorAt(int x, y)      // Returns a (R, G, B) color at screen point
GetCursorX()              // Gets the cursor's x-position
GetCursorY()              // Gets the cursor's y-position
IsKeyDown(string key)     // Returns true if key is currently pressed down
KeyDown(string key)       // Holds down a key
KeyUp(string key)         // Releases a key
MiddleClick()             // Sends a middle click keystroke
MiddleDown()              // Holds down middle mouse button
MiddleUp()                // Releases middle mouse button
MouseDown()               // Holds down left mouse button
MouseUp()                 // Releases left mouse button
MoveMouse(int x, y [, int width, height]) // Animates the mouse moving to a portion of the screen (providing width and height adds extra randomization)
PressKey(string key, int seconds) // Presses a key for a certain amount of time
Prompt(string message [, string title]) // Gets a string input from the user
Random(int a [, b])       // Generates a random integer from a to b, [or 0 to a]
RandomF(float a [, b])    // Generates a random float from a to b, [or 0 to a]
RealisticType(string message) // Sends keystrokes to type a message with pauses
RightClick()              // Sends a right click keystroke
RMouseDown()              // Holds down right mouse button
RMouseUp()                // Releases right mouse button
ScreenHeight()            // Returns the screen's height
ScreenWidth()             // Returns the screen's width
SetCursor(int x, y)       // Set the cursor position instantly
Sleep(int milliseconds)   // Pause execution for a certain amount of time
```
*Prepending "//Interval: X" to the first line of the code (where X is the number of seconds) will override the "Number of seconds per refresh" parameter when the code executes, this is so that saving and loading scripts can preserve the refresh rate. This extra line of code is optional.*

![](/screenshots/1.png)

The "build" branch contains the pre-built binary, however, it is encouraged to build the application yourself in Visual Studio.
The executable requires the file "InputSimulator.dll" to be in the same directory as the executable.
