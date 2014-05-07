using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    class Game
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Score { get; set; }
        public long QuizId { get; set; }

    }
}
