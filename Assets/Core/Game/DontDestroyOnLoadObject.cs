using UnityEngine;

namespace Core.Game
{
    public class DontDestroyOnLoadObject : MonoBehaviour
    {
        public void DestroyThis()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
