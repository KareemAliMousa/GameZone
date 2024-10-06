using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        //Validate extension and size
        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
