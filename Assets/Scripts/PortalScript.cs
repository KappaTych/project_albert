using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SaveManager.Instance?.Save(nextSceneName);
            Destroy(other.gameObject);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
