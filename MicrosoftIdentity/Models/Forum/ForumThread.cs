﻿namespace MicrosoftIdentity.Models.Forum
{
    public class ForumThread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public List<ForumPost> Posts { get; set; }
    }
}
