using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Blur.Scripts
{
    public class BlurPass : ScriptableRenderPass
    {
        private float _power;
        private float _downsamplePower;
        private int _samples;
        private int _offsetId = Shader.PropertyToID("_Offset");
        private Material _material;
        private Vector2Int _downsample;
        private RenderTargetHandle _blurHandle;
        private RenderTargetHandle _tempHandle;
        private List<RenderTargetHandle> _handles;
        private Vector2 _rect;
        
        public BlurPass(Settings settings)
        {
            _power = settings.Power;
            _material = settings.Material;
            _samples = settings.SamplesCount;
            _downsamplePower = settings.Downsample;
            _blurHandle.Init("_BlurTexture");
            _tempHandle.Init("_TempTexture");

            _handles = new List<RenderTargetHandle>();
            
            for (int i = 0; i < _samples; i++)
            {
                RenderTargetHandle handle = new RenderTargetHandle();
                handle.Init($"_Handle{i}");
                _handles.Add(handle);
            }
        }

        public override void Configure(CommandBuffer buffer, RenderTextureDescriptor cameraTextureDescriptor)
        {
            _rect =  new Vector2(cameraTextureDescriptor.height, cameraTextureDescriptor.width);
        }
        
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        { 
            var descriptor = renderingData.cameraData.cameraTargetDescriptor;
            _downsample =  new Vector2Int((int)(descriptor.width / .7f), (int)(descriptor.height / .7f));

            for (int i = 1; i < _samples; i++)
            {
                Vector2Int current = new Vector2Int(
                    Mathf.Clamp((int)(_downsample.x * _downsamplePower) / i, 1, _downsample.x),
                    Mathf.Clamp((int)(_downsample.y * _downsamplePower) / i, 1, _downsample.y));
                cmd.GetTemporaryRT(_handles[i].id, current.x, current.y, 0, FilterMode.Bilinear);
            }
            
            cmd.GetTemporaryRT(_tempHandle.id, descriptor, FilterMode.Bilinear);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("Blur");
            if(_handles.Count == 0 )
                return;
            
            var colorTarget = renderingData.cameraData.renderer.cameraColorTarget;
            
            for (int i = 1; i < _samples; i++)
            {
                cmd.Blit( colorTarget, _handles[i].id);
            }

            for (int i = 0; i < _samples - 1; i++)
            {
                _material.SetVector(_offsetId, Vector4.one / _rect * _power);
                cmd.Blit(_handles[i].id, _handles[i + 1].id, _material, 1);
            }

            cmd.Blit( _handles[_handles.Count - 1].id, colorTarget);

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            base.OnCameraCleanup(cmd);
            cmd.ReleaseTemporaryRT(_blurHandle.id);
            cmd.ReleaseTemporaryRT(_tempHandle.id);
        }
    }
}