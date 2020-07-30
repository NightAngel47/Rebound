/* Primary Author: Alex Cline
 */

using UnityEngine;
using UnityEngine.UI;

public class SelectFirstButton : MonoBehaviour
{
    [SerializeField] private Button buttonToSelect = null;

    void Start()
    {
        buttonToSelect.Select();
    }
}