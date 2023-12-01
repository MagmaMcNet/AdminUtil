#define MAGMAMC_ADMINSYSTEM
#define MAGMAMC_ADMINSYSTEM_0_3
#define MAGMAMC_ADMINSYSTEM_0_3_OR_NEWER
#define MAGMAMC_ADMINSYSTEM_0_2_OR_NEWER

using UnityEngine;
using VRC.SDKBase;

namespace MagmaMc.AdminUtil
{
    public class AdminUtil: IAdminUtil
    {

        public GameObject BannedObject;

        public override string[] AdminList { get; set; } = new string[0];
        public override string[] BannedList { get; set; } = new string[0];

        public void Awake() =>
            BannedObject.SetActive(false);

        public override void OnStart() =>
            base.OnStart();

        public override void OnStringDownloaded(string DownloadedString)
        {
            AdminList = DownloadedString.GetFromKey("Administrators").ToLower();
            BannedList = DownloadedString.GetFromKey("BannedPlayers").ToLower();
        }

        public void FixedUpdate()
        {
            if (BannedListContains(Networking.LocalPlayer) && !BannedObject.activeInHierarchy)
                BannedObject.SetActive(true);
        }

        public override bool AdminListContains(string Player) => ArrayContains(AdminList, Player, false);
        public override bool BannedListContains(string Player) => ArrayContains(BannedList, Player, false);
        public override bool AdminListContains(VRCPlayerApi Player) => ArrayContains(AdminList, Player.displayName, false);
        public override bool BannedListContains(VRCPlayerApi Player) => ArrayContains(BannedList, Player.displayName, false);
        public override bool AdminListContains(int Player) => ArrayContains(AdminList, VRCPlayerApi.GetPlayerById(Player).displayName, false);
        public override bool BannedListContains(int Player) => ArrayContains(BannedList, VRCPlayerApi.GetPlayerById(Player).displayName, false);

        public override bool IsAdmin(VRCPlayerApi Player) => AdminListContains(Player.displayName);
        public override bool IsAdmin(string Player) => AdminListContains(Player);
        public override bool IsAdmin(int Player) => AdminListContains(Player);

        public override bool IsBanned(VRCPlayerApi Player) => BannedListContains(Player.displayName);
        public override bool IsBanned(string Player) => BannedListContains(Player);
        public override bool IsBanned(int Player) => BannedListContains(Player);

    }
}
