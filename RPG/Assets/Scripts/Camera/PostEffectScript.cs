using System.Collections;
using UnityEngine;

public class PostEffectScript : MonoBehaviour
{
    public Material slowMotionMaterial;
    public Material stopTimeMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (AbilitiesManager.slowMotion)
        {
            Graphics.Blit(source, destination, slowMotionMaterial);
        }
        else if (AbilitiesManager.stopTime)
        {
            Graphics.Blit(source, destination, stopTimeMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
