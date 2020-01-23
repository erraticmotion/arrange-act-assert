namespace Motion
{
    using Ninject.Modules;

    /// <summary>
    /// "Arrange-Act-Assert" a pattern for arranging and formatting code in UnitTest methods.
    /// </summary>
    /// <typeparam name="TSut">The Software/System Under Test</typeparam>
    /// <typeparam name="TActivationRoot">The activation root for Ninject injection</typeparam>
    /// <remarks>
    /// Uses Ninject under the hood to create the SUT
    /// </remarks>
    public abstract class ArrangeActAssert<TSut, TActivationRoot> : ArrangeActAssert<TSut> 
        where TSut : class 
        where TActivationRoot : INinjectModule, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeActAssert{TSut, TActivationRoot}"/> class.
        /// </summary>
        protected ArrangeActAssert()
            : base(new TActivationRoot())
        {
        }
    }
}
