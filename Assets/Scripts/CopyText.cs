using TMPro;
using UnityEngine;

public class CopyText : MonoBehaviour
{
    [SerializeField] private TMP_Text _Text;

    private void Awake()
    {
        _Text = GetComponent<TMP_Text>();
    }

    public void CopyTextToClipboard()
    {
        GUIUtility.systemCopyBuffer = _Text.text;
    }
}
