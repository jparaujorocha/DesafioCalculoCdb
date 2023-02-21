using System;
using System.Diagnostics.CodeAnalysis;

namespace DesafioCalculoCdb.Api.Areas.HelpPage.ModelDescriptions
{
    [ExcludeFromCodeCoverage]
    public class ParameterAnnotation
    {
        public Attribute AnnotationAttribute { get; set; }

        public string Documentation { get; set; }
    }
}