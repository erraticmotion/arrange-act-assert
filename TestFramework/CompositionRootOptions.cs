namespace Motion
{
    /// <summary>
    /// Options for the composition root modules.
    /// </summary>
    public enum CompositionRootOptions
    {
        /// <summary>
        /// Indicates that no composition root should be added to the 
        /// IoC container.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the default composition root should be
        /// added to the IoC container, or if supplied, the one supplied 
        /// in the <see cref="ArrangeActAssert{TSut}"/> constructor.
        /// </summary>
        Default,
    }
}
