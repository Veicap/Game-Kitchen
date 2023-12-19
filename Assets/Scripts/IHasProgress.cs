using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public interface IHasProgress 
{
    public event EventHandler<OnBarUIChangedEventArgs> OnBarUIChanged;
    public class OnBarUIChangedEventArgs : EventArgs
    {
        public float fillNomarlized;
    }
    
}
