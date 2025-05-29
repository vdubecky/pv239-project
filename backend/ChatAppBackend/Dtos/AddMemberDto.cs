using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos;

public class AddMemberDto
{
    [Range(1, int.MaxValue)]
    public required int UserId { get; set; }       
}