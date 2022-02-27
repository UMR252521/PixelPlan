using pixelplan.puremvc;
using UnityEngine;

namespace pixelplan
{
    public class Client : ComponentEx
    {
        [SerializeField]
        private bool CanShowLog = true;

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(this);
            Application.targetFrameRate = 60;
            SDKManager.Instance.OnInit();
            MyDebug._canShowLog = CanShowLog;
        }
    }
}
