namespace Motion
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// "Arrange-Act-Assert" a pattern for arranging and formatting code in UnitTest methods.
    /// </summary>
    /// <typeparam name="TSut">The Software/System Under Test</typeparam>
    /// <remarks>
    /// Uses Ninject under the hood to create the SUT
    /// </remarks>
    public abstract class ArrangeActAssert<TSut> where TSut : class
    {
        private readonly List<INinjectModule> modules;
        private readonly CompositionRootOptions options;
        private StandardKernel kernel;

        /// <summary>
        /// The Software Under Test
        /// </summary>
        protected TSut Sut { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeActAssert{TSut}"/> class.
        /// </summary>
        protected ArrangeActAssert()
            : this(CompositionRootOptions.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeActAssert{TSut}"/> class.
        /// </summary>
        /// <param name="module">The module.</param>
        protected ArrangeActAssert(INinjectModule module)
            : this(new[] { module })
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeActAssert{TSut}"/> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        protected ArrangeActAssert(IEnumerable<INinjectModule> modules)
            : this(CompositionRootOptions.Default, modules)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeActAssert{TSut}" /> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        /// <param name="options">The options.</param>
        protected ArrangeActAssert(CompositionRootOptions options, IEnumerable<INinjectModule> modules = null)
        {
            this.options = options;
            if (options == CompositionRootOptions.Default)
            {
                this.modules = modules != null 
                    ? new List<INinjectModule>(modules) 
                    : new List<INinjectModule>(new [] { new CompositionRootBase() });
            }
        }

        /// <summary>
        /// Extension point for sub-classes to amend, delete or add modules specific
        /// to their needs
        /// </summary>
        /// <param name="item">The collection of Ninject modules.</param>
        /// <returns>An array of Ninject modules</returns>
        protected virtual INinjectModule[] GetModules(List<INinjectModule> item)
        {
            return item.ToArray();
        }

        /// <summary>
        /// Extension point for sub-classes to override the default
        /// <see cref="StandardKernel"/> object.
        /// </summary>
        /// <returns>An instance of the Ninject Kernel</returns>
        protected virtual StandardKernel GetKernel()
        {
            return options == CompositionRootOptions.Default
                ? new StandardKernel(Settings(), GetModules(modules))
                : new StandardKernel();
        }

        /// <summary>
        /// Steps that are run before each Assert.
        /// </summary>
        [SetUp]
        public void ArrangeAct()
        {
            kernel = GetKernel();
            Sut = Arrange(MoqKernel.For(kernel));
            Act(Sut);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Call Arrange before Get{T}</exception>
        protected T Get<T>()
        {
            if (kernel == null)
            {
                throw new InvalidOperationException("Call Arrange before Get{T}");
            }

            return kernel.Get<T>();
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        protected IKernel Kernel { get { return kernel; } }

        /// <summary>
        /// Steps that are run after each Assert.
        /// </summary>
        [TestFixtureTearDown]
        public void TestCleanup()
        {
            Cleanup(Sut);
            if (Sut == null) return;
            try
            {
                var d = Sut as IDisposable;
                if (d != null)
                {
                    d.Dispose();
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch { }
// ReSharper restore EmptyGeneralCatchClause
        }

        /// <summary>
        /// Arrange all necessary preconditions and inputs. 
        /// </summary>
        protected virtual TSut Arrange(MoqKernel moqKernel)
        {
            return kernel.Get<TSut>();
        }

        /// <summary>
        /// Act on the software/system under test. 
        /// </summary>
        /// <remarks>
        /// Unit test asserts can access the <param ref="sut" /> using the
        /// protected <see cref="ArrangeActAssert{TSut}.Sut" /> property.
        /// </remarks>
        protected virtual void Act(TSut sut)
        {
        }

        /// <summary>
        /// Cleans up the environment after the test is verified.
        /// </summary>
        protected virtual void Cleanup(TSut sut)
        {
        }

        /// <summary>
        /// Extension point for sub-classes to add <see cref="NinjectSettings" /> 
        /// to this instance
        /// </summary>
        /// <returns>Settings that control the underlying IoC container</returns>
        protected virtual NinjectSettings Settings()
        {
            return new NinjectSettings
            {
                LoadExtensions = true,
            }; 
        }
    }
}
