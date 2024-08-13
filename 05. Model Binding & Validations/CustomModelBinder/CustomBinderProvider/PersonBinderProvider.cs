using CustomModelBinder.CustomModelBinders;
using CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace CustomModelBinder.CustomBinderProvider
{
    /*
     * Set the CustomModelBinder(PersonModelBinder) to all the controllers which
     * are using PersonModel param in their action method dynamically using
     * IModelBinderProvider 
     * Step 1: Create/define CustomModelBinder i.e PersonModelBinder
     * Step 2: Create/define CustomBinderProvider i.e PersonBinderProvider
     * Step 3: Add PersonBinderProvider class as reusable service in program.cs i.e 
     */
    public class PersonBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(PersonModel))
            {
                return new BinderTypeModelBinder(typeof(PersonModelBinder));
            }
            throw new NotImplementedException();
        }
    }
}
