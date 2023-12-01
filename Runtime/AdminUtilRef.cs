using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace MagmaMc.AdminUtil
{
    public class AdminUtilRef: UdonSharpBehaviour
    {
        [Header("AdminUtil Referance")]
        [Tooltip("Required Object For Syncing Admin Data And Contains Server Synced Functions")]
        [InspectorName("Admin Util")]
        public IAdminUtil _AdminUtil;
        public bool IsAndroid =>
#if UNITY_ANDROID
        true;
#else
            false;
#endif
        public static VRCPlayerApi[] ConnectedPlayers { get => GetConnectedPlayers(); }
        public static VRCPlayerApi[] GetConnectedPlayers()
        {
            VRCPlayerApi[] vRCPlayers = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(vRCPlayers);
            return vRCPlayers;
        }

        public ushort GetStaffCount()
        {
            ushort Count = 0;
            foreach (VRCPlayerApi user in GetConnectedPlayers())
            {
                if (_AdminUtil.AdminListContains(user))
                    Count++;
            }
            return Count;
        }
        public string GetMaster()
        {
            foreach (VRCPlayerApi user in GetConnectedPlayers())
            {
                if (user.isMaster)
                    return user.displayName;
            }
            return "[Undetected]";
        }

        public virtual void InitializedAwake()
        {

        }


        public virtual void Awake()
        {
            if (_AdminUtil == null)
            {
                Debug.Log("[<color=#FF4500>AdminUtil</color>] AdminUtilRef Is Null Searching For Object...");
                IAdminUtil _RootObject;
                GameObject _Object;

                // Attempt 1
                _RootObject = gameObject.GetComponent<IAdminUtil>();
                if (_RootObject != null)
                {
                    Debug.Log("[<color=#FF4500>AdminUtilRef</color>] AdminUtil Was Successfully Automatically Found! [1]");
                    _AdminUtil = _RootObject;
                    return;
                }
                // Attempt 2
                _Object = GameObject.Find("AdminUtil");
                if (_Object != null)
                {
                    _RootObject = _Object.GetComponent<IAdminUtil>();
                    if (_RootObject != null)
                    {
                        Debug.Log("[<color=#FF4500>AdminUtilRef</color>] AdminUtil Was Successfully Automatically Found! [2]");
                        _AdminUtil = _RootObject;
                        return;
                    }
                }
                // Attempt 3
                _Object = GameObject.Find("VRC");
                if (_Object != null)
                {
                    _RootObject = _Object.GetComponent<IAdminUtil>();
                    if (_RootObject != null)
                    {
                        Debug.Log("[<color=#FF4500>AdminUtilRef</color>] AdminUtil Was Successfully Automatically Found! [3]");
                        _AdminUtil = _RootObject;
                        return;
                    }
                }
                Debug.LogError("[<color=#FF4500>AdminUtilRef</color>] AdminUtil Was Not Successfully Automatically Found!");
                InitializedAwake();
            }
        }
    }
}