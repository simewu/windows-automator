# Windows Automator
Create bots, automate yourself using C#. This bot was built to simulate human interaction as realistically as possible, by moving the mouse, typing, clicking, and pressing keys in such a way that it is indistinguishable from a human.

Rather than limiting the user, the application takes advantage of C# reflection to allow the user to customize their bot however they please, then save/load their bot files.

These customizations have limitations that require them to behave like a human, so that the specifics of how the mouse moves, and how long key presses should be pressed for, etc..., is all handled by the application. Then the user has access to functions such as MoveMouse, PressKey, RealisticType, ... that take care of this human-like requirement.

![](/screenshots/1.png)

The custom functions are as follows:
```csharp
Alert(message)    // Display a message box to the user
Break([message])  // Stop the program
Click()           // Send a left click keystroke
ElapsedHours()    // Returns number of hours since simulation began
ElapsedMilliseconds() // Returns number of milliseconds since simulation began
ElapsedMinutes()  // Returns number of minutes since simulation began
ElapsedSeconds()  // Returns number of seconds since simulation began
FastType(message) // Send instant keystrokes to type a message
GetColorAt(x, y)  // Returns a (R, G, B) color at screen point
GetCursorX()      // Get the cursor's x-position
GetCursorY()      // Get the cursor's y-position
IsKeyDown(key)    // Returns true if key is currently pressed down
KeyDown(key)      // Holds down a key
KeyUp(key)        // Releases a key
MiddleClick()     // Send a middle click keystroke
MiddleDown()      // Holds down middle mouse button
MiddleUp()        // Releases middle mouse button
MouseDown()       // Holds down left mouse button
MouseUp()         // Releases left mouse button
MoveMouse(x, y [, width, height]) // Animate the mouse moving to a portion of the screen
PressKey(key, seconds) // Presses a key for a certain amount of time
Prompt(message [, title]) // Get a string input from the user
Random(a [, b])    // Generate a random integer from a to b, [or 0 to a]
RandomF(a [, b])   // Generate a random double from a to b, [or 0 to a]
RealisticType(message) // Send keystrokes to type a message with pauses
RightClick()      // Send a right click keystroke
RMouseDown()      // Holds down right mouse button
RMouseUp()        // Releases right mouse button
ScreenHeight()    // Returns the screens height
ScreenWidth()     // Returns the screens width
SetCursor(x, y)   // Set the cursor position instantly
Sleep(milliseconds) // Pause execution for a certain amount of time
```

The "build" branch contains the pre-built binary, however, it is encouraged to build the application yourself in Visual Studio.
The executable requires the file "InputSimulator.dll" to be in the same directory as the executable.
