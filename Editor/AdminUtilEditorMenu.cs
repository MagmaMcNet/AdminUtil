using UnityEditor;
using UnityEngine;
using VRC.Udon;

namespace MagmaMc.AdminUtil.Editor
{
    public class AdminUtilEditorMenu: MonoBehaviour
    {
        [MenuItem("MagmaMc/AdminUtil/Create Instance")]
        static void CreateAdminUtilInstance()
        {
            AdminUtilEditorWindow window = EditorWindow.GetWindow<AdminUtilEditorWindow>("Create AdminUtil Instance");
            window.Show();
        }
    }

    public class AdminUtilEditorWindow: EditorWindow
    {
        private Vector3 BannedVec;
        private string configURL = "https://magmamcnet.github.io/AdminUtil/PlayerManager.example.txt";

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Create AdminUtil Instance", EditorStyles.boldLabel);

            BannedVec = EditorGUILayout.Vector3Field("Banned Position", BannedVec);
            configURL = EditorGUILayout.TextField("Config URL", configURL);

            if (GUILayout.Button("Create AdminUtil Instance"))
            {
                CreateAdminUtilInstance(BannedVec, configURL);
                Close();
            }
        }

        private void CreateAdminUtilInstance(Vector3 bannedObject, string configURL)
        {
            GameObject adminUtilObject = new GameObject("AdminUtil");
            adminUtilObject.transform.parent = null;

            adminUtilObject.transform.position = Vector3.zero;
            adminUtilObject.transform.rotation = Quaternion.identity;
            adminUtilObject.transform.localScale = Vector3.one;

            GameObject BanPlayer = new GameObject("BanPlayer");
            BanPlayer.transform.position = bannedObject;
            BanPlayer.transform.parent = adminUtilObject.transform;
            BanPlayer.SetActive(false);
            BanPlayer.AddComponent<ForceStay>().MaxDistance = 50;

            AdminUtil util = adminUtilObject.AddComponent<AdminUtil>();
            UdonBehaviour udonb = adminUtilObject.AddComponent<UdonBehaviour>();
            string path = "Packages/net.magma-mc.adminutil/Runtime/StringDownloader.asset";
            udonb.programSource = AssetDatabase.LoadAssetAtPath<AbstractUdonProgramSource>(path);
            util.StringDownloader = udonb;
            util.BannedObject = BanPlayer; 
            util.ConfigURL = new VRC.SDKBase.VRCUrl(configURL);
            Selection.activeObject = adminUtilObject;
        }
    }
}
