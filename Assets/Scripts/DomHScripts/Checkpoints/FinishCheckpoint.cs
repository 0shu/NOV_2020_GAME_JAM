using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCheckpoint : DoubleCheckpoint
{
    public delegate void GameFinished();
    public event GameFinished GameFinishedEvent;


    public override bool ValidCheckpoint
    {
        get => base.ValidCheckpoint;
        protected set
        {
            if (value == true)
                GameFinishedEvent?.Invoke();
        }
    }
}