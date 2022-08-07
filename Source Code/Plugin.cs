using BepInEx;
using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using Utilla;

namespace ThatWasEasy
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>
    /// 
    [Description("HauntedModMenu")]
    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        static bool modEnabled;
        static Transform button;
        static GameObject thatWasEasyButton;

        void Awake()
        {
            Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            modEnabled = true;
            if (thatWasEasyButton != null)
            {
                thatWasEasyButton.SetActive(true);
            }
        }

        void OnDisable()
        {
            modEnabled = false;
            if (thatWasEasyButton != null)
            {
                thatWasEasyButton.SetActive(false);
            }
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ThatWasEasy.Resources.easybutton");
            AssetBundle assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);
            GameObject buttonTemporaryObject = assetBundle.LoadAsset<GameObject>("button");
            thatWasEasyButton = Instantiate(buttonTemporaryObject);

            thatWasEasyButton.transform.position = new Vector3(-0.028f, 0.002f, 0.011f);
            thatWasEasyButton.transform.rotation = Quaternion.Euler(-1.424f, -6.051f, 76.803f);
            thatWasEasyButton.transform.localScale = new Vector3(0.1925041f, 0.1925041f, 0.1925041f);
            thatWasEasyButton.transform.SetParent(GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/").transform, false);

            thatWasEasyButton.SetActive(modEnabled);

            button = thatWasEasyButton.transform.Find("Button/");
            button.gameObject.layer = 18;
            button.gameObject.AddComponent<ButtonManager>();
        }

        public static void ButtonPressFunction()
        {
            //button.GetComponent<AudioSource>().Play();
            button.GetComponent<AudioSource>().PlayOneShot(button.GetComponent<AudioSource>().clip, 0.5f);
        }
    }
}
