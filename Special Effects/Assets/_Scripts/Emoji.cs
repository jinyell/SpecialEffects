using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Emoji : MonoBehaviour
{
    [SerializeField] private Texture[] textures;

    private Texture current;
    private Renderer rend;
    private int index = 0;

    private void Awake()
    {
        rend = this.GetComponent<Renderer>();
    }

    private void SetEmoji(bool up)
    {
        if (up == true)
        {
            index = (index - 1 < 0) ? textures.Length - 1 : --index;
        }
        else
        {
            index = (index + 1 >= textures.Length) ? 0 : ++index;
        }

        current = textures[index];
        rend.material.SetTexture("_MainTex", current);
    }

    private void Start()
    {
        current = textures[index];
        SEInput.Instance.upRegistered += Instance_upRegistered;
        SEInput.Instance.downRegistered += Instance_downRegistered;
    }

    private void OnDestroy()
    {
        SEInput.Instance.upRegistered -= Instance_upRegistered;
        SEInput.Instance.downRegistered -= Instance_downRegistered;
    }

    private void Instance_downRegistered()
    {
        SetEmoji(false);
    }

    private void Instance_upRegistered()
    {
        SetEmoji(true);
    }
}
