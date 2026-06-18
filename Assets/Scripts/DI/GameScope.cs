using NLB.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace NLB.DI
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // InputManager
            builder.Register<InputManager>(Lifetime.Singleton).As<IInput>();

            // EntryPoint
            builder.RegisterEntryPoint<EntryPoint>();
        }
    }
}