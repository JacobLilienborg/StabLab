using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair<T,I> : MonoBehaviour
{ 
    public T first { get; set; }
    public I second { get; set; }

    public Pair(T first, I second)
    {
        this.first = first;
        this.second = second;
    }
}
