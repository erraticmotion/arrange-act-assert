namespace Motion
{
    using NUnit.Framework;

    using Ninject;
    using Ninject.Activation.Caching;
    using Ninject.Syntax;
    using Ninject.Modules;

    /// <summary>
    /// "Arrange-Act-Assert" a pattern for arranging and formatting code in UnitTest methods.
    /// </summary>
    public abstract class ArrangeActAssert
    {
        private StandardKernel kernel;
        private readonly INinjectModule module;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeActAssert"/> class.
        /// </summary>
        protected ArrangeActAssert()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeActAssert"/> class.
        /// </summary>
        /// <param name="module">The module.</param>
        protected ArrangeActAssert(INinjectModule module)
        {
            this.module = module;
        }

        /// <summary>
        /// 
        /// </summary>
        [TestFixtureSetUp]
        public void TestSetUp()
        {
            kernel = module == null 
                ? new StandardKernel() 
                : new StandardKernel(new [] {module});

            Bind(kernel);
        }

        /// <summary>
        /// Steps that are run before each Assert.
        /// </summary>
        [SetUp]
        public void ArrangeAct()
        {
            Arrange();
            Act();
        }

        /// <summary>
        /// Steps that are run after each Assert.
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            Cleanup();
            kernel.Components.Get<ICache>().Clear();
        }

        protected T Get<T>()
        {
            return kernel.Get<T>();
        }

        /// <summary>
        /// Extension point for sub-classes to bind to the IoC kernel
        /// </summary>
        protected virtual void Bind(IBindingRoot root)
        {
        }

        /// <summary>
        /// Arrange all necessary preconditions and inputs. 
        /// </summary>
        protected virtual void Arrange()
        {
        }

        /// <summary>
        /// Act on the object or method under test. 
        /// </summary>
        protected virtual void Act()
        {
        }

        /// <summary>
        /// Cleans up the environment after the test is verified.
        /// </summary>
        protected virtual void Cleanup()
        {
        }
    }
} 
