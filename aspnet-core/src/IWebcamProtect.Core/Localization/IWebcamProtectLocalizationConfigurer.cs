using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace IWebcamProtect.Localization
{
    public static class IWebcamProtectLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(IWebcamProtectConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(IWebcamProtectLocalizationConfigurer).GetAssembly(),
                        "IWebcamProtect.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
