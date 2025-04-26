using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Utilities.DbContextSettings.Configurations.Converters
{
    /// <summary>
    /// Конвертер DateTime с явным указанием DateTimeKind.
    /// </summary>
    public sealed class DateTimeKindValueConverter : ValueConverter<DateTime, DateTime>
    {
        private DateTimeKindValueConverter(DateTimeKind kind, ConverterMappingHints? mappingHints = null)
            : base(
                convertToProviderExpression: v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, kind).ToUniversalTime() : v.ToUniversalTime(),
                convertFromProviderExpression: v => DateTime.SpecifyKind(v, kind),
                mappingHints)
        {
        }

        public static readonly DateTimeKindValueConverter Utc =
            new(DateTimeKind.Utc);

        public static readonly DateTimeKindValueConverter Local =
            new(DateTimeKind.Local);

        public static readonly DateTimeKindValueConverter Unspecified =
            new(DateTimeKind.Unspecified);
    }
}
