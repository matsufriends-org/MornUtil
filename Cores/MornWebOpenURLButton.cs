using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MornUtil
{
    [RequireComponent(typeof(Button))]
    public sealed class MornWebOpenURLButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _url;

        private void Awake()
        {
            _button.OnClickAsObservable().Subscribe(_ => MornWebUtil.Open(_url)).AddTo(this);
        }

        private void Reset()
        {
            _button = GetComponent<Button>();
        }
    }
}