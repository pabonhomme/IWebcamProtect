using System.ComponentModel.DataAnnotations;

namespace IWebcamProtect.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}