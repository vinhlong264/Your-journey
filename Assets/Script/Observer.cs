public class Observer
{
    public static System.Action<float> onGainReward;
    public static void callBackEvent(float reward)
    {
        onGainReward?.Invoke(reward);
    }
}
