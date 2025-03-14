using UnityEngine;

public class SwitchPropsManager : MonoBehaviour
{
    [SerializeField]
    private bool isTutorial = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTutorial)
            {
                // GameManager.Instance.TutorialSwitchProps();
            }
            else
            {
                // GameManager.Instance.SwitchProps();
            }
            Destroy(gameObject);
        }
    }
}
