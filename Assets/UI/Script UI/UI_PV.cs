using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class UI_PV : MonoBehaviour
{
//  [SerializeField]
  [SerializeField] private RectTransform _barRect;
  
  [SerializeField] private RectMask2D _mask;

  private float _maxRightMask;
  private float _InitialRightMask;
  
  
  
  
  void start()
  {
     _maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z;
     _InitialRightMask = _mask.padding.z;
     
  }

  public void SetValue(int newValue)
  {
 //     var targetWidth = newValue * _maxRightMask / _health.MaxHp;
   //   var newRightMask = _maxRightMask + _InitialRightMask - targetWidth;
     // var padding = _mask.padding;
      //padding.z = newRightMask;
      //_mask.padding = padding;
      
  }
}
