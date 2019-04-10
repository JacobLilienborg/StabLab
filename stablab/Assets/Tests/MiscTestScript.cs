using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor; //Important!
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
//
// Created by Viktor Barr
// The master of tests, destroyer of bugs
//
namespace Tests
{
    public class MiscTestScript
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator AllManagersAreActive()
        {
            //Scene testScene = SceneManager.GetActiveScene();
            
            yield return SceneManager.LoadSceneAsync("InjuryMode", LoadSceneMode.Single);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("InjuryMode"));
            
            GameObject InjuryManager = GameObject.Find("InjuryManager");
            Assert.IsTrue(InjuryManager.activeInHierarchy);
            GameObject DataManager = GameObject.Find("DataManager");
            Assert.IsTrue(InjuryManager.activeInHierarchy);
            GameObject ViewManager = GameObject.Find("ViewManager");
            Assert.IsTrue(InjuryManager.activeInHierarchy);
            GameObject ProjectManager = GameObject.Find("ProjectManager");
            Assert.IsTrue(InjuryManager.activeInHierarchy);
        }

        /*[UnityTest]
        public IEnumerator Active()
        {
            //Scene testScene = SceneManager.GetActiveScene();
           // var injuryManager = new InjuryManagerLogic();
            yield return null;
        }*/
    }
}
