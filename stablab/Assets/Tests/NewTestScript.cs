using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor; //Important!
using UnityEngine;
using UnityEngine.TestTools;
//
// Created by Viktor Barr
// The master of tests, destroyer of bugs
//
namespace Tests
{
    public class NewTestScript
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Instantiates_Injury_From_Prefab()
        {
            GameObject injuryManager = GameObject.Find("InjuryManager");            
            if (injuryManager != null)
            {
                Debug.Log("Found it boi");
            }
            else
            {
                Debug.Log("No :(");
            }
            // Use yield to skip a frame.
            yield return null;

            var spawnedInjury = GameObject.FindWithTag("Marker");
            //var prefabOfInjury = PrefabUtility.GetPrefabParent(spawnedInjury);
            var prefabOfInjury = PrefabUtility.GetCorrespondingObjectFromSource(spawnedInjury);

            // Use the Assert class to test conditions.
            Assert.AreEqual(prefabOfInjury, spawnedInjury);

            
        }
    }
}
