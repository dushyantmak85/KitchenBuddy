using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Mono.Cecil;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }


}
