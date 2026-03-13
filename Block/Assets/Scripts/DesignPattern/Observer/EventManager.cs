using System;
using System.Collections.Generic;
public abstract class EventManager : Singleton<EventManager>
{
    private Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();
    protected override void Awake()
    {
        base.Awake();
    }
    public void Subscribe(string nameEvent, Action listener)
    {
        if(!this.eventDictionary.ContainsKey(nameEvent))
            this.eventDictionary.Add(nameEvent, listener);
        this.eventDictionary[nameEvent] = (Action)this.eventDictionary[nameEvent] + listener;
    }
    public void UnSubscribe(string nameEvent, Action listener)
    {
        if (this.eventDictionary.ContainsKey(nameEvent))
            this.eventDictionary[nameEvent] = (Action)this.eventDictionary[nameEvent] - listener;
        if (this.eventDictionary[nameEvent] == null) 
            this.eventDictionary.Remove(nameEvent);
    }
    public void TriggerEvent(string nameEvent)
    {
        if (this.eventDictionary.ContainsKey(nameEvent) && this.eventDictionary[nameEvent] is Action action)
            action.Invoke();
    }
}
