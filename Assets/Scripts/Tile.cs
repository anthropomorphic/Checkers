using UnityEngine;

public class Tile : MonoBehaviour
{
    private int _rank;
    private int _file;
    public int RankInt => _rank;
    public int FileInt => _file;
    public char RankChar => (char) ((int) '1' + _rank);
    public char FileChar => (char) ((int) 'A' + _file);

    private void Start()
    {
        _rank = (int) name[0] - '1';
        _file = (int) name[1] - 'A';
    }
}
