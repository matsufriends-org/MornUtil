using TMPro;
using UnityEngine;

namespace MornUtil
{
    public sealed class MornVerText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private void Awake()
        {
            _text.text = $"{Application.version}";
        }

        private void Reset()
        {
            _text = GetComponent<TMP_Text>();
        }
    }
}