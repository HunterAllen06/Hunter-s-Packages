# DISCLAIMER
This repo primarily exists for personal use, and so projects I'm working on that have multiple programmers can share these utility/helper classes.

Again, please note that these tools are built for my own projects; **<ins>this means that they could change in functionality at any time</ins>**. If you plan on using them long term, I strongly suggest sticking to one version/installing a packing and sticking to it, or paying very close attention to each update/commit.

Feel free to use these in your own projects or base your own code off of mine, no credit needed; just don't claim it as your own.

# Index
- [Events](#events)
- [Game Services](#game-services)
- [Log Tools](#log-tools)
- [Menus](#menus)
- [Player Controller](#player-controller)
- [Save System](#save-system)
- [State Machine](#state-machine)
- [Utility Scripts](#utility-scripts)

*Documentation is W.I.P., some sections are unfinished.*

# Events
A small system that allows you to bind events to functions by type or object.
```cs
GameObject _someObject;
EventBus _bus;
EventChannel _someChannel;

void Examples()
{
    EventManager.TBind<SomeType>(MyFunction);
    _someObject.Bind(SomeFunction);
    _bus.Bind(SomeOtherFunction);
    _someChannel.Bind(AnotherFunction);

    EventManger.TRaise<SomeType>(); // Calls MyFunction
    _someObject.Raise(); // Calls SomeFunction
    _bus.Raise(); // Calls SomeOtherFunction
    _someChannel.Raise(); // Calls AnotherFunction
}
```

# Game Services
A simple system that sort of acts as a replacement/substitute for static Instances.
```cs
void OnEnable() => GameServices.Register(this);
void OnDisable() => GameServices.Deregister(this);

void GetExamples()
{
    var service = GameServices.Get<SomeService>();
    bool hasOtherService = GameServices.TryGet(out SomeOtherService otherService);
    bool containsAnotherService = GameServices.Contains<AnotherService>();
}
async void GetAsyncExample()
{
    var service = await GetAsync<SomeService>();
}
```

# Log Tools
Simple Debug.Log wrappers.
```cs
void Examples()
{
    this.Log("Some log."); // [ThisClass] Some log.
    this.Log("Another log.", "#FF0"); // [ThisClass] Another log.  -  Prints in the color green
    this.LogC("Log with extra info."); // [ThisClass.cs:XX - Examples] Log with extra info.
}
```

# Menus
A few components to help with interface menus.

## Menu.cs
    Goes onto a GameObject that is the 'menu'. Requires a CanvasGroup component on the same GameObject.
    - EnterMenu() - Calls the OnEnterMenu event, animates the menu if a MenuAnimator component is on the same GameObject.
    - ExitMenu() - Calls the OnExitMenu event, animates the menu if a MenuAnimator component is on the same GameObject.
## MenuAnimator.cs
    Automatically animates the menu's transform and canvas transform when the menu is entered or exited.
## MenuGroup.cs
    Can be put on a GameObject that is the parent of one or more GameObjects with the Menu component to help automatically exit previous menus when a new one is entered.

# Player Controller
**System is a heavy W.I.P!**
Simple player controller with built in stair/slope support.

## PlayerController.cs
    Has functions for providing input to the PlayerMover and PlayerCamera, but doesn't apply input on its own. You will need to create a class to provide input to PlayerController.

## PlayerMover.cs
    Can move the player and handle ground/stair/slope detection. Automatically sets Collider height in OnValidate() according to height/raycast settings. Input is provided by the Player Controller.

## PlayerCamera.cs
    Rotates the player camera and player body in Update and FixedUpdate. Input is provided by the Player Controller.

# Save System
**System is a heavy W.I.P!**
Saves and loads encrypted json files, stores them in a Dictionary<string, object> (name/id - data) at runtime. **This system is incredibly work in progress and has been changing a lot as of recent. If you are going to use this package, I heavily recommend downloading the package and sticking to it, modifying it yourself if need be.** Summaries are on the functions in the class, I will write documentation here once the system is in a more finished state.

# State Machine
A <a href="https://en.wikipedia.org/wiki/Finite-state_machine">Finite State Machine</a> system for Unity projects.

## StateMachine
The MonoBehaviour to derive from for state machine behaviour.
```cs
void Start()
{
    var state1 = new State1();
    var state2 = new State2();
    var condition1 = new Condition1();
    var condition2 = new Condition2();

    state1.AddTransition(state2, condition1); // Will transition from state 1 -> state 2 if condition 1 evaluates to true.
    state2.AddTransition(state1, condition2); // Will transition from state 2 -> state 1 if condition 2 evaluates to true.
}
```

# Utility Scripts
Lots of miscellaneous components and extension scripts for various classes/types. Here are some of the main features:

## Editor
- Albedo View Toggle
- Orient to Surface Component

## Components
- Raycaster
- TransformSpring (very buggy - breaks at low framerates in regular Update(), stutters at high framerates in FixedUpdate)

## Data Classes
- EAxis - Simple flags enum with [X, Y, Z] values
- ListWrapper - Literally just a List. I made this to be able to store Lists within Lists.

## Extensions
- AudioSourceExtensions
- CanvasGroupExtensions
- ComponentExtensions
- EnumerableExtensions
- GameObjectExtensions
- MeshRendererExtensions
- NavMeshExtensions
- RigidbodyExtensions
- SceneExtensions
- VectorExtensions

## Helper Classes
- WaitFunctions
- Timer
- ObjecWithProbability
- RandomFunctions
- RandomObjectContainer

## Physics
- Spring

Some of these utility scripts are based off of [git-ammend's Unity Utils](https://github.com/adammyhre/Unity-Utils)
