using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class ForceStay : UdonSharpBehaviour
{
    [Header("Util For Keeping Players In One Spot")]
    public uint MaxDistance = 15;
    public bool RemoveVelocity = true;
    public bool SetRotation = true;
    void FixedUpdate()
    {
        if (Vector3.Distance(Networking.LocalPlayer.GetPosition(), transform.position) > MaxDistance)
        {
            Networking.LocalPlayer.TeleportTo(transform.position, (SetRotation ? transform.rotation : Networking.LocalPlayer.GetRotation()));
            if (RemoveVelocity)
                Networking.LocalPlayer.SetVelocity(Vector3.zero);
        }
    }
}
