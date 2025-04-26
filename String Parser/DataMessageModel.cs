namespace String_Parser;

public class DataMessageModel
{
    public string Val1 { get; set; } = string.Empty;
    public string Val2 { get; set; } = string.Empty;
    public string Val3 { get; set; } = string.Empty;
    public string Val4 { get; set; } = string.Empty;
    public string StringProp1 { get; set; } = string.Empty;
    public string StringProp2 { get; set; } = string.Empty;
    public bool BoolVal1 { get; set; } = false;
    public bool BoolVal2 { get; set; } = false;
    public int IntVal1 { get; set; }
    public decimal DecVal
    {
        get { return _decVal; }
        set { _decVal = Decimal.Round(value, 3); }
    }
    private decimal _decVal;
    public bool IsEnabled { get; set; }

    private bool Enabled
    {
        get { return IsEnabled; }
        set { IsEnabled = value; }
    }
    private bool Disabled
    {
        get { return IsEnabled; }
        set { IsEnabled = !value; }
    }
}
