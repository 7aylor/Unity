using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class OnUnityLoad
{
    static OnUnityLoad()
    {
        EditorApplication.playModeStateChanged += (x) =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                //If the current scene has been modified and is still not saved
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    //Console debug
                    Debug.Log("Auto-Saved opened scenes before entering Play mode");
                    //Sound
                    //EditorApplication.Beep();
                    //Save assets
                    AssetDatabase.SaveAssets();
                    //Save scenes, but ask the user before
                    //EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                    //IF YOU DON'T WANT TO BE ASKED FOR CONFIRMATION, USE THIS INSTEAD :
                    EditorSceneManager.SaveOpenScenes();
                }
            }
        };
    }
}