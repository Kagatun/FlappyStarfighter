using System.Runtime.InteropServices;
using UnityEngine;
using YG;

namespace Scripts.Input
{
    public class DeviceDetector : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern int DetectDevice();

        private void Start()
        {
            if (YG2.isSDKEnabled)
                IdentifyDevice();
        }

        private void IdentifyDevice()
        {
            int deviceType = 1;

            if (Application.platform == RuntimePlatform.WebGLPlayer)
                deviceType = DetectDevice();

            if (deviceType == 0)
                YG2.saves.IsDesktop = false;
            else
                YG2.saves.IsDesktop = true;

            YG2.SaveProgress();
        }
    }
}