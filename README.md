# Windows Automator
Create bots, automate yourself using C#. This bot was built to simulate human interaction as realistically as possible, by moving the mouse, typing, clicking, and pressing keys in such a way that it is indistinguishable from a human.

Rather than limiting the user, the application takes advantage of C# reflection to allow the user to customize their bot however they please. These customizations have limitations that require them to behave like a human, so that the specifics of how the mouse moves, how long each key should be pressed, etc..., is all handled by the application. Then the user has access to functions such as MoveMouse, PressKey, RealisticType, ... that take care of this human-like requirement.

The custom functions are as follows:
```csharp
Alert(string message)     // Display a message dialog box to the user
Break([string message])   // Stop the program
Click()                   // Send a left click keystroke
ElapsedHours()            // Return number of hours since simulation began
ElapsedMilliseconds()     // Return number of milliseconds since simulation began
ElapsedMinutes()          // Return number of minutes since simulation began
ElapsedSeconds()          // Return number of seconds since simulation began
FastType(string message)  // Send instant keystrokes to type a message
GetColorAt(int x, y)      // Return a (R, G, B) color at screen point
GetCursorX()              // Get the cursor's x-position
GetCursorY()              // Get the cursor's y-position
IsKeyDown(string key)     // Return true if key is currently pressed down
KeyDown(string key)       // Hold down a key
KeyUp(string key)         // Release a key
MiddleClick()             // Send a middle click keystroke
MiddleDown()              // Hold down middle mouse button
MiddleUp()                // Release middle mouse button
MouseDown()               // Hold down left mouse button
MouseUp()                 // Release left mouse button
MoveMouse(int x, y [, int width, height]) // Move the mouse to a location (width and height add randomization)
PressKey(string key, int seconds) // Press a key for a certain amount of time
Prompt(string message [, string title]) // Get a string input from the user
Random(int a [, b])       // Generate a random integer from a to b, [or 0 to a]
RandomF(float a [, b])    // Generate a random float from a to b, [or 0 to a]
RealisticType(string message) // Send keystrokes to type a message with pauses
RightClick()              // Send a right click keystroke
RMouseDown()              // Hold down right mouse button
RMouseUp()                // Release right mouse button
ScreenHeight()            // Return the screen's height
ScreenWidth()             // Return the screen's width
SetCursor(int x, y)       // Set the cursor position instantly
Sleep(int milliseconds)   // Pause execution for a certain amount of time
```
*Prepending "//Interval: X" to the first line of the code (where X is the number of seconds) will override the "Number of seconds per refresh" parameter when the code executes, this is so that saving and loading scripts can preserve the refresh rate. This extra line of code is optional.*

![](/screenshots/1.png)

The "build" branch contains the pre-built binary, however, it is encouraged to build the application yourself in Visual Studio.
The executable requires the file "InputSimulator.dll" to be in the same directory as the executable.
