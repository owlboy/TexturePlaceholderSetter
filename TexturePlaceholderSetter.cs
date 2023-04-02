using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TexturePlaceholderSetter : MonoBehaviour
{
    public Material targetMaterial;
    public Texture mainTexture;
}

[CustomEditor(typeof(TexturePlaceholderSetter))]
public class TextureSetterEditor : Editor
{
    private TexturePlaceholderSetter textureSetter;

    private void OnEnable()
    {
        textureSetter = (TexturePlaceholderSetter)target;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            SetMainTexture();
        }
    }

    private void SetMainTexture()
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
