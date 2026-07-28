# DISCLAIMER
This repo primarily exists for personal use, and so projects I'm working on that have multiple programmers can share these utility/helper classes.

Again, please note that these tools are built for my own projects; this means that they could change in functionality at any time. If you plan on using them long term, I strongly suggest sticking to one version or paying close attention to each update/commit.

Feel free to use these in your own projects or base your own code off of mine, no credit needed; just don't claim it as your own.

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
W.I.P!

# Player Controller
W.I.P!

# Save System
W.I.P!

# State Machine
W.I.P!

# Utility Scripts
W.I.P!
