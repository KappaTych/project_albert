# Overivew ECS
```
+------------------+
|     Context      |
|------------------|
|    e       e     |      +-----------+
|        e     e---|----> |  Entity   |
|  e        e      |      |-----------|
|     e  e       e |      | Component |
| e            e   |      |           |      +-----------+
|    e     e       |      | Component-|----> | Component |
|  e    e     e    |      |           |      |-----------|
|    e      e    e |      | Component |      |   Data    |
+------------------+      +-----------+      +-----------+
  |
  |
  |     +-------------+  Groups:
  |     |      e      |  Subsets of entities in the context
  |     |   e     e   |  for blazing fast querying
  +---> |        +------------+
        |     e  |    |       |
        |  e     | e  |  e    |
        +--------|----+    e  |
                 |     e      |
                 |  e     e   |
                 +------------+
```

## Component
It is an atomic representation of data. It can be empty, have one or many properties, or even be marked as unique.

### Flag components
Flag components are defined to flag entities. In this case we say that something is movable. So if we have an entity we can ask `entity.isMovable` and get `true` or `false` back. 
```C#
public sealed class MovableComponent : IComponent {}
```

### Data Component
Data Component can have multiple properties which can store pure data:
```C#
public sealed class PositionComponent : IComponent {
    public int x, y;
}
```
There is a method to check if an entity has a component(`hasPosition`). We can also get(`position`), replace(`ReplacePosition`) and remove(`RemovePosition`) components and add(`AddPosition`). Every entity can have only one type of a component set. This is why we have Replace methods. But we can combine all the different types of components in a single entity.

### Reference Component
Those components are harder to serialise. They point to some objects created at runtime, therefore it is not useful to persist the pointer as is. 
```C#
public sealed class ViewComponent : IComponent {
    public GameObject gameObject;
}
``` 

### Action Component
This is a valid use of components, but it does more harm than good.
```C#
public sealed class DelegateComponent : IComponent {
    public Action action;
}
```

### Unique Component
Every type of component we discussed previously can be defined as a unique component. The framework will make sure that only one instance of a unique component can be present in your context.
```C#
[Unique]
public sealed class GameBoardComponent : IComponent {
    public int columns;
    public int rows;
}
```
We can get an instance of a unique component with the following expression - `context.gameBoard`.

The component can also be replaced and removed. So it breaks the idiom of the singleton pattern where an object is unique and persistent throughout the application life cycle. A unique component is more of a global variable than a singleton.


## Entity
An entity is a container holding data to represent certain objects in application. You can add, replace or remove data from entities in form of IComponent. Entities have corresponding events to let you know if components were added, replaced or removed.

In this example you can see some generated methods for `PositionComponent`, `HealthComponent`, `MovableComponent`.
```C#
entity.AddPosition(3, 7);
entity.AddHealth(100);
entity.isMovable = true;

entity.ReplacePosition(10, 100);
entity.ReplaceHealth(entity.health.value - 1);
entity.isMovable = false;

entity.RemovePosition();

var hasPos = entity.hasPosition;
var movable = entity.isMovable;
```

### Entity creation
An entity should always be a part of a context. This is why we are not able to instantiate an entity directly, but have to call `context.CreateEntity()`. Context is a managing data structure which monitors entities life cycle. When an entity is destroyed it will be put into a temporary pool and reused if it's reference count is back at `0`.

When you keep a reference to an entity, you have to call `entity.Retain(this)`; and when it's time to drop the reference it is important to call `entity.Release(this)`;. Those calls increase and decrease the reference count. All internal classes of Entitas-CSharp are respecting this mechanism and so should your code. If you don't call `Retain` while keeping a reference to an entity, you might end up holding a reference to an entity which was destroyed and reborn as something else. If you forget to call `Release` on an entity which you retained, it will stay in the object pool forever, making your memory consumption grow over time.

### Entity observation
An entity has multiple events which users can subscribe to, in order to have introspection into entity life cycle.
- OnComponentAdded
- OnComponentRemoved
- OnComponentReplaced
- OnEntityReleased
- OnDestroyEntity
Those events are the same events context uses to monitor entity. They are exposed for the external use as well, however I would not recommend to use them directly. In a typical use case you rather want to have a group, collector or a reactive system.

