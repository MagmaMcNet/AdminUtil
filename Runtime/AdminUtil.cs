using UnityEngine;
using VRC.SDKBase;

namespace MagmaMc.AdminUtil
{
    public class AdminUtil: ConfigRequestHandler
    {
        
        public GameObject BannedObject;

        public string[] AdminList = new string[0];
        public string[] BannedList = new string[0];

        public void Awake()
        {
            BannedObject.SetActive(false);
        }
        public override void OnStart()
        {
            base.OnStart();
        }

        public override void OnStringDownloaded(string DownloadedString)
        {
            AdminList = DownloadedString.GetFromKey("Administrators").ToLower();
            BannedList = DownloadedString.GetFromKey("BannedPlayers").ToLower();
        }
        public void FixedUpdate()
        {
            if (BannedListContains(Networking.LocalPlayer) && !BannedObject.activeInHierarchy)
            {
                BannedObject.SetActive(true);
                BannedObject.transform.position = new Vector3(2000, 2000, 2000);
            }
        }

        public bool AdminListContains(string Player) => ArrayContains(AdminList, Player, false);
        public bool BannedListContains(string Player) => ArrayContains(BannedList, Player, false);
        public bool AdminListContains(VRCPlayerApi Player) => ArrayContains(AdminList, Player.displayName, false);
        public bool BannedListContains(VRCPlayerApi Player) => ArrayContains(BannedList, Player.displayName, false);
        public bool AdminListContains(int Player) => ArrayContains(AdminList, VRCPlayerApi.GetPlayerById(Player).displayName, false);
        public bool BannedListContains(int Player) => ArrayContains(BannedList, VRCPlayerApi.GetPlayerById(Player).displayName, false);
        private static bool ArrayContains(string[] array, string value, bool CaptialSensitve = true, bool WeakCheck = false)
        {
            if (!CaptialSensitve)
                value = value.ToLower();
            foreach (string item in array)
            {
                string Item = !CaptialSensitve ? item.ToLower() : item;
                if (WeakCheck)
                {
                    if (Item.Contains(value))
                        return true;
                }
                else
                {
                    if (Item == value)
                        return true;
                }
            }
            return false;
        }

    }
}
