using UnityEngine;

public class ScorePageButton : MonoBehaviour
{
    public void pushed()
    {
        GameManager.Instance.ReturnToTitle();
    }
}
