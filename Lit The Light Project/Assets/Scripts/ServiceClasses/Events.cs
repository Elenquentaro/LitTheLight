using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void EmptyAction();

public class EmptyEvent : UnityEvent { }

public class BoolEvent : UnityEvent<bool> { }

public class IntEvent : UnityEvent<int> { }

public class PositionEvent : UnityEvent<Vector3> { }

public class GameObjectEvent : UnityEvent<GameObject> { }

public class GenericEvent<T> : UnityEvent<T> { }