## Context
The Context is the factory where you create and destroy entities. This way a context can manage the life cycle of all entities we create. It also is the first observer which get notified when we manipulate an entity.
```C#
// Contexts.game is kindly generated for you by the code generator
var gameContext = Contexts.game;
var entity = gameContext.CreateEntity();
entity.isMovable = true;

// Returns all entities having MovableComponent and PositionComponent.
// Matchers are also generated for you.
var entities = gameContext.GetEntities(Matcher<GameEntity>.AllOf(GameMatcher.Movable, GameMatcher.Position));
foreach (var e in entities) {
    // do something
}
```

### Entity object pool
In order to avoid garbage collection, a context in Entitas-CSharp has an internal object pool. It contains destroyed entities, which will be used when a user creates a new entity. This way memory on the heap gets recycled. An entity can only be recycled, when we can be sure that no one holds a reference to this entity any more. This is why Entitas-CSharp has an internal reference count mechanism. If you use only stock Entitas and do not hold any references to entites your self, you don't have to think about it. The internal classes already taking care of all reference counting for you. 

If however you want to create a something like this:
```C#
class Neighbour: IComponent {
    public IEntity reference;
}

class EntityLink : MonoBehaviour {
    IEntity _entity;
}
```
Than you would need to call `_entity.Retain(this)`; when you store the reference. And you should not forget to call `_entity.Release(this)`; when you are not interested in the entity any more. We discourage components which have references to another entity in favour of an entity index.

### Multiple context types
A component is a column, an entity is a row and context is a table itself. Now in relational databases a table is defined by a schema.In Entitas it is based on classes which implement IComponent. This implies that when we define more component classes, our table becomes broader.Dependent on implementation detail, it can have an implication on memory consumption. 

In order to tackle the growing table size, we can just introduce another table. Here is a snippet from Entitas-Csharp Wiki:
```C#
using Entitas;
using Entitas.CodeGenerator;

[Game, UI]
public class SceneComponent : IComponent
{
    public Scene Value;
}

[Game]
public class Bullet
{
    // Since it doesn't derive from 'IComponent'
    // it will be generated as 'BulletComponent'
}

[Meta]
public struct EditorOnlyVisual
{
    public bool ShowInMode;

    public EditorOnlyVisual(bool show) {
        this.ShowInMode = show;
    }
}
```
In this particular example we have a `Game`, `Meta` and `UI` context. As you can see with `SceneComponent`, one component can be part of multiple contexts. 

### How many context types should I have?
This really depends on your use case. You just have to keep in mind than an entity is backed by an array of Icomponents meaning that it is an array of pointers and a pointer is 8bytes big on an 64bit architecture.  If you have 100 entites in your game, they take up 40KB. 

### Context observation
And this is also what we use internally for groups (described in its own chapter) and visual debugger. If you want to write some tooling for Entitas e.g. custom Logging or profiling you can use follwoing events:
- OnEntityCreated
- OnEntityWillBeDestroyed
- OnEntityDestroyed
- OnGroupCreated


## Group
Groups enable super quick filtering on entities in the context. They are continuously updated when entities change and can return groups of entities instantly.
```C#
var context = contexts.game;

var movables = context.GetGroup(GameMatcher.Movable);
var count = movables.count; // count is 0, the group is empty

var entity1 = context.CreateEntity();
entity1.isMovable = true;
var entity2 = context.CreateEntity();
entity2.IsMovable = true;

count = movables.count; // count is 2, the group contains the entity1 and entity2

// GetEntities() always provides an up to date list
var movableEntities = movables.GetEntities();
foreach (var e in movableEntities) {
  
}

entity1.Destroy();
entity2.Destroy();

count = movables.count; // count is 0, the group is empty
```
Both the group and fetched entities are cached, so even calling this method multiple times is super fast.Always prefer using groups when possible. 
`gameContext.GetEntities(GameMatcher.Movable)` internally uses groups, too.

