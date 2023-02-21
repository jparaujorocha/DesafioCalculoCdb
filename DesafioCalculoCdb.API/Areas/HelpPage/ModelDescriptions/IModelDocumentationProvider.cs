using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DesafioCalculoCdb.Api.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}