public abstract class BasePole
{
    protected PoleData _data;
    protected BasePoleView _view;
    public PoleData Data => _data;
    public BasePoleView View => _view;

    protected BasePole(PoleData data, BasePoleView view)
    {
        _data = data;
        _view = view;
    }
}

public class PoleData
{
    public float Strength { get; private set; }

    public PoleData(float strength)
    {
        Strength = strength;
    }

    public PoleData(PoleConfig config)
    {
        Strength = config.strength;
    }
}