### Group observation
Groups have events for `OnEntityAdded`, `OnEntityRemoved` and `OnEntityUpdated` to directly react to changes in the group. Even more importantly is to understand than, when we replace a component on an entity, old component will be removed and new component will be added.
Other ingredients like Collector, Index and Reactive system are using the same events. So, for day to day work, you probably can use those. 
```C#
gameContext.GetGroup(GameMatcher.Position).OnEntityAdded += (group, entity, index, component) => {
    // Do something
};
```

### Matcher
Matchers are generated by the code generator and can be combined. Matchers are usually used to get groups of entities from the context of interest. Remember to prefix the matcher with the context name you are interested (e.g. GameMatcher, InputMatcher etc).

In order to define more complex groups, we can use `AllOf`, `AnyOf` and `NoneOf` methods.
- `AllOf` means that all the listed components has to be present on the entity in order for this entity to become part of the group. 
- `AnyOf` means that one of the listed component has to be present. 
- And in case of `NoneOf` we don't want the listed components to be present. `NoneOf` is not a stand alone description, meaning that you will not be able to write `context.GetGroup(GameMatcher.NoneOf(GameMatcher.Position))`; It is prohibited because it creates a very large set. NoneOf can be used only in combination with `AllOf` or `AnyOf`.
```C#
var matcher = GameMatcher.Movable;

GameMatcher.AllOf(GameMatcher.Movable, GameMatcher.Position);

GameMatcher.AnyOf(GameMatcher.Move, GameMatcher.Position);

GameMatcher
    .AllOf(GameMatcher.Position)
    .AnyOf(GameMatcher.Health, GameMatcher.Interactive)
    .NoneOf(GameMatcher.Animating);
```

## Collector
The Collector provides an easy way to react to changes in a group over time.
```C#
context.CreateCollector(GameMatcher.GameBoardElement.Removed());
```
We define that we want to collect all entities which got `GameBoardElement` component removed. Internally a collector will ask for a group of entities which contain `GameBoardElement` components. It will subscribe it self to group events and keep a list of references to entities which will leave the group, as we were interested in Removed event. 
Also important to notice, when an entity got collected as removed from a group. It will still stay collected even if we add a `GameBoardElement` component to it again and there for it will be added to the group again. This is why reactive systems has to implement `Filter` method.

A collector can also be created with an array of groups and events. Meaning that we can observe multiple groups and keep a joined list of changed entites.
```C#
var group = gameContext.GetGroup(GameMatcher.Position, GameMatcher.View);
var collector = group.CreateCollector(GroupEvent.Added);
```
We can iterate over collected entities and clear them out.
```C#
foreach (var e in collector.collectedEntities) {
    // do something with all the entities
    // that have been collected to this point of time
}
collector.ClearCollectedEntities();
```
A collector can be activated and deactivated, so that we can stop and resume the observing of the group. 
```C#
collector.Activate();

collector.Deactivate();
```

There are follwoing three events that we can be interested in:
- Added
- Removed
- AddedOrRemoved

## Index
However, what about cases, where we want to get entities on a certain position. We could iterate over all entites which have a position and collect only those which have desired position. Or we could use an Index.
```C#
[Game]
public sealed class PositionComponent : IComponent {

    [EntityIndex]
    public IntVector2 value;
}
```
The `EntityIndex` annotation will tell the code generator to create API on context so that user will be able to get entities by given `IntVector2` value.

We ask context to give us all entities on position, where the "input" was effected and we filter out entites which are not interactive:
```C#
foreach (var e in _contexts.game.GetEntitiesWithPosition(
                    new IntVector2(input.x, input.y)
                  ).Where(e => e.isInteractive)) {
    e.isDestroyed = true;
}
```

Internally an index is a group observer. It is created on context initialisation, subscribing to group events from the beginning. When we start to create entities and add components to them, they will start entering groups and the index will be notified that an entity was added with following component.We can use the value of the component as a key in a `HashMap`, where the value is the entity itself. When we replace or remove component, the index is notified by the group as well.

In Entitas-CSharp we have two types of built in indexes: `EntityIndex` and `PrimaryEntityIndex`.  An `EntityIndex` is backed by a HashMap which stores a set of entities as value. Meaning - you could have multiple entities on the same position. `PrimaryEntityIndex` makes sure that every key is associated with only one entity. This is very good if you have an `Id` component and you want to look up entities by this Id. This is also what we recommend, when you need to store a reference from one entity to another.

