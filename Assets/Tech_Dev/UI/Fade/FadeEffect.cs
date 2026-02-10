using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tech_Dev.UI
{
	public class FadeEffect : MonoBehaviour
	{
		private Animator _animator;
		
		private static readonly int FadeIn = Animator.StringToHash("FadeIn");
		private static readonly int FadeOut = Animator.StringToHash("FadeOut");
		private Image _fade;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		public void PlayFadeIn()
		{
			_animator.SetTrigger(FadeIn);
		}

		public void PlayFadeOut()
		{
			_animator.SetTrigger(FadeOut);
		}
	}
}
