using UnityEngine;

public class PushSetTag : MonoBehaviour
{
    public void SetTag(int num)
    {
        Pushwoosh.Instance.SetIntTag("Subscription purchased", num);
        Debug.Log($"[PushNotificator] PushSetTag: SetIntTag: Subscription purchased, {num}");
    }
}
