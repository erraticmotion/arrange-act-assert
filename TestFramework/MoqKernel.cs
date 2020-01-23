namespace Motion
{
    using System;
    using System.Collections.Generic;

    using Moq;

    using Ninject;
    using Ninject.Syntax;

    /// <summary>
    /// Wraps the <see cref="StandardKernel"/> object so that <see cref="ArrangeActAssert{TSut}"/>
    /// type can inject instances of the <see cref="MockProvider{T}"/> type and return the
    /// mock implementation with one line.
    /// </summary>
    public class MoqKernel
    {
        /// <summary>
        /// GoF builder for creating instances of this type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static MoqKernel For(StandardKernel item)
        {
            return new MoqKernel(item);
        }

        private readonly StandardKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoqKernel"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private MoqKernel(StandardKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Binds this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }

        /// <summary>
        /// Binds the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="Mock{T}"/> created from the <typeparamref name="{T}"/></returns>
        public T Bind<T>(T item) 
        {
            kernel.Bind<T>().ToMethod(x => item);
            return item;
        }

        /// <summary>
        /// Binds the specified provider to this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider">The provider.</param>
        /// <returns>A <see cref="Mock{T}"/> created from the <paramref name="provider"/></returns>
        public Mock<T> BindMock<T>(MockProvider<T> provider) where T : class
        {
            kernel.Bind<T>().ToProvider(provider);
            return provider.Mock;
        }

        /// <summary>
        /// Creates and adds a <typeparamref name="{T}"/> object and returns a <see cref="Mock{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A <see cref="Mock{T}"/> created from the <typeparamref name="{T}"/></returns>
        /// <remarks>
        /// Test double - Dummy
        /// </remarks>
        public Mock<T> BindMock<T>() where T : class
        {
            return BindMock(new MockProvider<T>());
        }

        
        /// <summary>
        /// Adds the specified behaviour.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="behaviour">The behaviour that the mock object exhibits.</param>
        /// <returns>A <see cref="Mock{T}"/> created from the <typeparamref name="{T}"/></returns>
        public Mock<T> BindMock<T>(Action<Mock<T>> behaviour) where T : class
        {
            return BindMock(new MockProvider<T>(behaviour));
        }

        /// <summary>
        /// Gets an instance of the specified service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class
        {
            return kernel.Get<T>();
        }

        /// <summary>
        /// Gets a collection of the specified service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">The number of specified services.</param>
        /// <returns>A collection of specified services</returns>
        public IList<T> Get<T>(int count) where T : class
        {
            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                result.Add(Get<T>());
            }
            return result;
        }
    }
}
