using System.Collections.Generic;
using UnityEngine;

public class PushNotificator2 : MonoBehaviour
{
    [SerializeField] private string _applicationCode = "ENTER_PUSHWOOSH_APP_ID_HERE";

    private void Start()
    {
        Initialize_WHITEOUT();
    }

    private void Initialize_WHITEOUT()
    {

        Pushwoosh.ApplicationCode = _applicationCode;

        Pushwoosh.Instance.OnRegisteredForPushNotifications += OnRegisteredForPushNotifications;
        Pushwoosh.Instance.OnFailedToRegisteredForPushNotifications += OnFailedToRegisteredForPushNotifications;
        Pushwoosh.Instance.OnPushNotificationsReceived += OnPushNotificationsReceived;

        Pushwoosh.Instance.SetBadgeNumber(0);

        Pushwoosh.Instance.RegisterForPushNotifications();

        Pushwoosh.Instance.SetIntTag("Subscription purchased", 0);
        Debug.Log("[Pushwoosh] InitializationManager: SetIntTag: Subscription purchased, 0");
    }

    void OnRegisteredForPushNotifications(string token)
    {
        Debug.Log("[PushNotificator] Received token: \n" + token);
        Debug.Log("[PushNotificator] HWID: " + Pushwoosh.Instance.HWID);
        Debug.Log("[PushNotificator] PushToken: " + Pushwoosh.Instance.PushToken);


        Pushwoosh.Instance.GetTags((IDictionary<string, object> tags, PushwooshException error) => {
            string json = PushwooshUtils.DictionaryToJson(tags);
            Debug.Log("Tags: " + json);
        });
        Debug.Log(Pushwoosh.Instance.GetRemoteNotificationStatus());
    }

    void OnFailedToRegisteredForPushNotifications(string error)
    {
        Debug.Log("[PushNotificator] Error ocurred while registering to push notifications: \n" + error);
    }

    void OnPushNotificationsReceived(string payload)
    {
        Debug.Log("[PushNotificator] Received push notificaiton: \n" + payload);
    }
}