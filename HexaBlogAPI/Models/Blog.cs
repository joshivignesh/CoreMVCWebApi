using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HexaBlogAPI.Models
{
    public class Blog : EntityBase
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string AddedBy { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime AddedDate { get; set; }

    }
}