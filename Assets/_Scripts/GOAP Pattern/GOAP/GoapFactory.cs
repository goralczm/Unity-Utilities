using UnityEngine;
using Zenject;

public class GoapFactory : MonoInstaller<GoapFactory>
{
    public override void InstallBindings()
    {
        Container.Bind<GoapFactory>().To<GoapFactory>().AsSingle();
    }
    
    public IGoapPlanner CreatePlanner() {
        return new GoapPlanner();
    }
}