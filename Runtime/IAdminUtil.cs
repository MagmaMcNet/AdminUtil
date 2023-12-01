using MagmaMc.AdminUtil;
using UnityEngine;
using VRC.SDKBase;

public abstract class IAdminUtil: ConfigRequestHandler
{
    public abstract string[] AdminList { get; set; }
    public abstract string[] BannedList { get; set; }


    public abstract bool AdminListContains(string Player);
    public abstract bool AdminListContains(VRCPlayerApi Player);
    public abstract bool AdminListContains(int Player);

    public abstract bool BannedListContains(string Player);
    public abstract bool BannedListContains(VRCPlayerApi Player);
    public abstract bool BannedListContains(int Player);


    /// <summary>
    /// Checks If The Player Is A Admin In This Instance
    /// </summary>
    /// <param name="Player"></param>
    /// <returns></returns>
    public abstract bool IsAdmin(string Player);
    /// <summary>
    /// Checks If The Player Is A Admin In This Instance
    /// </summary>
    /// <param name="Player"></param>
    /// <returns></returns>
    public abstract bool IsAdmin(VRCPlayerApi Player);
    /// <summary>
    /// Checks If The Player Is A Admin In This Instance
    /// </summary>
    /// <param name="Player"></param>
    /// <returns></returns>
    public abstract bool IsAdmin(int Player);

    /// <summary>
    /// Checks If The Player Is Currently Banned
    /// </summary>
    /// <param name="Player"></param>
    /// <returns></returns>
    public abstract bool IsBanned(string Player);
    /// <summary>
    /// Checks If The Player Is Currently Banned
    /// </summary>
    /// <param name="Player"></param>
    /// <returns></returns>
    public abstract bool IsBanned(VRCPlayerApi Player);
    /// <summary>
    /// Checks If The Player Is Currently Banned
    /// </summary>
    /// <param name="Player"></param>
    /// <returns></returns>
    public abstract bool IsBanned(int Player);

}
