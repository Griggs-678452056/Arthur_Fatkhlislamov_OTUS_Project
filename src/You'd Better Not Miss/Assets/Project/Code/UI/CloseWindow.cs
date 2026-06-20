using UnityEngine;

namespace Code
{
    public class CloseWindow : MonoBehaviour
    {
        public void BackButtonClicked()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}