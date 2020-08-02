using System.Collections.Generic;
using UnityEngine;

//Stolen from https://github.com/quill18/UnityCallbackAndEventTutorial
//Modified by Eric Sexton
public class GameEventSystem : MonoBehaviour
{
    // Use this for initialization
    void OnEnable()
    {
        __Current = this;
    }

    static private GameEventSystem __Current;
    static public GameEventSystem Current
    {
        get
        {
            if (__Current == null)
            {
                __Current = GameObject.FindObjectOfType<GameEventSystem>();
            }

            return __Current;
        }
    }

    delegate void EventListener(EventInfo ei);
    Dictionary<System.Type, List<EventListener>> eventListeners;

    public void RegisterListener<T>(System.Action<T> listener) where T : EventInfo
    {
        System.Type eventType = typeof(T);
        if (eventListeners == null)
        {
            eventListeners = new Dictionary<System.Type, List<EventListener>>();
        }

        if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
        {
            eventListeners[eventType] = new List<EventListener>();
        }

        // Wrap a type converstion around the event listener
        EventListener wrapper = (ei) => { listener((T)ei); };

        eventListeners[eventType].Add(wrapper);
    }

    public bool UnregisterListener<T>(System.Action<T> listener) where T : EventInfo
    {
        System.Type eventType = typeof(T);

        if (eventListeners == null || eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
        {
            return false;
        }

        return eventListeners.Remove(eventType);
    }

    public void FireEvent(EventInfo eventInfo)
    {
        System.Type trueEventInfoClass = eventInfo.GetType();
        if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
        {
            // No one is listening, we are done.
            return;
        }

        foreach (EventListener el in eventListeners[trueEventInfoClass])
        {
            el(eventInfo);
        }
    }
}