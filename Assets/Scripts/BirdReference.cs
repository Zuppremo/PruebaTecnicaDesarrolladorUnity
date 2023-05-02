using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdReference : MonoBehaviour
{
    [Serializable]
    public struct BirdsReference
    {
        public Birds Type;
        public Bird Prefab;
    }

    public class BirdSpawner : MonoBehaviour
    {
        [SerializeField] private List<BirdsReference> references = new List<BirdsReference>();
    }

}
