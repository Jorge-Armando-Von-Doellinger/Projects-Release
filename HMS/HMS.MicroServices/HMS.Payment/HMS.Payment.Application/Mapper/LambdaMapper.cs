using System.Linq.Expressions;
using System.Reflection;

namespace HMS.Payments.Application.Extensions
{
    internal static class LambdaMapper
    {
        public static Func<TSource, TDestination> CreateDynamicMapper<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource), "source"); // Qualquer nome
            var destinationType = typeof(TDestination);

            // Criar uma nova instância do tipo de destino
            var bindings = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(destProp =>
                {
                    var sourceProp = typeof(TSource).GetProperty(destProp.Name);
                    if (sourceProp != null && destProp.CanWrite)
                    {
                        // Verificar se os tipos das propriedades são compatíveis
                        if (sourceProp.PropertyType == destProp.PropertyType ||
                            destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                        {
                            // Criar uma ligação para a propriedade
                            return Expression.Bind(destProp, Expression.Property(sourceParam, sourceProp));
                        }
                        return null; // Ignorar propriedades que não têm correspondência
                    }
                    return null; // Ignorar propriedades que não têm correspondência
                })
                .Where(binding => binding != null); // Filtrar as ligações nulas

            var body = Expression.MemberInit(Expression.New(destinationType), bindings);
            return Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam).Compile();
        }
    }
}
