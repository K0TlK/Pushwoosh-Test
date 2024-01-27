using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PushNotificator2 : MonoBehaviour
{
    [SerializeField] private string _applicationCode = "ENTER_PUSHWOOSH_APP_ID_HERE";
    [SerializeField] private TMP_Text _textHWID;
    [SerializeField] private TMP_Text _textLatPush;

    private void Awake()
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
    }

    void OnRegisteredForPushNotifications(string token)
    {
        Debug.Log("[PushNotificator] Application Code: " + _applicationCode);
        Debug.Log("[PushNotificator] HWID: " + Pushwoosh.Instance.HWID);
        Debug.Log("[PushNotificator] PushToken: " + Pushwoosh.Instance.PushToken);


        Pushwoosh.Instance.GetTags((IDictionary<string, object> tags, PushwooshException error) => {
            string json = PushwooshUtils.DictionaryToJson(tags);
            Debug.Log("Tags: " + json);
        });
        Debug.Log(Pushwoosh.Instance.GetRemoteNotificationStatus());
        _textHWID.text = Pushwoosh.Instance.HWID;
    }

    void OnFailedToRegisteredForPushNotifications(string error)
    {
        Debug.Log("[PushNotificator] Error ocurred while registering to push notifications: \n" + error);
    }

    void OnPushNotificationsReceived(string payload)
    {
        Debug.Log("[PushNotificator] Received push notificaiton: \n" + payload);
        _textLatPush.text = $"Last Push: {DateTime.Now} || {payload}";
    }
}