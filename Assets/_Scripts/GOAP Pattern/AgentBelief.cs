using System;
using System.Collections.Generic;
using UnityEngine;

public class BeliefFactory
{
    readonly private GoapAgent _agent;
    readonly private Dictionary<string, AgentBelief> _belief;

    public BeliefFactory(GoapAgent agent, Dictionary<string, AgentBelief> belief)
    {
        _agent = agent;
        _belief = belief;
    }

    // TODO Add Sensor Belief

    public void AddLocationBelief(string key, float distance, Transform locationCondition)
    {
        AddLocationBelief(key, distance, locationCondition.position);
    }

    public void AddLocationBelief(string key, float distance, Vector3 locationCondition)
    {
        _belief.Add(key, new AgentBelief.Builder(key)
            .WithCondition(() => InRangeOf(locationCondition, distance))
            .WithLocation(() => locationCondition)
            .Build());
    }

    public void AddBelief(string key, Func<bool> condition)
    {
        _belief.Add(key, new AgentBelief.Builder(key)
            .WithCondition(condition)
            .Build());
    }

    bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(_agent.transform.position, pos) < range;
}

public class AgentBelief
{
    public string Name { get; }

    private Func<bool> _condition = () => false;
    private Func<Vector3> _observedLocation = () => Vector3.zero;

    public Vector3 Location => _observedLocation();
    public bool Evaluate() => _condition();

    AgentBelief(string name)
    {
        Name = name;
    }

    public class Builder
    {
        readonly private AgentBelief _belief;

        public Builder(string name)
        {
            _belief = new AgentBelief(name);
        }

        public Builder WithCondition(Func<bool> condition)
        {
            _belief._condition = condition;
            return this;
        }

        public Builder WithLocation(Func<Vector3> observedLocation)
        {
            _belief._observedLocation = observedLocation;
            return this;
        }

        public AgentBelief Build()
        {
            return _belief;
        }
    }
}
