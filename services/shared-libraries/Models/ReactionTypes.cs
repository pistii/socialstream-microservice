﻿namespace shared_libraries.Models
{
    public class ReactionTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PostReaction? PostReaction { get; set; }
    }

    public enum ReactionType
    {
        Like = 1,
        Dislike = 2,
    }
}
