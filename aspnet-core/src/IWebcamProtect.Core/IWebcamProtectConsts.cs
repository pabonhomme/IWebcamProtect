using IWebcamProtect.Debugging;

namespace IWebcamProtect
{
    public class IWebcamProtectConsts
    {
        public const string LocalizationSourceName = "IWebcamProtect";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "9d2307059e134787989d4bd7653fc407";
    }
}
