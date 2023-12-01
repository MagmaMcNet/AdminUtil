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
        VRCPlayerApi LocalPlayer = Networking.LocalPlayer;

        if (Vector3.Distance(LocalPlayer.GetPosition(), transform.position) > MaxDistance)
        {
            Quaternion TeleportRot = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Quaternion InvPlayerRot = Quaternion.Inverse(Networking.LocalPlayer.GetRotation());
            VRCPlayerApi.TrackingData origin = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Origin);

            Vector3 targetPos = transform.position + TeleportRot * InvPlayerRot * (origin.position - LocalPlayer.GetPosition());
            Quaternion targetRot = TeleportRot * (InvPlayerRot * origin.rotation);

            if (RemoveVelocity)
                LocalPlayer.SetVelocity(Vector3.zero);

            LocalPlayer.TeleportTo(targetPos, targetRot, VRC_SceneDescriptor.SpawnOrientation.AlignRoomWithSpawnPoint, true);

        }
    }
}
