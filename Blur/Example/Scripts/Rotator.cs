using System;
using UnityEngine;

namespace Blur.Example.Scripts
{
    [ExecuteAlways]
    public class Rotator : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(Vector3.right * Time.deltaTime * 25);
        }
    }
}