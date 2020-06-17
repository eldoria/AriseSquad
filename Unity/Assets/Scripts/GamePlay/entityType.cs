using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityType : MonoBehaviour
{
    [SerializeField] private string type;

    public new string GetType()
    {
        return type;
    }
}
