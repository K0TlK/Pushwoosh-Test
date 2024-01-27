using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomEventPush : MonoBehaviour
{
    [SerializeField] private TMP_InputField _pushName;

    public void Push()
    {
        IDictionary<string, object> attributes = new Dictionary<string, object>();
        Pushwoosh.Instance.PostEvent(_pushName.text, attributes);
        Debug.Log("[CustomEventPush|CustomEventPush] PushWithAttributes " +
            $"Pushwoosh.Instance.PostEvent({_pushName.text}, ();");
    }
    public void PushWithAttributes()
    {
        IDictionary<string, object> attributes = new Dictionary<string, object>();
        attributes.Add("CustomPush", true);
        Pushwoosh.Instance.PostEvent(_pushName.text, attributes);
        Debug.Log("[CustomEventPush|CustomEventPush] PushWithAttributes " +
            $"Pushwoosh.Instance.PostEvent({_pushName.text}, (CustomPush, true));");
    }
}
