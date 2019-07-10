using System.Collections;
using UnityEngine;

public class FaceColor : MonoBehaviour
{
    [SerializeField] private Color defaultColor;

    private Renderer rend;
    private float elapsedTime = 0f;
    private float duration = 3.5f;
    private Color startColor;
    private Color endColor;

    private void Update()
    {
        rend = this.GetComponent<Renderer>();
        
        if(rend != null)
        {
            elapsedTime += Time.deltaTime / duration;
            if (elapsedTime > 1f) {
                ResetTween();
            } else {
                rend.material.color = Color.Lerp(startColor, endColor, elapsedTime);
            }
        }
    }

    private void ResetTween()
    {
        elapsedTime = 0f;
        
        startColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, rend.material.color.a);
        float alpha = rend.material.color.a <= 0.5f ? 1f : 0f;
        endColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
    }
}
