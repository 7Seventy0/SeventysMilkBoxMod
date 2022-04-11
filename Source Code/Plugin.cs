using BepInEx;
using System;
using System.IO;
using UnityEngine;
using Utilla;

namespace SeventysMilkBoxMod
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "gorillamilkbundle"));
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }
            var milk = myLoadedAssetBundle.LoadAsset<GameObject>("gorillaMilk");

            GameObject lefthand = GameObject.Find("palm.01.L");


            Instantiate(milk);
            GameObject milkInstance = GameObject.Find("gorillaMilk(Clone)");
            milkInstance.transform.SetParent(lefthand.transform, false);
            milkInstance.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
            milkInstance.transform.localPosition = new Vector3(0f, - 0.1f, 0.1f);
            milkInstance.transform.localEulerAngles = new Vector3(7.3853f, 58.365f, 356.7716f);
        }
        void Update()
        {
            /* Code here runs every frame when the mod is enabled */
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }
    }
}
