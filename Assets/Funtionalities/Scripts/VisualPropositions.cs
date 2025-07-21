using AYellowpaper;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

public class VisualPropositions : MonoBehaviour
{
    public static VisualPropositions Instance;

    

    public List<string> NextSymbolList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

    }


}

