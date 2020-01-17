using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Transition").GetComponent<Transition>().GoToNextLevel();
        }
    }
}