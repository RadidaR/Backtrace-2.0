using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised()
    { Response.Invoke(); }
}
