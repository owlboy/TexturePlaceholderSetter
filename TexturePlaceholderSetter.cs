using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TexturePlaceholderSetter : MonoBehaviour
{
    public Material targetMaterial;
    public Texture mainTexture;

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            SetMainTextures();
        }
    }
#endif

    private static void SetMainTextures()
    {
        foreach (TexturePlaceholderSetter textureSetter in FindObjectsOfType<TexturePlaceholderSetter>())
        {
            SetMainTexture(textureSetter);
        }
    }

    private static void SetMainTexture(TexturePlaceholderSetter textureSetter)
    {
        if (textureSetter.targetMaterial && textureSetter.mainTexture)
        {
            textureSetter.targetMaterial.SetTexture("_MainTex", textureSetter.mainTexture);
#if UNITY_EDITOR
            EditorUtility.SetDirty(textureSetter.targetMaterial);
#endif
        }
        else
        {
            Debug.LogWarning("Target Material or Main Texture is not assigned.");
        }
    }
}
