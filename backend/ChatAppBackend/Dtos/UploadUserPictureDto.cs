namespace ChatAppBackend.Dtos;

public class UploadUserPictureDto
{
    public required IFormFile File { get; set; }
    public required string FileName { get; set; }
}
