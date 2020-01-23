namespace Motion
{
    using System;

    using Moq;

    using Ninject.Activation;

    /// <summary>
    /// An <see cref="IProvider{T}" /> implementation that creates and adds <see cref="MockProvider{T}" />
    /// mocked objects into the IoC container.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MockProvider<T> : IProvider<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockProvider{T}"/> class.
        /// </summary>
        public MockProvider()
            : this(x => { })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockProvider{T}"/> class.
        /// </summary>
        /// <param name="behaviour">The behaviour.</param>
        public MockProvider(Action<Mock<T>> behaviour)
        {
            Mock = new Mock<T>();
            behaviour(Mock);
        }

        /// <summary>
        /// Gets the mock object created by this instance.
        /// </summary>
        /// <value>
        /// The mock object set with any behaviours specified.
        /// </value>
        public Mock<T> Mock { get; private set; }

        #region Implementation of IProvider

        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public object Create(IContext context)
        {
            return Mock.Object;
        }

        /// <summary>
        /// Gets the type (or prototype) of instances the provider creates.
        /// </summary>
        public Type Type
        {
            get { return typeof(T); }
        }

        #endregion
    }
}
