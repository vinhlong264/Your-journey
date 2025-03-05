using UnityEngine;

[CreateAssetMenu(fileName = "Event Bus" , menuName = "Event")]
public class EventBus : ScriptableObject
{
    public System.Action<object> EventCallBack;

    public void NotifyEvent(object Value)
    {
        EventCallBack?.Invoke(Value);
    }
}
