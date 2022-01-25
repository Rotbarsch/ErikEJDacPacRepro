
using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;
using ReproDB.Entities.Generators;

namespace ReproDB.Entities
{
    /// <summary>
    /// DesignTimeServices to prepare Helpers for the CodeTemplates.
    /// </summary>
    public class ScaffoldingDesignTimeServices : IDesignTimeServices
    {
        public static string PREFIX = "MAT";

        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            var options = ReverseEngineerOptions.DbContextAndEntities;
            services.AddHandlebarsScaffolding(options);

            // Add Handlebars scaffolding templates
            services.AddHandlebarsScaffolding(options)
                .AddHandlebarsHelpers(
                    ("normalized-property-name", (Action<EncodedTextWriter, Context, Arguments>)NormalizePropertyName),
                    ("normalized-set-property-name", (Action<EncodedTextWriter, Context, Arguments>)NormalizeSetPropertyName),
                    ("prefixed-class", (Action<EncodedTextWriter, Context, Arguments>)PrefixClassName),
                    ("prefixed-nav-property-type", (Action<EncodedTextWriter, Context, Arguments>)PrefixNavPropertyType),
                    ("prefixed-property-type", (Action<EncodedTextWriter, Context, Arguments>)PrefixPropertyType),
                    ("prefixed-nav-property-annotation", (Action<EncodedTextWriter, Context, Arguments>)PrefixPropertyAnnotation)
                    )
                .AddHandlebarsTransformers(entityFileNameTransformer: EntityFileNameTransformer); ;

            services.AddSingleton<ICSharpDbContextGenerator, DbContextGenerator>();
            
        }

        private string EntityFileNameTransformer(string arg)
        {
            return $"{PREFIX}"+arg;
        }

        private void PrefixPropertyAnnotation(EncodedTextWriter writer, Context context, Arguments arguments)
        {
            var propertyAnnotation = context.GetValue<string>("nav-property-annotation");
            
            propertyAnnotation = propertyAnnotation.Replace("[InverseProperty(nameof(", $"[InverseProperty(nameof({PREFIX}");

            writer.Write(propertyAnnotation);
        }

        private void PrefixPropertyType(EncodedTextWriter writer, Context context, Arguments arguments)
        {
            var propertyType = context.GetValue<string>("property-type");
            writer.Write($"{PREFIX}{propertyType}");
        }

        private void PrefixNavPropertyType(EncodedTextWriter writer, Context context, Arguments arguments)
        {
            var navPropertyType = context.GetValue<string>("nav-property-type");
            writer.Write($"{PREFIX}{navPropertyType}");
        }

        private void PrefixClassName(EncodedTextWriter writer, Context context, Arguments arguments)
        {
            var className = context.GetValue<string>("class");
            writer.Write($"{PREFIX}{className}");
        }

        private void NormalizeSetPropertyName(EncodedTextWriter writer, Context context, Arguments arguments)
        {
            var propertyTypeName = context.GetValue<string>("set-property-type");
            writer.Write($"{PREFIX}{propertyTypeName}");
        }

        private void NormalizePropertyName(EncodedTextWriter writer, Context context, Arguments arguments)
        {
            var propertyName = context.GetValue<string>("property-name");
            if (propertyName.EndsWith("1"))
            {
                propertyName = propertyName.Remove(propertyName.Length - 1, 1);
            }

            writer.Write(propertyName);
        }
    }
}
