using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handding : MonoBehaviour
{
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform Lobject;
    [SerializeField] private Transform Robject;

    void Start()
    {
        Lobject.position = Left.position;
        Robject.position = Right.position;
    }


}
