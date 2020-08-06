using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceUtility : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        [SerializeField] private ClipChain[] _clips;

        private void Awake()
        {
            if (_source == null)
            {
                _source = GetComponent<AudioSource>();
            }
        }

        public void PlayOneShot(ClipType type)
        {
            if (_clips != null)
            {
                foreach (var clip in _clips)
                {
                    if (clip.Type == type)
                    {
                        _source.Stop();
                        _source.PlayOneShot(clip.GetRandomClip());
                    }
                }
            }
        }
    }
}
