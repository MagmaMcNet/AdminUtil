using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace MagmaMc.AdminUtil
{
    public class ConfigRequestHandler: UdonSharpBehaviour
    {
        [Tooltip("StringDownloader UdonGraph Program")]
        [SerializeField] public UdonBehaviour StringDownloader;
        [Tooltip("Raw URL For Config It Is Recommended To Use Github.io To Host The Files")]
        [SerializeField] public VRCUrl ConfigURL = new VRCUrl("https://magmamcnet.github.io/AdminUtil/PlayerManager.example.txt");
        [Tooltip("How Long It Will Wait To Send New Request To Download New Config From The Server")]
        [Range(4, byte.MaxValue)]
        [SerializeField] public byte DownloadDelay = 10;
        /// <summary>
        /// Function is called when the config string has finished downloading
        /// </summary>
        /// <param name="ConfigData"></param>
        public virtual void OnStringDownloaded(string ConfigData)
        {
            
        }
        public void OnStringFailed()
        {
            Debug.LogError($"Failed To Download String From '{StringDownloader.GetProgramVariable("URL")}'");
        }
        public void Start()
        {
            StringDownloader.SetProgramVariable("Reciever", this);
            StringDownloader.SetProgramVariable("OnFinishedEvent", nameof(Internal_StringDownloaded));
            StringDownloader.SetProgramVariable("URL", ConfigURL);
            StringDownloader.SendCustomEvent("InvokeDownload");
            OnStart();
        }
        public virtual void OnStart()
        {
        }
        public void Internal_StringDownloaded()
        {
            string DownloadedString = (string)StringDownloader.GetProgramVariable("StringDownloaded");
            OnStringDownloaded(DownloadedString);
            StringDownloader.SendCustomEventDelayedSeconds("InvokeDownload", DownloadDelay);
        }
        protected static bool ArrayContains(string[] array, string value, bool CaptialSensitve = true, bool WeakCheck = false)
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
    public static class StringExtensions
    {
        public static T[] Add<T>(this T[] array, T item)
        {
            T[] newArray = new T[array.Length + 1];
            Array.Copy(array, newArray, array.Length);
            newArray[array.Length] = item;
            return newArray;
        }
        public static string[] ToLower(this string[] array)
        {
            string[] newArray = new string[array.Length];
            System.Array.Copy(array, newArray, array.Length);
            for (int i = 0; i < newArray.Length; i++)
                newArray[i] = newArray[i].ToLower();
            return newArray;
        }
        public static string[] ToUpper(this string[] array)
        {
            string[] newArray = new string[array.Length];
            System.Array.Copy(array, newArray, array.Length);
            for (int i = 0; i < newArray.Length; i++)
                newArray[i] = newArray[i].ToUpper();
            return newArray;
        }
        public static bool Contains(this string[] array, string value)
        {
            foreach (string item in array)
                if (item == value)
                    return true;
            return false;
        }

        /// <summary>
        /// Get Value Of Key From "Magma's Array Config Format"
        /// </summary>
        /// <param name="Key">The String Name Of The Variable Name To Locate Eg</param>
        /// <returns></returns>
        public static string[] GetFromKey(this string data, string Key)
        {
            string[] ConfigData = data.Split('\n');
            string[] ReturnValue = new string[0];
            int StartIndex = 0;
            for (int i = 0; i < ConfigData.Length; i++)
            {
                if (ConfigData[i].Contains( "--" + Key + "--"))
                {
                    StartIndex = i + 1; 
                    break;
                }
            }
            for (int i = StartIndex; i < ConfigData.Length; i++)
            {
                if (ConfigData[i].StartsWith("--") || ConfigData[i].EndsWith("--"))
                    break;
                if (!string.IsNullOrWhiteSpace(ConfigData[i]))
                {
                    ReturnValue = ReturnValue.Add(ConfigData[i].Trim());
                }

            }
            return ReturnValue;
        }

    }


}
