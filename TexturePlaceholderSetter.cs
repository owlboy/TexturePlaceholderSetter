using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TexturePlaceholderSetter : MonoBehaviour
{
    public Material targetMaterial;
    public Texture mainTexture;
}

[InitializeOnLoad]
public class TextureSetterEditor
{
    static TextureSetterEditor()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            EditorApplication.delayCall += SetMainTextures;
        }
    }

    private static void SetMainTextures()
    {
        TexturePlaceholderSetter[] textureSetters = GameObject.FindObjectsOfType<TexturePlaceholderSetter>();
        foreach (TexturePlaceholderSetter textureSetter in textureSetters)
        {
            SetMainTexture(textureSetter);
        }
    }

    private static void SetMainTexture(TexturePlaceholderSetter textureSetter)
    {
        if (textureSetter.targetMaterial == null || textureSetter.mainTexture == null)
        {
            Debug.LogWarning("Target Material or Main Texture is not assigned.");
            return;
        }

        textureSetter.targetMaterial.SetTexture("_MainTex", textureSetter.mainTexture);
        EditorUtility.SetDirty(textureSetter.targetMaterial);
    }
}
