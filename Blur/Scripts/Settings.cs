using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Blur.Scripts
{
    [Serializable]
    public class Settings
    {
        [SerializeField] private float _power;
        [SerializeField, Range(0.005f, 1f)] private float _downsample;
        [SerializeField] private int _samplesCount;
        [SerializeField] private Material _material;
        [SerializeField] private RenderPassEvent _passEvent;

        public float Power => _power;
        public float Downsample => _downsample;
        public int SamplesCount => _samplesCount;
        public Material Material => _material;
        public RenderPassEvent PassEvent => _passEvent;
    }
}