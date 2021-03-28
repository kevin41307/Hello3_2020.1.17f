using UnityEngine;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard instance;


    public Text number1;
    private int currentNumber;
    private bool isBreak;


    private void Awake()
    {
        CheckObject();
        CheckSingle();
    }

    private void Start()
    {
        AIController_Skeleton.OnDeaded += SkeletonDead ;
        Setup(10);
    }


    private void Setup(int defaultNum)
    {
        number1.text = defaultNum.ToString();
        currentNumber = defaultNum;
    }


    public void SkeletonDead()
    {
        MinusNumber(1);
    }

    public void MinusNumber(int delta)
    {
        currentNumber -= delta;
        if (currentNumber < 0)
            isBreak = true;
        currentNumber = Mathf.Clamp(currentNumber, 0, currentNumber);
        number1.text = currentNumber.ToString();
    }

    public void CheckObject()
    {
        if(this.tag == "ScoreBoard")
        {

        }
        else
        {
            Destroy(this);
        }
    }

    public void CheckSingle()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        else
        {
            Destroy(this);
        }
    }
}
