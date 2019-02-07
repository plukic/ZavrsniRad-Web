using System;
using System.Globalization;
using System.Web.Mvc;

namespace COAssistance.WEB.Infrastructure
{
    public class DoubleModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            if (valueResult.AttemptedValue != string.Empty)
            {
                try
                {
                    actualValue = Convert.ToDouble(valueResult.AttemptedValue, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}