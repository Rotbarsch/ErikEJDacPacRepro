
using EntityFrameworkCore.Scaffolding.Handlebars;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.Options;

namespace ReproDB.Entities.Generators
{
    /// <summary>
    /// Class to generate the dbContext; especially the OnModelCreating method in accordance to our naming conventions by editing the already produced code.
    /// </summary>
    public  class DbContextGenerator : HbsCSharpDbContextGenerator
    {
        public DbContextGenerator(
            IProviderConfigurationCodeGenerator providerConfigurationCodeGenerator,
            IAnnotationCodeGenerator annotationCodeGenerator,
            IDbContextTemplateService dbContextTemplateService,
            IEntityTypeTransformationService entityTypeTransformationService,
            ICSharpHelper cSharpHelper,
            IOptions<HandlebarsScaffoldingOptions> options) : base(providerConfigurationCodeGenerator,
            annotationCodeGenerator, dbContextTemplateService, entityTypeTransformationService, cSharpHelper, options)
        {

        }

        protected override void GenerateOnModelCreating(IModel model)
        {
            base.GenerateOnModelCreating(model);

            var onModelCreatingMethod = TemplateData["on-model-creating"] as string;
            var dbSets = TemplateData["dbsets"] as List<Dictionary<string, object>>;

            foreach (var dbSet in dbSets)
            {
                // Prefix Entity Names
                var dbSetPropertyTypeName = dbSet["set-property-type"];
                onModelCreatingMethod = onModelCreatingMethod.Replace($"Entity\u003C{dbSetPropertyTypeName}\u003E", $"Entity\u003C{ScaffoldingDesignTimeServices.PREFIX}{dbSetPropertyTypeName}\u003E");

                // Fix NavProperties
                onModelCreatingMethod = onModelCreatingMethod.Replace($"entity.HasKey(e =\u003E e.{dbSetPropertyTypeName}1)", $"entity.HasKey(e =\u003E e.{dbSetPropertyTypeName})");
                onModelCreatingMethod = onModelCreatingMethod.Replace($"entity.Property(e =\u003E e.{dbSetPropertyTypeName}1)", $"entity.Property(e =\u003E e.{dbSetPropertyTypeName})");
            }

            TemplateData["on-model-creating"] = onModelCreatingMethod;

        }
    }
}
