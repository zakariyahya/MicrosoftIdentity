﻿namespace MicrosoftIdentity.Models.Forum
{
    public class Forum
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImagePath { get; set; }
        public int ThreadId { get; set; }
    }
}