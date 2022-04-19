using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Api.Requesting
{
    public interface IRequestService
    {
        MonoBehaviour Behaviour { get; }

        bool CanRequest();
    }
}