## Systems
There are 5 different types of Systems:
- **IInitializeSystem:** Executes once (`system.Initialize()`)
- **IExecuteSystem:** Executes every frame (`system.Execute()`)
- **ICleanupSystem:** Executes every frame after the other systems are finished (`system.Cleanup()`)
- **ITearDownSystem:** Executes once, after the game ends (`system.TearDown()`)
- **ReactiveSystem:** Executes when the observed group changed (`system.Execute(Entity[])`)

Recommend create systems for each single task or behaviour in application and execute them in a defined order. This helps to keep app deterministic.
```C#
var systems = new Systems()
    .Add(new SomeInitializeSystem(contexts))
    .Add(new SomeReactiveSystem(contexts))
    .Add(new SomeExecuteSystem(contexts));
```

### InitializeSystem
Initialize systems run once at the start of your program. They implement the interface `IInitializeSystem`, which defines the method `Initialize()`. This is where you set up your initial game state, in a similar way to Unity's `Start()` method.

Possible Use Case:
- Add event handlers to `Groups` or `Contexts`
- Create global entities that will exist for the lifetime of your game (e.g. for accessing configuration data)

```C#
public class CreateLevelSystem : IInitializeSystem {
    public void Initialize() {}
}
```

### ExecuteSystem
Execute systems run once per frame. They implement the interface `IExecuteSystem`, which defines the method `Execute()`. This is where you put code that needs to run every frame, similar to Unity's `Update()` method.

Possible Use Cases:
- Poll for events not represented as components.

```C#
public class MoveSystem : IExecuteSystem {
    public void Execute() {}
}
```

### CleanupSystem
Cleanup systems run at the end of each frame, after all other systems have completed their work. They implement the interface `ICleanupSystem`, which defines the method `Cleanup()`. These are useful if you want to create entities that only exist for one frame.

```C#
public class MyCleanupSystem : ICleanupSystem {
    public void Cleanup() {}
}
```

### TearDownSystem
Teardown systems run once at the end of your program. They implement the interface `ITearDownSystem`, which defines the method `TearDown()`. This is where you can clean up all resources acquired throughout the lifetime of your game.

Possible Use Cases:
- Release all resources not managed by Unity
- Flush modified files (e.g. save data, logs) to disk
- Gracefully terminate network connections

```C#
public class MyTearDownSystem : ITearDownSystem {
    public void TearDown() {}
}
```

### ReactiveSystem
Entitas also provides a special system called ReactiveSystem, which is using a Group Observer under the hood. 

Unlike the other systems, ReactiveSystems inherit from a base class `ReactiveSystem<TEntity>` instead of implementing an interface.     
Entitas generates an entity Type for each context in your game. If your contexts are `Game`, `GameState` and `Input`, Entitas generates three types: `GameEntity`, `GameStateEntity` and `InputEntity`. Reactive systems require that you provide the specific context and associated entity type to which they react.

The base class defines some abstract methods you must implement. First you must create a constructor that calls the base constructor and provides it with the appropriate context. You must override 3 methods: `GetTrigger()` returns a collector, this tells the system what events to react to. `Filter()` performs a final check on the entities returned by the collector, ensuring they have the required components attached before `Execute()`  is called on each of them. Execute() is where the bulk of your game logic resides.

Note: You should not try to combine a reactive system with an execute system - think of reactive systems as a special case of execute systems. All the other interfaces can be mixed.

