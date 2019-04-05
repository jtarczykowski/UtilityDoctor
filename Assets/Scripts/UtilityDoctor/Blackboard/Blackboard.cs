using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor
{
    public class Blackboard
    {
        public int Id { get; private set; }

        private Dictionary<string, int> integerValues;
        private Dictionary<string, float> floatValues;
        private Dictionary<string, string> stringValues;
        private Dictionary<string, Vector3> vectorValues;
        private Dictionary<string, object> objectValues;
        private Dictionary<string, GameObject> gameObjectValues;

        public Blackboard()
        {
            integerValues = new Dictionary<string, int>();
            floatValues = new Dictionary<string, float>();
            stringValues = new Dictionary<string, string>();
            vectorValues = new Dictionary<string, Vector3>();
            objectValues = new Dictionary<string, object>();
            gameObjectValues = new Dictionary<string, GameObject>();
        }

        public void Initialize(int id) => Id = id;

        public int GetIntegerValue(string key, int defaultValue = default)
        {
            return integerValues.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public float GetFloatValue(string key, float defaultValue = default)
        {
            return floatValues.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public string GetStringValue(string key, string defaultValue = default)
        {
            return stringValues.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public Vector3 GetVector3Value(string key, Vector3 defaultValue = default)
        {
            return vectorValues.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public object GetObjectValue(string key, object defaultValue = default)
        {
            return objectValues.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public GameObject GetGameObjectValue(string key, GameObject defaultValue = default)
        {
            return gameObjectValues.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public void SetIntegerValue(string key, int value)
        {
            integerValues[key] = value;
        }

        public void SetFloatValue(string key, float value)
        {
            floatValues[key] = value;
        }

        public void SetStringValue(string key, string value)
        {
            stringValues[key] = value;
        }

        public void SetVector3Value(string key, Vector3 value)
        {
            vectorValues[key] = value;
        }

        public void SetObjectValue(string key, object value)
        {
            objectValues[key] = value;
        }

        public void SetGameObjectValue(string key, GameObject value)
        {
            gameObjectValues[key] = value;
        }
    }
}
