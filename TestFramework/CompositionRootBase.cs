namespace Motion
{
    using System.Threading;

    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// Contains bindings for common objects used within this solution.
    /// </summary>
    /// <remarks>
    /// Expecting sub-classes to implement an activation root for each test assembly.
    /// </remarks>
    public class CompositionRootBase : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<CancellationTokenSource>().ToSelf().InSingletonScope();
            Bind<CancellationToken>().ToMethod(ctx => ctx.Kernel.Get<CancellationTokenSource>().Token);
        }

        #endregion
    }
}
