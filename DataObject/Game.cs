using System;

namespace DataObject
{
    [Serializable()]
    public class Game
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public long QuizId { get; set; }

    }
}
