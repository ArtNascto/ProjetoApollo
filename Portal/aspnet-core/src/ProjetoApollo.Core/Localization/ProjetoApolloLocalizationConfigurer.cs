using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ProjetoApollo.Localization
{
    public static class ProjetoApolloLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ProjetoApolloConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ProjetoApolloLocalizationConfigurer).GetAssembly(),
                        "ProjetoApollo.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
