using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class HelloCamera:MonoBehaviour
    {
        public float filterIntensity = 0f;
        private Material filterMaterial;

        private void Awake()
        {
            filterMaterial = new Material(Shader.Find("Hidden/BWDiffuse"));
        }

        // Rendering Post-Process Effect
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (filterIntensity == 0)
            {
                Graphics.Blit(source, destination);
                return;
            }

            filterMaterial.SetFloat("_bwBlend", filterIntensity);
            Graphics.Blit(source, destination, filterMaterial);
        }
    }
}
