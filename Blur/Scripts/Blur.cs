using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Blur.Scripts
{
    public class Blur : ScriptableRendererFeature
    {
        [SerializeField] private Settings _settings;
        
        private BlurPass _pass;
        
        public override void Create()
        {
            _pass = new BlurPass(_settings)
            {
                renderPassEvent = _settings.PassEvent
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_pass);
        }
    }
}
