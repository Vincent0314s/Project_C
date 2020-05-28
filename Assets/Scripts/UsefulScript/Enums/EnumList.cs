using System.Collections.Generic;

[System.Serializable]
public class EnumList {
    public string name;
    public List<string> enumVariable;

    public EnumList(string _name) {
        name = _name;
    }
}
