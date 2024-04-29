using System;
using UnityEngine;
using UnityEngine.UI;


public class MeatCollector : MonoBehaviour
{
    public event Action OnCollectMaxMeatnumb;

    [SerializeField] int meatNumbToUlt;
    [SerializeField] Image meatImage;


    private int meatCount;


    private void Start()
    {
        meatImage.fillAmount = 0;

    }
    public void CollectMeat(int count)
    {
        meatCount = meatCount + count;
        meatImage.fillAmount = meatCount / (float)meatNumbToUlt;
        if (meatCount >= meatNumbToUlt)
        {
            OnCollectMaxMeatnumb?.Invoke();
        }
    }

    public void SpendMeat()
    {
        meatCount = meatCount - meatNumbToUlt;
    }
}
