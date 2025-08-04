using AspireOrchestrator.Validation.Interfaces;

namespace AspireOrchestrator.Validation.Business
{
    public static class ValidationRuleFactory
    {
        public static List<IValidationRule> GetAllValidationRules()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().Where(ass => ass.IsDynamic == false)
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IValidationRule)
                    .IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface);
            return types.Select(validation => Activator.CreateInstance(validation) as IValidationRule).ToList()!;
        }
    }
}
