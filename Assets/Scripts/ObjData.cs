using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjData : ScriptableObject
{
    [System.Serializable]
    public class Attribute
    {
        public string productName;
        public string productCategory;
        public string productMaterial;
        public string Floor;
        public string objectSize;
        public int workFlow;
        public string startDate;
        public string endDate;
        public string currentStatus;
    }

    public List<Attribute> list = new List<Attribute>();
}

[System.Serializable]
public class ObjDataForJson
{
    [System.Serializable]
    public class Attribute
    {
        public string productName;
        public string productCategory;
        public string productMaterial;
        public string Floor;
        public string objectSize;
        public int workFlow;
        public string startDate;
        public string endDate;
        public string currentStatus;
    }

    public List<Attribute> list = new List<Attribute>();
}