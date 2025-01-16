using System.Collections.Generic;
using UnityEngine;
public class Observer : Singleton<Observer>
{
    public delegate void callBackEvent(object value);

    private Dictionary<GameEvent, HashSet<callBackEvent>> EventObserver = new Dictionary<GameEvent, HashSet<callBackEvent>>();

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void subscribeListener(GameEvent key, callBackEvent _callBack) // đăng kí lắng nghe sự kiện
    {
        if (EventObserver.ContainsKey(key)) return;

        Debug.Log("Đăng kí sự kiện");
        EventObserver.Add(key, new HashSet<callBackEvent>());
        EventObserver[key].Add(_callBack);
    }

    public void unsubscribeListener(GameEvent key, callBackEvent _callBack) // hủy lắng nghe sự kiện
    {
        if (!EventObserver.ContainsKey(key))
        {
            return;
        }

        if(EventObserver[key].Count > 0)
        {
            EventObserver[key].Remove(_callBack);
        }
        else
        {
            EventObserver.Remove(key);
        }
    }

    public void NotifyEvent(GameEvent key, object value)
    {
        foreach (KeyValuePair<GameEvent, HashSet<callBackEvent>> _event in EventObserver)
        {
            if (_event.Key == key)
            {
                foreach (var _action in _event.Value)
                {
                    _action.Invoke(value);
                }
            }
        }
    }
}

public enum GameEvent
{
    RewardExp,
    UpdateCurrentExp,
    UpdateUI
}
