using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace SP.Infraestrutura.Common.Base
{
    public abstract class ConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            AplicarAutoInclude(builder);
            ConfigurarEntidade(builder);
        }

        /// <summary>
        /// Método que cada entidade deve implementar para configurações específicas.
        /// </summary>
        public abstract void ConfigurarEntidade(EntityTypeBuilder<T> builder);

        /// <summary>
        /// Aplica AutoInclude automaticamente para propriedades de navegação de coleção.
        /// Evita auto-relacionamentos para prevenir ciclos.
        /// </summary>
        private static void AplicarAutoInclude(EntityTypeBuilder<T> builder)
        {
            var propriedadesNavegacao = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => IsNavigationCollectionProperty(p) && !IsSelfReferencingProperty(p));

            foreach (var propriedade in propriedadesNavegacao)
            {
                try
                {
                    builder.Navigation(propriedade.Name)
                           .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction)
                           .AutoInclude();
                }
                catch
                {
                    // Se não conseguir configurar AutoInclude, ignora
                    // Pode acontecer com propriedades que não são navegação do EF
                }
            }
        }

        /// <summary>
        /// Verifica se a propriedade é uma coleção de navegação válida.
        /// </summary>
        private static bool IsNavigationCollectionProperty(PropertyInfo property)
        {
            if (!property.PropertyType.IsGenericType)
                return false;

            var genericType = property.PropertyType.GetGenericTypeDefinition();

            // Verifica se é ICollection<>, IEnumerable<>, List<>, etc.
            var isCollection = genericType == typeof(ICollection<>) ||
                              genericType == typeof(IEnumerable<>) ||
                              genericType == typeof(List<>) ||
                              typeof(IEnumerable<>).IsAssignableFrom(genericType);

            if (!isCollection)
                return false;

            // Verifica se o tipo genérico é uma classe (possível entidade)
            var genericArgument = property.PropertyType.GetGenericArguments()[0];
            return genericArgument.IsClass && genericArgument != typeof(string);
        }

        /// <summary>
        /// Verifica se a propriedade é um auto-relacionamento (mesmo tipo da entidade).
        /// </summary>
        private static bool IsSelfReferencingProperty(PropertyInfo property)
        {
            if (!property.PropertyType.IsGenericType)
                return false;

            var genericArgument = property.PropertyType.GetGenericArguments()[0];
            return genericArgument == typeof(T);
        }
    }
}
