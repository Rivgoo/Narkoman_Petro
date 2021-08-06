using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Game.Load
{
    public class LoaderGame : ILoaderGame
    {
        [SerializeField]
        private string _sceneLoad;

        public override AsyncOperation Load()
        {
            return SceneManager.LoadSceneAsync(_sceneLoad);
        }
    }
}
