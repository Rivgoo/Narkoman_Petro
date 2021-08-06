using UnityEngine;
using System.Collections;

namespace Core.Game.Load
{
    public class LoadGame : MonoBehaviour
    {
        [SerializeField]
        private ILoaderGame _loader;

        [SerializeField]
        private GameUI.Game.Load.ProgressShower _shower;

        [SerializeField]
        private DontDestroyOnLoadObject _dontDestroyUnLoad;

        private AsyncOperation _loadInfo;

        private void Start()
        {
            _loadInfo = _loader.Load();

            StartCoroutine(CheckProgress());
            _shower.ShowLoadingGameText(1);
        }

        private IEnumerator CheckProgress()
        {
            while(!_loadInfo.isDone)
            {
                _shower.UpdateProgressBar(_loadInfo.progress);

                yield return new WaitForEndOfFrame();
            }

            _loadInfo.allowSceneActivation = false;

            _shower.FillBarComletely();

            _shower.ChangeElementsColor();
            _shower.ShowPressAnyKeyText();

            StartCoroutine(CheckPressAnyKey());
        }

        private IEnumerator CheckPressAnyKey()
        {
            while(true)
            {
               if(Input.anyKey)
               {
                   _loadInfo.allowSceneActivation = true;

                   yield return new WaitForSeconds(0.3f);

                   _dontDestroyUnLoad.DestroyThis();

                   yield break;
               }

               yield return new WaitForEndOfFrame();
            }
        }
    }
}