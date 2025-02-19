/// <summary>
/// IUpdatable must be implemented by Updatable classes to execute in the UpdatableSystem Update
/// </summary>
public interface IUpdatable 
{
    void ExecuteUpdate();
}

/// <summary>
/// IFixedUpdatable must be implemented by FixedUpdatable classes to execute in the UpdatableSystem FixedUpdate
/// </summary>
public interface IFixedUpdatable
{
    void ExecuteFixedUpdate();
}
