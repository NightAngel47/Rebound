/* Primary Author: Trent Lewis
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Words
{
    public string name;

    [TextArea(3, 7)]
    public string[] sentences;
}
