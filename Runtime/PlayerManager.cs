
using Miner28.UdonUtils;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerManager: UdonSharpBehaviour
{
    [Tooltip("StringDownloader UdonGraph Program")]
    [SerializeField] UdonBehaviour StringDownloader;
    [Tooltip("Raw URL For Config It Is Recommended To Use Github.io To Host The Files")]
    [SerializeField] VRCUrl ConfigURL = new VRCUrl("https://magmamcnet.github.io/MagmaMc.AdminUtil/PlayerManager.example.txt");
    [Range(4, byte.MaxValue)]
    [Tooltip("How Long It Will Wait To Send New Request To Download New Config From The Server")]
    [SerializeField] byte DownloadDelay = 10;

    [HideInInspector] public string[] AdminList = new string[0];
    [HideInInspector] public string[] BannedList = new string[0];

    void Awake()
    {
    }
    public void Start()
    {
        StringDownloader.SetProgramVariable("Reciever", this);
        StringDownloader.SetProgramVariable("OnFinishedEvent", "OnStringDownloaded");
        StringDownloader.SetProgramVariable("URL", ConfigURL);
        StringDownloader.SendCustomEvent("InvokeDownload");
    }
    public void OnStringDownloaded()
    {
        string DownloadedString = (string)StringDownloader.GetProgramVariable("StringDownloaded");
        AdminList = GetArrayFrom(DownloadedString, "Administrators");
        BannedList = GetArrayFrom(DownloadedString, "BannedPlayers");
        StringDownloader.SendCustomEventDelayedSeconds("InvokeDownload", DownloadDelay);
    }
    private static string[] GetArrayFrom(string data, string Key)
    {
        string[] ConfigData = data.Split('\n');
        string[] ReturnValue = new string[0];
        int StartIndex = 0;
        for (int i = 0; i < ConfigData.Length; i++)
        {
            if (ConfigData[i] == "--" + Key + "--")
            {
                StartIndex = i + 1; break;
            }
        }
        for (int i = StartIndex; i < ConfigData.Length; i++)
        {
            if (ConfigData[i].StartsWith("--") || ConfigData[i].EndsWith("--"))
                break;
            if (!string.IsNullOrEmpty(ConfigData[i]))
                ReturnValue = ReturnValue.Add(ConfigData[i]);

        }
        return ReturnValue;
    }
}
