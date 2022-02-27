#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections.Generic;

namespace pixelplan {
    public partial class Util {
        private Dictionary <string, object> m_ConfigDataDictionary =  new Dictionary<string, object>();
        public T GetConfig<T>(string path, string key){
            object _data;
            if (!m_ConfigDataDictionary.TryGetValue(key, out _data)) {
                _data = this.ReadFromStreamAssets<T>(path);
                m_ConfigDataDictionary.Add(key, _data);
            }
            return (T)_data;
        }
    }    
}
