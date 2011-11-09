﻿namespace Nancy.Routing
{
    using ModelBinding;
    using Nancy.ViewEngines;

    /// <summary>
    /// Default implementation for building a full configured <see cref="NancyModule"/> instance.
    /// </summary>
    public class DefaultNancyModuleBuilder : INancyModuleBuilder
    {
        private readonly IViewFactory viewFactory;
        private readonly IResponseFormatterFactory responseFormatterFactory;
        private readonly IModelBinderLocator modelBinderLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultNancyModuleBuilder"/> class.
        /// </summary>
        /// <param name="viewFactory">The <see cref="IViewFactory"/> instance that should be assigned to the module.</param>
        /// <param name="responseFormatterFactory">An <see cref="IResponseFormatterFactory"/> instance that should be used to create a response formatter for the module.</param>
        /// <param name="modelBinderLocator">A <see cref="IModelBinderLocator"/> instance that should be assigned to the module.</param>
        public DefaultNancyModuleBuilder(IViewFactory viewFactory, IResponseFormatterFactory responseFormatterFactory, IModelBinderLocator modelBinderLocator)
        {
            this.viewFactory = viewFactory;
            this.responseFormatterFactory = responseFormatterFactory;
            this.modelBinderLocator = modelBinderLocator;
        }

        /// <summary>
        /// Builds a fully configured <see cref="NancyModule"/> instance, based upon the provided <paramref name="module"/>.
        /// </summary>
        /// <param name="module">The <see cref="NancyModule"/> that shoule be configured.</param>
        /// <param name="context">The current request context.</param>
        /// <returns>A fully configured <see cref="NancyModule"/> instance.</returns>
        public NancyModule BuildModule(NancyModule module, NancyContext context)
        {
            module.Context = context;
            module.Context.Module = module;
            module.Response = this.responseFormatterFactory.Create(context);
            module.ViewFactory = this.viewFactory;
            module.ModelBinderLocator = this.modelBinderLocator;

            return module;
        }
    }
}