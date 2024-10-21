using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UniActivation
{
    [ExecuteInEditMode()]
    [DefaultExecutionOrder(-32000)]
    [AddComponentMenu("UniActivation/NamedActivationsManager")]
    public class NamedActivationsManager : NamedActivationsRegister
    {
        static NamedActivationsManager instance;

        public static NamedActivationsManager Instance
        {
           get
            {
                return instance;
            }
        }

        void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {

                Debug.LogWarning("A singleton can only be instantiated once!");
                Destroy(gameObject);
                return;
            }
        }

        void OnDestroy()
        {
            if(instance == this)
            {
                instance = null;
            }
        }

#if UNITY_EDITOR
        void LateUpdate()
        {
            if(Application.isPlaying == false)
            {
                instance = this;
            }
        }
#endif
    }
}