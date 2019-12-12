# Windows Automator
Create bots, automate yourself using C#. This bot was built to simulate human interaction as realistically as possible, by moving the mouse, typing, clicking, and pressing keys in such a way that it is indistinguishable from a human.

Rather than limiting the user, the application takes advantage of C# reflection to allow the user to customize their bot however they please, then save/load their bot files.

These customizations have limitations that require them to behave like a human, so that the specifics of how the mouse moves, and how long key presses should be pressed for, etc..., is all handled by the application. Then the user has access to functions such as MoveMouse, PressKey, RealisticType, etc... that take care of this human-like requirement.

![](/screenshots/1.png)

The "build" branch contains the pre-built binary, however, it is recommended to build the application yourself in Visual Studio.
The executable requires the file "InputSimulator.dll" to be in the same directory as the executable.
