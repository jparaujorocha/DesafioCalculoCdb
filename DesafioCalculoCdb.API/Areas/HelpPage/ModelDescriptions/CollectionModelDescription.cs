using System.Diagnostics.CodeAnalysis;

namespace DesafioCalculoCdb.Api.Areas.HelpPage.ModelDescriptions
{
    [ExcludeFromCodeCoverage]
    public class CollectionModelDescription : ModelDescription
    {
        public ModelDescription ElementDescription { get; set; }
    }
}