```C#
using Entitas;
using UnityEngine;

[Game]
PositionComponent : IComponent {
    int x;
    int y;
}

[Game]
ViewComponent : IComponent {
    GameObject gameObject;
}

public class RenderPositionSystem: ReactiveSystem<GameEntity> {

    public RenderPositionSystem(Contexts contexts) : base(contexts.Game) {}

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        // specify which component you are reacting to
        return context.CreateCollector(GameMatcher.Position);

        // you can also specify which type of event you need to react to
        return context.CreateCollector(GameMatcher.MyComponent.Added()); // the default
        return context.CreateCollector(GameMatcher.MyComponent.Removed());
        return context.CreateCollector(GameMatcher.MyComponent.AddedOrRemoved());

        // combine matchers with AnyOf and AllOf
        return context.CreateCollector(LevelMatcher.AnyOf(GameMatcher.Component1, GameMatcher.Component2));

        // use multiple matchers
        return context.CreateCollector(LevelMatcher.GameMatcher, GameMatcher.Component2.Removed());

        // or any combination of all the above
        return context.CreateCollector(LevelMatcher.AnyOf(GameMatcher.Component1, GameMatcher.Component2),
                                       LevelMatcher.Component3.Removed(),
                                       LevelMatcher.AllOf(GameMatcher.C4, GameMatcher.C5).Added());
    }

    protected override bool Filter(GameEntity entity) {
        // check for required components (here it is position and view)
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var e in entities) {
            var pos = e.gridPosition.position;
            e.view.gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }
}
```

To react to changes of entities from multiple contexts you will need to use multi-reactive system. First you need to declare an interface that will combine entities from multiple contexts that have the same components, and you need to implement that interface for the entity classes via partial classes.
```C#
public interface PositionViewEntity : IEntity, IPosition, IView {}

public partial class EnemyEntity : PositionViewEntity {}
public partial class ProjectileEntity : PositionViewEntity {}
```
Then create a system inherited from MultiReactiveSystem and pass the new interface.
```C#
public class ViewSystem : MultiReactiveSystem<PositionViewEntity, Contexts> {

    public ViewSystem(Contexts contexts) : base(contexts) {}

    protected override ICollector[] GetTrigger(Contexts contexts) {
        return new ICollector[] {
            contexts.Enemy.CreateCollector(EnemyMatcher.Position),
            contexts.Projectile.CreateCollector(ProjectileMatcher.Position)
        };
    }

    protected override bool Filter(PositionViewEntityentity) {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<PositionViewEntity> entities) {
        foreach(var e in entities) {
            e.View.transform.position = e.Position.value;
        }
    }
}
```

### Features
Use them to group related systems together. This has the added benefit of separating the visual debugging objects for your systems in the Unity hierarchy. Now they can be inspected in logical groups instead of all at once.

Features also help you to enforce broader paradigmatic rules in your project. The order of execution of features is determined by the order in which they're added and is always respected by Entitas. Separating your systems into `InputSystems : Feature`,  `GameLogicSystems : Feature` and `RenderingSystems : Feature` then initializing them in that order provides an easy way of ensuring that game logic doesn't interfere with rendering.

Features require that you implement a constructor. Use the `Add()` method in the ctor to add systems to the feature. The order in which they are added here defines their execution order at runtime. Features can be used in your GameController to instantiate groups of systems together.

```C#
public class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("Input Systems")
    {
        // order is respected 
        Add(new EmitInputSystem(contexts));
        Add(new ProcessInputSystem(contexts));
    }
}
```

Then in your GameController:
```C#
Systems createSystems(Contexts contexts) {

     // order is respected
     return new Feature("Systems")

         // Input executes first
         .Add(new InputSystems(contexts))
         // Update 
         .Add(new GameBoardSystems(contexts))
         .Add(new GameStateSystems(contexts))
         // Render executes after game logic 
         .Add(new ViewSystems(contexts))
         // Destroy executes last
         .Add(new DestroySystem(contexts));
}
```

### Example Mixed System
This system is both an Execute and a Cleanup system. Its function is to monitor Unity's Input class for mouse clicks and create entities with InputComponent added. A separate system processes these components, then, in the Cleanup phase these entities are destroyed.

The advantage of this arrangement is that we could have multiple separate systems listening for `InputComponent` and doing different things with them. None of these systems should be responsible for destroying the entities they process, since we may later add more systems or remove existing ones. Still the entity should be destroyed before the next frame since we will never need it again.

