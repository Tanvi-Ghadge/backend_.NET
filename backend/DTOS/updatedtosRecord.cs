using System.ComponentModel.DataAnnotations;

namespace backend.DTOS;

public record class updatedtosRecord( 
    
    [Required][StringLength(50)]string Name,
     [Required][StringLength(50)]string Description,
      DateTime CreatedAt, DateTime UpdatedAt);
