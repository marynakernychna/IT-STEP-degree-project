﻿using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class TemplateHelper : ITemplateHelper
    {
        private IRazorViewEngine _razorViewEngine;
        private IServiceProvider _serviceProvider;
        private ITempDataProvider _tempDataProvider;

        public TemplateHelper(
            IRazorViewEngine razorViewEngine,
            IServiceProvider serviceProvider,
            ITempDataProvider tempDataProvider
            )
        {
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> GetTemplateHtmlAsStringAsync<T>(string viewName, T model)
        {
            var httpContext = new DefaultHttpContext()
            {
                RequestServices = _serviceProvider
            };

            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor());

            using (StringWriter stringWriter = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(
                    actionContext,
                    viewName,
                    false);

                if (viewResult.View == null)
                {
                    return string.Empty;
                }

                var viewDataDictionary = new ViewDataDictionary(
                    new EmptyModelMetadataProvider(),
                    new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDataDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    stringWriter,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return stringWriter.ToString();
            }
        }
    }
}