```C#
using Entitas;
using UnityEngine;

public class EmitInputSystem : IExecuteSystem, ICleanupSystem {

    readonly InputContext _context;
    readonly IGroup<InputEntity> _inputs;

    // get a reference to the group of entities with InputComponent attached 
    public EmitInputSystem(Contexts contexts) {
        _context = contexts.input;
        _inputs = _context.GetGroup(InputMatcher.Input);
    }

    // this runs early every frame (defined by its order in GameController.cs)
    public void Execute() {

        // check for unity mouse click
        var input = Input.GetMouseButtonDown(0);        
         
        if(input) {
            // perform a raycast to see if we clicked an object
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100);

            if(hit.collider != null) {

                // we hit an object, so this is a valid input.
                // create a new entity to represent the input
                // give it the position of the object we hit

                var pos = hit.collider.transform.position;
                _context.CreateEntity()
                     .AddInput((int)pos.x, (int)pos.y);
            }
        }
    }

    // all other systems are done so we can destroy the input entities we created
    public void Cleanup() {
        // group.GetEntities() always provides an up-to-date list
        foreach(var e in _inputs.GetEntities()) {
            e.Destroy();
        }
    }
}
```

### Careful with AnyOf based collector
When you create a collector which whatches a group based on AnyOf matcher, you probably will get an unexpected result, as when you have components A and B and you have an AnyOf(A, B) group. An entity will enter a group only when one of the components is added, when we add the second component, the entity is still in the group so it is not Added and therefore it is not collected. This is however probably not what you want to have. Normally people want to see entities collected when any of the two components are added. In this case what you should do is to setup a collector with two distinct groups and not one AnyOf group. 

## Attributes

The code generator currently supports the following attributes to be used with classes, interfaces and structs :
- `[Context]`: You can use this attribute to make a component be available only in the specified context(s); e.g., `[MyContextName]`, `[Enemies]`, `[UI]`, etc. Improves memory footprint. It can also create components.
When used simply as `[Context]` on a `class`, `interface` or `struct` the code generator will create a new `class` for you with the suffix `Component` and add this new class to the default context;
- `[Unique]`: The code generator will provide additional methods to ensure that up to a maximum of one entity with this component exists.
It will generate the following additional properties and methods for the component: `Context.{ComponentName}Entity`.
- `[FlagPrefix]`: Can be used to support custom prefixes for flag components only.
- `[PrimaryEntityIndex]`: Can be used to limit entities to a unique component value.
- `[EntityIndex]`: Can be used to search for entities with a component value.
- `[CustomComponentName]`: Generates multiple components with different names for one class or interface. This can be used to to enforce uniformity across multiple components and avoid the tedious task of writing all the components individually.
- `[DontGenerate]`: The code generator will not process components with this attribute.
- `[Event]`: The code generator will generate components, systems and interface to support reactive UI. Eliminate the need to write `RenderXSystems`.
- `[Cleanup]`(Asset Store only): The code generator will generate systems to remove components or destroy entities.

### Event 
`[Event({EventTarget}, {EventType}, {priority})]`

EventTarget
- `.Self`: View's `OnPosition` will be called only when the listened `GameEntity`'s position is changed.
- `.Any`: View's `OnPosition` will be call when any `GameEntity`'s position is changed.
First parameter of `OnPosition` is the entity whose Position has changed.

EventType
- `.Added` (default): Will generate `IPositionListener`.
- `.Removed`: Will generate `IPositionRemovedListener`.

priority
Decide generated systems' execution order.

Possible Use Cases
- Playing animations
- Playing sound effects
- Updating UI (e.g. for score)
- Any other interaction with APIs that exist outside of your game logic

```C#
[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent
{
  public float x;
  public float y;
}

public class GameView: Monobehaviour, IPositionListener
{
  // Function to call after adding this View to a GameEntity
  public void RegisterListeners(Contexts contexts, GameEntity entity)
  {
    entity.AddGamePositionListener(this);
  }
  
  public void OnPosition(GameEntity entity, float x, float y)
  {
    transform.position = new Vector2(x,y);
  }
}

// using the same GameController from HelloWorld tutorial
public class GameController : MonoBehaviour
{
  ...
  private static Systems CreateSystems(Contexts contexts)
    {
      return new Feature("Systems")
        // Your systems here
        .Add(new GameEventSystems(contexts));
    }
  }
}
```