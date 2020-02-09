﻿using System;
 using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;

 namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class UserFile
    {
        public UserFile()
        {
            AddDate = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Size { get; set; }

        public string Url { get; set; }

        public string LocalId { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public string MediaType { get; set; }

        public string UniqueId { get; set; }

        public bool IsPrivate { get; set; }

        public DateTime AddDate { get; }
    }
}