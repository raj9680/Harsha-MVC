using CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomModelBinder.CustomModelBinders
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            PersonModel person = new PersonModel();
            if(bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
            {
                person.PersonName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;

                if(bindingContext.ValueProvider.GetValue("LastName").Length> 0)
                {
                    person.PersonName += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
                }  
            }

            // Email
            if(bindingContext.ValueProvider.GetValue("Email").Length > 0)
            {
                person.Email += bindingContext.ValueProvider.GetValue("Email").FirstValue + " Email";

            }


            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;
        }
    }
}
