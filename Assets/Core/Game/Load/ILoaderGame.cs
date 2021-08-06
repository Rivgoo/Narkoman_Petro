using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Game.Load
{
    public abstract class ILoaderGame : MonoBehaviour
    {
        public abstract AsyncOperation Load();
    }
}
