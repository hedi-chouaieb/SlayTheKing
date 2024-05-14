using UnityEngine;
using System.Linq;

namespace SlayTheKing
{
    public class InitializationManager : MonoBehaviour
    {
        void Start()
        {
            var monoBehaviours = FindObjectsOfType<MonoBehaviour>(true);

            var preInitializeObjects = monoBehaviours
                .OfType<IPrepare>();

            foreach (var obj in preInitializeObjects)
            {
                obj.Prepare();
            }

            var initializeObjects = monoBehaviours
                .OfType<IInitialize>();

            foreach (var obj in initializeObjects)
            {
                obj.Initialize();
            }
        }
    }
}