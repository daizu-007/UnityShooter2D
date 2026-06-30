using UnityEngine;

public class StartPageButton : MonoBehaviour
{
    public void pushed()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }
}
