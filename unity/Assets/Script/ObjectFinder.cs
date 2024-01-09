using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinder : MonoBehaviour
{
    private static ObjectFinder instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static GameObject FindPianoPrefab()
    {
        for (int i = 0; i < 13; i++)
        {
            GameObject piano = GameObject.Find($"pianoPrefab{i}");
            if (piano != null)
            {
                return piano;
            }
        }
        return null;
    }
    
    public static GameObject FindGuitarePrefab()
    {
        for (int i = 0; i < 13; i++)
        {
            GameObject guitare = GameObject.Find($"guitarePrefab{i}");
            if (guitare != null)
            {
                return guitare;
            }
        }
        return null;
    }
    
    public static GameObject FindBatteryPrefab()
    {
        for (int i = 0; i < 13; i++)
        {
            GameObject battery = GameObject.Find($"batteryPrefab{i}");
            if (battery != null)
            {
                return battery;
            }
        }
        return null;
    }
    
    public static GameObject FindViolonPrefab()
    {
        for (int i = 0; i < 13; i++)
        {
            GameObject violon = GameObject.Find($"violonPrefab{i}");
            if (violon != null)
            {
                return violon;
            }
        }
        return null;
    }
}
