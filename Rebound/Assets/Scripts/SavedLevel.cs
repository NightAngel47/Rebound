/* Primary Author: Trent Lewis
 */

[System.Serializable]
public class SavedLevel
{
    public int levelSaved = 0;

    public SavedLevel(GameManager gm)
    {
        levelSaved = gm.nextLevel;
    }
}