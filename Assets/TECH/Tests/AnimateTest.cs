using UnityEngine;

namespace TECH.Tests
{
    public class AnimateTest : MonoBehaviour
    {
        private Animation _anim;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _anim = GetComponent<Animation>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                _anim.Play();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (Mathf.Approximately(Time.timeScale, 1))
                {
                    print("reduced timescale");
                    Time.timeScale = 0.33f;
                }
                else
                {
                    print("timescale = 1");
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
