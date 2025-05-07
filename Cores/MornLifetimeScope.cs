using VContainer;
using VContainer.Unity;

namespace MornUtil
{
    public class MornLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(resolver =>
            {
                foreach (var rootGameObject in gameObject.scene.GetRootGameObjects())
                {
                    resolver.InjectGameObject(rootGameObject);
                }
            });
        }
    }
}