using com.adjust.sdk;
using UnityEngine;

namespace Integrations_WHITEOUT
{
    public class AdjustLoader : MonoBehaviour
    {
        [SerializeField] private AdjustEnvironment _adjustEnvironment;
        [SerializeField] private AdjustLogLevel _adjustLogLevel;
        [SerializeField] private string _appToken = "m3vi7aesgw00";
        private static bool isInit = false;

        private void Awake()
        {
            //Initialize_WHITEOUT();
        }

        public void Initialize(bool isInit)
        {
            if (isInit)
            {
                Initialize_WHITEOUT();
            }
        }

        private void Initialize_WHITEOUT()
        {
            if (isInit)
            {
                Destroy(gameObject);
                return;
            }
            isInit = true;

            var adjustObject = new GameObject("AdjustObject");
            var adjustComponent = adjustObject.AddComponent<Adjust>();
            adjustComponent.startManually = true;
            adjustComponent.appToken = _appToken;
            adjustComponent.environment = _adjustEnvironment;
            adjustComponent.logLevel = _adjustLogLevel;

            var config = new AdjustConfig(_appToken, _adjustEnvironment, _adjustLogLevel == AdjustLogLevel.Suppress);
            config.setLogLevel(_adjustLogLevel);
            Adjust.start(config);

            DontDestroyOnLoad(adjustObject);
            RequestTrackingAuthorization_WHITEOUT();
            Destroy(gameObject);
        }

        private void RequestTrackingAuthorization_WHITEOUT()
        {
            Adjust.requestTrackingAuthorizationWithCompletionHandler(HandleTrackingAuthorizationResponse_WHITEOUT);
        }

        private void HandleTrackingAuthorizationResponse_WHITEOUT(int status)
        {
            switch (status)
            {
                case 0:
                    Debug.Log("The user has not responded to the access prompt yet.");
                    break;
                case 1:
                    Debug.Log("Access to app-related data is blocked at the device level.");
                    break;
                case 2:
                    Debug.Log("The user has denied access to app-related data for device tracking.");
                    break;
                case 3:
                    Debug.Log("The user has approved access to app-related data for device tracking.");
                    break;
            }
        }
    }
}
