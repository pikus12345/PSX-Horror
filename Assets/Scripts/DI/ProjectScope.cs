using NLB.Core.Audio;
using NLB.Core.Scene;
using NLB.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace NLB.DI
{
    public class ProjectScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // AudioService
            builder.Register<AudioService>(Lifetime.Singleton).As<IAudioService>();

            // SceneLoader
            builder.Register<SceneLoader>(Lifetime.Singleton).As<ISceneLoader>();
        }
    }
}