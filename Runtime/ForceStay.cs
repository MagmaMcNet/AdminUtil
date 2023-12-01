using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class ForceStay: UdonSharpBehaviour
{
    [Header("Util For Keeping Players In One Spot")]
    public uint MaxDistance = 15;
    public bool RemoveVelocity = true;
    public bool SetRotation = true;
    void FixedUpdate()
    {
        if (Vector3.Distance(Networking.LocalPlayer.GetPosition(), transform.position) > MaxDistance)
        {
            Teleport(Networking.LocalPlayer, transform.position, (SetRotation ? transform.rotation : Networking.LocalPlayer.GetRotation()), true);
            if (RemoveVelocity)
                Networking.LocalPlayer.SetVelocity(Vector3.zero);
        }
    }

    public static void Teleport(VRCPlayerApi player, Vector3 teleportPos, Quaternion teleportRot, bool lerpOnRemote = false)
    {
        teleportRot = Quaternion.Euler(0, teleportRot.eulerAngles.y, 0);

        Quaternion invPlayerRot = Quaternion.Inverse(player.GetRotation());

        VRCPlayerApi.TrackingData origin = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Origin);

        Vector3 targetPos = teleportPos + teleportRot * invPlayerRot * (origin.position - player.GetPosition());
        Quaternion targetRot = teleportRot * (invPlayerRot * origin.rotation);

        player.TeleportTo(targetPos, targetRot, VRC_SceneDescriptor.SpawnOrientation.AlignRoomWithSpawnPoint, lerpOnRemote);
    }
}
