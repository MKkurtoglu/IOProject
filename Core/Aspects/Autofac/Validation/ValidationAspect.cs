using Base.CrossCuttingConcerns.ValidationTools;
using Base.Utilities.Interceptors;
using Castle.DynamicProxy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        // bu bir attribute olduğu için ctor ile biz typeof yaparak bu duruma özel olarak validator type yı çekiyoruz.

        public ValidationAspect(Type validatorType) 
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("bu bir validation type değil");
            }

            _validatorType = validatorType;
        }

        // methodinterception dan gelen virtualları override ederek burada custom method yazıyoruz.
        protected override void OnBefore(IInvocation invocation) // buradan gelecek method bilgisi ile
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // ctordan gleen validatoru reflection ile instance ediyoruz.


            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // ve bu type'ın basetype'ının generic argumentlerinden birincisini alıyoruz.
            // productvalidatoru'ın basetypeı abstractvaldator 'ın genric argumenti PRODUCT dönecek.


            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // buradaki invocation bizim gelecek method demek önemli !
            // validatortype'ın generic ifadesinden gelen entitytype'ı ile methoddan gelen parametrelerin eşit olanlarını entites referansına ekle demek

            foreach (var entity in entities)
            {
                ValidationTool.ValidateMethod(validator, entity);
            }
        }
    }
}
