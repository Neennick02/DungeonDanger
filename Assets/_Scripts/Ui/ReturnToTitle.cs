using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("StartScene");
    }    
}
