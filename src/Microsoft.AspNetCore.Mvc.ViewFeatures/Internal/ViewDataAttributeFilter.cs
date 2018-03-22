// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    internal class ViewDataAttributeFilter : IActionFilter, IViewExecutionCallback, IViewComponentExecutionCallback
    {
        private readonly IReadOnlyList<LifecycleProperty> _properties;

        public ViewDataAttributeFilter(IReadOnlyList<LifecycleProperty> properties)
        {
            _properties = properties;
        }

        public object Subject { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Subject = context.Controller;
        }

        public void OnViewComponentExecuting(ViewContext context)
        {
            SaveProperties(context);
        }

        public void OnViewExecuting(ViewContext context)
        {
            SaveProperties(context);
        }

        private void SaveProperties(ViewContext context)
        {
            var viewData = context.ViewData;
            for (var i = 0; i < _properties.Count; i++)
            {
                var property = _properties[i];
                var value = property.GetValue(Subject);

                if (value != null)
                {
                    viewData[property.Key] = value;
                }
            }
        }
    }
}
