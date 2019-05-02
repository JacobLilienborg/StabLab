using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModelView{
    AllVisible,
    ActiveVisible,
    NonVisible
};

public class Settings : MonoBehaviour
{
    ModelView modelView = ModelView.ActiveVisible;

}
