using System.Collections.Generic;
using UnityEngine;

public class FishBehaviourStateMachine 
{
    private BaseFishBehaviour _behaviour;
    private BaseState _initialState;

    private BaseState _currentState;
    private BaseState _unfinishedState;

    private Queue<BaseState> _repetableStatesQueue;
    private Queue<BaseState> _oneTimeFiringStatesQueue;
    public BaseFishBehaviour Behaviour => _behaviour;

    public FishBehaviourStateMachine(BaseFishBehaviour behaviour, BaseState initialState)
    {
        _behaviour = behaviour;
        _repetableStatesQueue = new Queue<BaseState>();
        _oneTimeFiringStatesQueue = new Queue<BaseState>();
        _initialState = initialState;
    }

    public void Start()
    {
        _currentState = _initialState;
        _currentState.Start(this);
    }

    public void Update()
    {
        if(_currentState == null)
        {
            return;
        }

        if(_currentState.Finished)
        {
            NextState();
        }

        _currentState.Update();
    }

    public void Stop()
    {
        _currentState?.Stop();
        _currentState = null;
    }

    private void NextState()
    {
        _currentState?.Stop();

        if (_unfinishedState != null)
        {
            _currentState = _unfinishedState;
            _unfinishedState = null;
        }
        else if (_oneTimeFiringStatesQueue.TryDequeue(out BaseState oneTimeState))
        {
            _currentState = oneTimeState;
        }
        else
        {
            if(!_repetableStatesQueue.TryDequeue(out BaseState repetableState))
            {
                Debug.Log("No states in repetable Queue. Stopping");
                _currentState = null;
                return;
            }

            _currentState = repetableState;
            _repetableStatesQueue.Enqueue(repetableState);
        }

        _currentState.Start(this);
    }

    public void StartStateImmidiate(BaseState state)
    {
        if(_currentState != null)
        {
            _currentState.Stop();
        }

        // Needed to return to previous state next
        _unfinishedState = _currentState;

        _currentState = state;
        _currentState.Start(this);
    }

    public void StopCurrentState()
    {
        if(_currentState == null)
        {
            Debug.LogWarning("No states in progress");
            return;
        }

        _currentState.Stop();
        _currentState = null;
    }

    public void EnqueueRepetableState(BaseState state)
    {
        _repetableStatesQueue.Enqueue(state);
    }

    public void DequeueRepetableState(BaseState state)
    {
        if(!_repetableStatesQueue.Contains(state))
        {
            Debug.LogWarning($"{state} can not be found");
            return;
        }

        BaseState[] states = _repetableStatesQueue.ToArray();
        _repetableStatesQueue.Clear();

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i] == state) 
            {
                continue;
            }

            _repetableStatesQueue.Enqueue(states[i]);
        }
    }

    public void EnqueueOneTimeState(BaseState state)
    {
        _oneTimeFiringStatesQueue.Enqueue(state);
    }

    public void EnqueueOneTimeStates(BaseState[] states)
    {
        for (int i = 0; i < states.Length; i++)
        {
            _oneTimeFiringStatesQueue.Enqueue(states[i]);
        }
    }
}
